using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace StatePattern
{
    public partial class MainView : Form
    {
        protected class TouchArea {
            public RectangleF Rect { get; set; }
            public int Key { get; set; }
        }


        private IConfigurationRoot _configRoot;


        private List<TouchArea> touchAreas;
    

        ToolTip mouseTip;


        private bool CloseApp = false;

        private ApplicationSettings applicationCfg;
        

        public MainView(IConfigurationRoot configRoot)
        {
            InitializeComponent();
            this.picScreen.Controls.Add(this.picLayer);
            this.picLayer.BackColor = Color.Transparent;
            this.picLayer.BringToFront();

            mouseTip = new ToolTip();

            _configRoot = configRoot;
            applicationCfg = new ApplicationSettings();

            this.Load += Form_Load;


            touchAreas = new List<TouchArea>();
        }



        private void Form_Load(object sender, EventArgs e)
        {
            _configRoot.GetSection(ApplicationSettings.Section).Bind(applicationCfg);

            StateManager.Instance.NextState += this.OnNextState;
            StateManager.Instance.Initilize(this, Options.Create<List<State>>(applicationCfg.StateData));
            StateManager.Instance.RunState(0);
        }





        public void OnNextState(object sender, int nextStateId)
        {
            if (CloseApp)
                this.SafeInvoke(() => base.Close());

            if (!Object.ReferenceEquals(sender, StateManager.Instance))
                return;

            StateManager.Instance.RunState(nextStateId);
        }




        public void Draw(string screenId)
        {
            picScreen.Image?.Dispose();

            var screen = applicationCfg.ScreenData.FirstOrDefault(s => s.Id.ToUpper() == screenId.ToUpper());

            if (screen == null)
                return;

            initTouchArea(screen);

            var image = screen.Parameters["Image"];
            if (!string.IsNullOrEmpty(image))
            {
                if (image.ToLower().Contains(".gif"))
                    StartGif(image);
                else
                {
                    StopGif();
                    picScreen.Image = Image.FromFile(image);
                }
            }
        }




        private void StartGif(string gifFile) 
        {
            playGif = true;

            Thread thread = new Thread( new ThreadStart(() => PlayGif(gifFile)) );
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        bool playGif = false;
        private void PlayGif(string gifFile)
        {
            gifBox.SafeInvoke(() => {
                gifBox.Image?.Dispose();
                gifBox.Image = Image.FromFile(gifFile);
                gifBox.Visible = true;
                });

            while (this.SafeInvoke(() => this.playGif))
            {
                Application.DoEvents();
                Thread.Sleep(200);
            }

            this.Invoke((MethodInvoker)delegate { gifBox.Visible = false; });
        }

        private void StopGif()
        {
            playGif = false;
        }



        private void initTouchArea(Screen screen)
        {
            touchAreas.Clear();
            
            var touch = screen.Parameters.Keys.Where(k => k.ToUpper().Contains("TOUCH")).ToList();

            foreach (var t in touch)
            {
                var Data = screen.Parameters[t].Trim();
                var data = Data.Split(';');
                var p1 = data[0].Split(',');
                var p2 = data[1].Split(',');
                var key = int.Parse(data[2]);

                if (p1.Count() >= 2 && p2.Count() >= 2)
                {
                    var left = int.Parse(p1[0]);
                    var top = int.Parse(p1[1]);
                    var right = int.Parse(p2[0]);
                    var bottom = int.Parse(p2[1]);

                    touchAreas.Add(new TouchArea
                        {
                        Rect = RectangleF.FromLTRB((float)left, (float)top, (float)right, (float)bottom),
                        Key = key
                        }
                    );
                }
            }
        }





        void MainView_KeyPress(object sender, KeyPressEventArgs e)
        {
            StateManager.Instance.OnKeyPress(sender, e);
            e.Handled = true;
        }



        void MainView_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var area = getCorrespondenteArea(new PointF((float)e.X, (float)e.Y));

            if (area != null)
                StateManager.Instance.OnKeyPress(this,new KeyPressEventArgs((char)area.Key));
        }



        void MainView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            string tipText = String.Format("({0}, {1})", e.X, e.Y);
            mouseTip.Show(tipText, this, e.Location);            
        }




        private TouchArea getCorrespondenteArea(PointF p)
        {
            return touchAreas.FirstOrDefault(r =>
                p.X >= r.Rect.Left && p.X <= r.Rect.Right &&
                p.Y <= r.Rect.Bottom && p.Y >= r.Rect.Top);
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    // reject close application
            CloseApp = true;    // warning to break state flow...
        }

    }
}
