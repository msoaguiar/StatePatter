
using System.Collections.Generic;

namespace StatePattern
{

    public class ApplicationSettings
    {
        public static string Section = "Application";

        //public StateData StateData { get; set; }
        public List<State> StateData { get; set; }

        public List<Screen> ScreenData { get; set; }
    }

    /*
    public class StateData 
    {
        public static string Section = "StateData";
        public List<State> States { get; set; }
    }*/

    public class State {
        public string Type { get; set; } // state class type
        public int Id { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }

    /*
    public class ScreenData
    {
        public static string Section = "ScreenData";
        public List<Screen> Screens { get; set; }
    }*/

    public class Screen
    {
        public string Id { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
