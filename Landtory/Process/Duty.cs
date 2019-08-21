using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using NativeFunctionHook;
using Landtory.Engine.API.Common;

namespace Landtory.Process
{
    class Duty : Script
    {
        Engine.API.Logger logger = new Engine.API.Logger();
        private bool OnDutyStat;
        public Duty()
        {
            logger.Log("Initilazing Duty Manager", "Duty");
            this.GUID = new Guid("983110F2-7F23-4248-8B5C-315641351FEB");
            base.BindScriptCommand("ON_DUTY", OnDuty);
            base.BindConsoleCommand("LandtoryForceDuty", ConsoleDutyOn, "To force a duty with Landtory; called LandtoryForceDuty for do not confuse with LCPD:FR.");
            this.Tick += new EventHandler(Duty_Tick);
        }

        void Duty_Tick(object sender, EventArgs e)
        {
            Player.WantedLevel = 0;
        }

        void OnDuty(Script sender, ObjectCollection parameter)
        {
            logger.Log("On Duty signal received", "Duty");
            DutyOn();
        }

        void DutyOn()
        {
            if (OnDutyStat)
            {
                SendScriptCommand("E8450404-F1B8-4505-A3B6-D77B9C9BF933","ProceedSuspect");
                return;            
            }
            OnDutyStat = true;
            logger.Log("Start On Duty function", "Duty");
            this.Interval = 100;
            if (Player.isOnMission)
            {
                logger.Log("Player is on mission, Request Aborted and disabled Mod","Duty" ,Engine.API.Logger.LogLevel.Warning);
                return;
            }
            logger.Log("Changing Random Cop Model and Relationship", "Duty");
            Player.Model = Model.BasicCopModel;
            Player.Character.RelationshipGroup = RelationshipGroup.Cop;
            

            Vector3 CarPosition = new Vector3();
            CarPosition.X = 85.9519f;
            CarPosition.Y = -724.686f;
            CarPosition.Z = 4.99546f;
            Vehicle dutyCar = World.CreateVehicle(Model.BasicPoliceCarModel, CarPosition);
            Blip dutyCarBlip = dutyCar.AttachBlip();
            dutyCarBlip.Icon = BlipIcon.Building_Garage;
            // yellow on IV/TLaD, pink on TBoGT
            dutyCarBlip.Color = BlipColor.Yellow;

            logger.Log("Sending arrest enabled signal", "Duty");
            SendScriptCommand(new Guid("E8450404-F1B8-4505-A3B6-D77B9C9BF933"), "EnableArrest", dutyCar);

            NGame.PrintSubtitle(NLanguage.GetLangStr("OnDutyNow"));
        }

        void ConsoleDutyOn(ParameterCollection parameters)
        {
            logger.Log("Received force duty console command");
            DutyOn();
        }
    }
}
