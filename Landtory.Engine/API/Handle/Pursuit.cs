using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using Landtory.Engine.API;
using NativeFunctionHook;
using Landtory;
using Landtory.Engine;
using Landtory.Engine.API.Common;

namespace Landtory.Process
{
    class Pursuit
    {
        private Ped target;
        private NPed Ntarget;
        private Blip targetB;
        private List<Blip> officerblips;
        private readonly BlipIcon icon = BlipIcon.Misc_Waypoint;
        private readonly BlipColor color = BlipColor.Cyan;
        private List<Ped> officers;
        private Player Plyr = Game.LocalPlayer;
        private Timer time = new Timer();
        public Pursuit(Ped suspect)
        {
            target = suspect;
            targetB = suspect.AttachBlip();
            targetB.Friendly = false;
            targetB.Name = "Suspect";
            
            
            if (target.isInVehicle())
            {
                target.Task.CruiseWithVehicle(target.CurrentVehicle, 50, false);
                target.Task.AlwaysKeepTask = true;
                target.SayAmbientSpeech("GENERIC_FUCK_OFF");
            }
            else
            {
                target.SetPathfinding(false, true, false);
                target.Task.FleeFromChar(Plyr.Character, true);
            }
            time.Tick += new EventHandler(Time_Tick);
            time.Interval = 100;
        }

        void Time_Tick(object sender, EventArgs e)
        {
            if (target.Exists() == false || target == null)
            {
                Kill();
            }
            if (Ntarget.IsArrested)
            {
                NGame.PrintSubtitle("Suspect Apprehended!");
                Kill();
            }
            foreach(Ped officer in officers)
            {
                if(officer.isInVehicle() && target.isInVehicle())
                {
                    officer.Task.DriveTo(target, 30, false);
                }
                if(officer.isInVehicle() &&  !target.isInVehicle())
                {
                    TaskSequence pursue = new TaskSequence();
                    pursue.AddTask.LeaveVehicle(officer.CurrentVehicle, false);
                    pursue.AddTask.RunTo(target.Position);
                }
                if(!officer.isInVehicle() && target.isInVehicle())
                {
                    officer.Task.EnterVehicle();
                }
                if(!officer.isInVehicle() && !target.isInVehicle())
                {
                    TaskSequence pursue = new TaskSequence();
                    pursue.AddTask.RunTo(target.Position);
                    pursue.AddTask.AimAt(target, -1);
                    pursue.Perform(officer);
                    if(officer.Position.DistanceTo(target.Position) < 4.0f)
                    {
                        officer.Task.ClearAllImmediately();
                        PedOpreation.FootOfficerNPCArrestPed(officer, target);
                    }
                }
            }
        }

        public void AttachBackup()
        {
            Ped officer = World.CreatePed(Model.BasicCopModel, Plyr.Character.Position.Around(30f));
            Vehicle copcar = World.CreateVehicle(Model.BasicPoliceCarModel, Plyr.Character.Position.Around(29f));
            Blip copBlip = copcar.AttachBlip();
            copBlip.Color = color;
            copBlip.Icon = icon;
            copcar.SirenActive = true;
            officer.WarpIntoVehicle(copcar, VehicleSeat.Driver);
            officer.Task.ClearAllImmediately();
            AttachOfficerInVehicle(officer, copBlip);
        }

        /// <summary>
        /// Attach a officer in pursuit.
        /// </summary>
        /// <param name="officer">Officer.</param>
        /// <exception cref="GTA.NonExistingObjectException"></exception>
        public void AttachOfficerOnFoot(Ped officer)
        {
            if (officer.Exists() == false)
            {
                throw new GTA.NonExistingObjectException("That officer ped you called dosen't exist or not created.");
            }
            if (officer.isInVehicle() == true)
            {
                throw new ArgumentException("The officer ped you called is in a vehicle.");
            }
            officer.Task.ClearAllImmediately();
            officer.SayAmbientSpeech("CHASE_SOLO");
            officers.Add(officer);
        }
        public void AttachOfficerInVehicle(Ped officer, Blip blip)
        {
            if (officer.Exists() == false)
            {
                throw new GTA.NonExistingObjectException("That officer ped you called dosen't exist or not created.");
            }
            if (officer.isInVehicle() == false)
            {
                throw new ArgumentException("The officer ped you called is on foot.");
            }
            officer.Task.ClearAllImmediately();
            officer.CurrentVehicle.SirenActive = true;
            officer.SayAmbientSpeech("CHASE_SOLO");
            
            if (target.isInVehicle())
            {
                officer.SayAmbientSpeech("PULL_OVER_WARNING");
            }
            else
            {
                officer.SayAmbientSpeech("MEGAPHONE_FOOT_PURSUIT");
                officer.Task.LeaveVehicle();
            }
            officers.Add(officer);
            officerblips.Add(blip);
        }
        public void AttachNooseTeamBackup()
        {
            Ped SwatDriver = World.CreatePed(Plyr.Character.Position.Around(30f));
            Ped SwatOfficer = World.CreatePed(Plyr.Character.Position.Around(30f));
            Vehicle SwatVehicle = World.CreateVehicle("NSTOCKADE", Plyr.Character.Position.Around(30f));
            SwatDriver.WarpIntoVehicle(SwatVehicle, VehicleSeat.Driver);
            SwatOfficer.WarpIntoVehicle(SwatVehicle, VehicleSeat.RightFront);
            SwatOfficer.WillDoDrivebys = true;
            SwatVehicle.SirenActive = true;
        }

        private void Kill()
        {
            foreach(Ped officer in officers)
            {
                if (officer == null || officer.Exists() == false) continue;
                if (officer.isInVehicle())
                {
                    if(CommonFunc.GetRandomBool())
                    {
                        officer.Task.ClearAll();
                        officer.Task.CruiseWithVehicle(officer.CurrentVehicle, 15f, true);
                        officer.NoLongerNeeded();
                    }
                    else
                    {
                        officer.Task.ClearAll();
                        officer.Task.LeaveVehicle();
                        officer.Task.WanderAround();
                        officer.NoLongerNeeded();
                    }
                    
                }
                if (officer.isInVehicle() == false)
                {
                    officer.Task.ClearAll();
                    officer.Task.WanderAround();
                    officer.NoLongerNeeded();
                }
            }
        }
    }
}
