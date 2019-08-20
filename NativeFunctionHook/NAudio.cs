using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA.Native;

namespace NativeFunctionHook
{
    public class NAudio
    {
        /// <summary>
        /// Trigger a story missions police report.
        /// </summary>
        /// <param name="Type">Go to https://gtamods.com/wiki/TRIGGER_POLICE_REPORT to find all of police report type.</param>
        public static void TriggerStoryPoliceReport(string Type)
        {
            Function.Call("TRIGGER_POLICE_REPORT", Type);
        }

        /// <summary>
        /// This function will play a mission complete audio usually heard when a mission is completed.
        /// The audio are located in the gps.rfp file. Valid values include 6-7, 10-11, 15, 18, 24-25, 27-28, 33-35, 42-43, 50-69, 71.
        /// Using other numbers will not play any audio. The audio will only play if a mission complete audio is not currently playing.
        /// </summary>
        /// <param name="Type">The type of Mission Complete Sound.</param>
        public static void TriggerMissionCompleteSFX(int Type)
        {
            Function.Call("TRIGGER_MISSION_COMPLETE_AUDIO", Type);
        }

        /// <summary>
        /// Switch ON or OFF for the ambient police scanner.
        /// </summary>
        /// <param name="Switch">ON or OFF.</param>
        public static void SwitchPoliceScanner(bool Switch)
        {
            if (!Switch) Function.Call("DISABLE_POLICE_SCANNER");
            else Function.Call("ENABLE_POLICE_SCANNER");
        }
    }
}
