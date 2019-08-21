using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GTA;
using NativeFunctionHook;
using AdvancedHookManaged;


namespace Landtory
{
    class Main : Script
    {
        bool SirenDriver;
        Checkpoint CheckStation;
        Engine.API.Logger logger = new Engine.API.Logger();
        public Main()
        {
            this.BindKey(System.Windows.Forms.Keys.L, false, false, true, Load_Mod);
        }


        void Load_Mod()
        {
            try
            {
                Vector3 vector = new Vector3();
                vector.X = 79.2884f;
                vector.Y = -713.946f;
                vector.Z = 4.95886f;
                logger.Log("Initilazing Checkpoint", "Main");
                CheckStation = new Checkpoint(vector, Color.Yellow, 0.5f);

                this.Interval = 100;
                logger.Log("Initilazing Mod Functions", "Main");
                this.Tick += new EventHandler(Main_Tick);
                this.BindKey(System.Windows.Forms.Keys.End, SirenSwitchDriver);
                this.GUID = new Guid("2AA8D642-C91F-4688-A32C-7D27F588014A");
                AGame.PrintText("Welcome to Landtory. Go to Goldberg & Shyster to get on duty.");
            }
            catch (Exception ex)
            {
                logger.Log("Fatal Error During Initilazing Mod, Caused by: " + ex.Message + Environment.NewLine + ex.ToString(), Engine.API.Logger.LogLevel.Fatal);
                Game.DisplayText("Fatal Error of Landtory! Check Landtory.log for more info.");
            }
        }
        void Main_Tick(object sender, EventArgs e)
        {
            if (Player.Character.Position.DistanceTo(CheckStation.Position) < 0.5f)
            {
                logger.Log("On Duty checkpoint arrived, sending signal", "Main");
                SendScriptCommand(new Guid("983110F2-7F23-4248-8B5C-315641351FEB"), "ON_DUTY");
                this.Interval = 0;
            }
        }

        void SirenSwitchDriver()
        {
            if (Exists(Player.Character.CurrentVehicle) == false)
            {
                logger.Log("Siren without Driver switch failed: No Vehicle", "Main");
                NGame.PrintSubtitle("~r~You need a vehicle to switch Siren without Driver.");
                return;
            }
            if (SirenDriver)
            {
                Player.Character.CurrentVehicle.AllowSirenWithoutDriver = false;
                SirenDriver = false;
                NGame.PrintSubtitle("Siren Without Driver Off");
                logger.Log("Siren without Driver OFF", "Main");
            }
            else
            {
                Player.Character.CurrentVehicle.AllowSirenWithoutDriver = true;
                SirenDriver = true;
                NGame.PrintSubtitle("Siren Without Driver On");
                logger.Log("Siren without Driver ON", "Main");
            }
        }
    }
}
