using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using GTA;

namespace TimeFix.net
{
    public class Main : Script
    {
        private static bool changeTime;
        private static bool respray;
        private static GTA.Native.Pointer hour = new GTA.Native.Pointer(typeof(int));
        private static GTA.Native.Pointer minute = new GTA.Native.Pointer(typeof(int));
        private static GTA.Native.Pointer day = new GTA.Native.Pointer(typeof(int));
        private static GTA.Native.Pointer newDay = new GTA.Native.Pointer(typeof(int));
        public Main()
        {
            Interval = 250;
            this.Tick += new EventHandler(this.Main_Tick);
        }
        private int Ra(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private void Main_Tick(object sender, EventArgs e)
        {
            if ((Player.Character.isDead) && !changeTime)
            {
                changeTime = true;
                day = GTA.Native.Function.Call<int>("GET_CURRENT_DAY_OF_WEEK");
            }
            if (changeTime && Player.Character.isAlive && GTA.Native.Function.Call<bool>("IS_SCREEN_FADING_IN"))
            {
                GTA.Native.Function.Call("GET_TIME_OF_DAY", hour, minute);
                newDay = GTA.Native.Function.Call<int>("GET_CURRENT_DAY_OF_WEEK");

                int thatDay = day;
                int thisDay = newDay;

                if ((hour < 12) && thatDay == thisDay)
                {
                    GTA.Native.Function.Call("SET_TIME_ONE_DAY_FORWARD");
                    changeTime = false;
                }
                else
                    changeTime = false;
            }
            if (GTA.Native.Function.Call<bool>("HAS_RESPRAY_HAPPENED") && GTA.Native.Function.Call<bool>("IS_SCREEN_FADING_IN"))
            {
                GTA.Native.Function.Call("GET_TIME_OF_DAY", hour, minute);
                newDay = GTA.Native.Function.Call<int>("GET_CURRENT_DAY_OF_WEEK");

                int thatDay = day;
                int thisDay = newDay;

                if ((hour < 3))
                    GTA.Native.Function.Call("SET_TIME_ONE_DAY_FORWARD");
            }
        }
    }
}
