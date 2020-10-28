namespace StatePattern
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.picLayer = new System.Windows.Forms.PictureBox();
            this.gifBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picScreen
            // 
            this.picScreen.Location = new System.Drawing.Point(12, 15);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(522, 422);
            this.picScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picScreen.TabIndex = 1;
            this.picScreen.TabStop = false;
            this.picScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseClick);
            this.picScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // picLayer
            // 
            this.picLayer.Location = new System.Drawing.Point(12, 15);
            this.picLayer.Name = "picLayer";
            this.picLayer.Size = new System.Drawing.Size(523, 423);
            this.picLayer.TabIndex = 2;
            this.picLayer.TabStop = false;
            this.picLayer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseClick);
            this.picLayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // gifBox
            // 
            this.gifBox.Location = new System.Drawing.Point(11, 15);
            this.gifBox.Name = "gifBox";
            this.gifBox.Size = new System.Drawing.Size(522, 421);
            this.gifBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gifBox.TabIndex = 3;
            this.gifBox.TabStop = false;
            this.gifBox.Visible = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 450);
            this.Controls.Add(this.gifBox);
            this.Controls.Add(this.picLayer);
            this.Controls.Add(this.picScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainView_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainView_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.PictureBox picLayer;
        private System.Windows.Forms.PictureBox gifBox;
    }
}

