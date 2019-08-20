using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using AdvancedHookManaged;
using NativeFunctionHook;
using Landtory.Engine.API;

namespace Landtory.Process
{
    class Arrest : Script
    {
        Engine.API.Logger logger = new Engine.API.Logger();
        NPed suspect;
        bool OnArrest;
        bool OnDuty;
        bool ReadyToProceed;
        public Arrest()
        {
            logger.Log("Initilazing Arrest", "Arrest");
            this.GUID = new Guid("E8450404-F1B8-4505-A3B6-D77B9C9BF933");
            this.BindScriptCommand("EnableArrest", OnDutyEnabled);
            this.BindScriptCommand("ProceedSuspect", ProceedArrestedSuspect);
            this.Interval = -1;
            this.Tick += new EventHandler(Arrest_Tick);
            this.BindKey(System.Windows.Forms.Keys.E, ArrestSuspect);
        }

        void Arrest_Tick(object sender, EventArgs e)
        {
            if (Exists(Player.GetTargetedPed()) && OnArrest == false)
            {
                AGame.PrintText("Press ~INPUT_PICKUP~ to arrest the suspect when aiming.");
            }
                if (OnArrest == true && !suspect.GTAPed.Exists() || suspect == null || suspect.isArrested)
                {
                    OnArrest = false;
                    return;
                }
                if (ReadyToProceed == true && OnArrest == true && suspect.isInVehicle() && suspect.GTAPed.CurrentVehicle != Player.Character.CurrentVehicle)
                {
                    suspect.GTAPed.Task.LeaveVehicle();
                    suspect.GTAPed.RelationshipGroup = RelationshipGroup.Player;
                }
        }
        void ArrestSuspect()
        {
            logger.Log("Starting Arrest");
            
            if (!Exists(Player.GetTargetedPed()))
            {
                logger.Log("Arrest Failed: Suspect does not exist, or not targetting a ped.", "Arrest", Engine.API.Logger.LogLevel.Error);
                return;
            }
            if (Player.GetTargetedPed() == suspect.GTAPed)
            {
                logger.Log("Arrest Failed: Suspect aleardy apprehended", "Arrest", Engine.API.Logger.LogLevel.Error);
                NGame.PrintSubtitle("~r~This suspect is aleardy apprehended.");
            }
            if (OnArrest == true)
            {
                logger.Log("Arrest Failed: Alerady arresting", "Arrest", Engine.API.Logger.LogLevel.Error);
                NGame.PrintSubtitle("~r~You cannot arrest multiple suspect at once.");
                return;
            }
            if (OnDuty == false)
            {
                return;
            }
            if (Player.Character.Position.DistanceTo(Player.GetTargetedPed().Position) > 10)
            {
                logger.Log("Arrest Failed: Out of range", "Arrest", Engine.API.Logger.LogLevel.Error);
                AGame.PrintText("You must stand close to suspect to preform an arrest.");
                return;
            }
            logger.Log("Arrest Check Passed, starting arrest", "Arrest");
            Random random = new Random();
            int randomed = random.Next(0, 4);
            Ped target = Player.GetTargetedPed();
            if (randomed == 1)
            {
                suspect.GTAPed.Weapons.Glock.Ammo = 5000;
                suspect.GTAPed.Task.SwapWeapon(Weapon.Handgun_Glock);
                suspect.GTAPed.Task.FightAgainst(Player.Character, -1);
            }
            suspect = new NPed(target);
            OnArrest = true;
            if(target.isInVehicle())
            {
                target.Task.LeaveVehicle();
            }
            if(Exists(Player.LastVehicle))
            {
                TaskSequence tasks = new TaskSequence();
                tasks.AddTask.HandsUp(5000);
                tasks.AddTask.RunTo(Player.LastVehicle.Position);
                tasks.AddTask.EnterVehicle(Player.LastVehicle, VehicleSeat.LeftRear);
                tasks.Perform(target);
                ReadyToProceed = true;
                return;
            }
        }
        void OnDutyEnabled(Script sender, ObjectCollection parameter)
        {
            logger.Log("Arrest enabled signal received", "Arrest");

            this.Interval = 100;
        }
        void ProceedArrestedSuspect(Script sender, ObjectCollection parameter)
        {
            logger.Log("Proceed arrested suspect signal received", "Arrest");
            if (OnArrest = false || !Exists(suspect))
            {
                return;
            }
            OnArrest = false;
            if(suspect.isInVehicle())
            {
                suspect.GTAPed.Task.LeaveVehicle();
            }

            for (int num11 = 51; num11 > 1; num11--)
            {
                Functions.SetPedAlpha(suspect.GTAPed, num11 * 5);
                Game.WaitInCurrentScript(10);
            }
            logger.Log("Deleting suspect", "Arrest");
            Functions.SetPedAlpha(suspect.GTAPed, 0);
            suspect.GTAPed.NoLongerNeeded();
            suspect.GTAPed.Delete();
            suspect = null;
            logger.Log("Suspect Apprehended", "Arrest");
            NGame.PrintSubtitle("The suspect was apprehended.");
        }
    }
}
