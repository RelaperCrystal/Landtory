using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using Landtory.Engine.API;

namespace Landtory.Engine
{
    public class PedOpreation
    {
        public static void StopVehiclePed(Ped target)
        {
            if (target == null)
            {
                throw new ArgumentNullException();
            }
            if (!Game.Exists(target))
            {
                throw new NonExistingObjectException();
            }
            if (!target.isInVehicle())
            {
                throw new InvalidOperationException("The target is on foot.");
            }
            if (!Game.Exists(target.CurrentVehicle))
            {
                throw new NonExistingObjectException("The vehicle of the ped not exist.");
            }

            TaskSequence pullOver = new TaskSequence();
            pullOver.AddTask.DriveTo(target.Position.Around(10.5f), 10, true);
            pullOver.AddTask.LeaveVehicle(target.CurrentVehicle, false);
            pullOver.AddTask.HandsUp(1000);
            pullOver.AddTask.RunTo(Game.LocalPlayer.Character.Position, true);
            pullOver.AddTask.StandStill(-1);
            pullOver.Perform(target);
        }

        public static void FootOfficerPursuitPed(Ped officer, Ped target)
        {
            // Let's do a quick check before using these
            if(officer == null || target == null)
            {
                throw new ArgumentNullException();
            }
            if (!officer.Exists() || !target.Exists())
            {
                throw new NonExistingObjectException();
            }
            
        }

        public static void FootOfficerNPCArrestPed(Ped officer, Ped target)
        {
            if ( officer == null || target == null ||!Game.Exists(officer) || !Game.Exists(target) || officer.isInVehicle() || target.isInVehicle())
            {
                throw new ArgumentException();
            }
            Vehicle PoliceCar = World.CreateVehicle(Model.BasicPoliceCarModel, officer.Position.Around(35.0f));
            Ped TransportDriver = World.CreatePed(Model.BasicCopModel, officer.Position.Around(35.0f));
            TransportDriver.WarpIntoVehicle(PoliceCar, VehicleSeat.Driver);
            TransportDriver.Task.DriveTo(officer, 20, false);
            PoliceCar.SirenActive = true;
            PoliceCar.isRequiredForMission = true;
            
            officer.Task.ClearAllImmediately();
            target.Task.ClearAllImmediately();
            officer.Task.AlwaysKeepTask = true;
            target.Task.AlwaysKeepTask = true;
            TaskSequence officerTask = new TaskSequence();
            TaskSequence targetTask = new TaskSequence();
            officerTask.AddTask.AimAt(target, 1000);
            officerTask.AddTask.RunTo(target.Position, true);
            officerTask.AddTask.AimAt(target, 15000);
            officerTask.AddTask.EnterVehicle(PoliceCar, VehicleSeat.RightFront);

            targetTask.AddTask.HandsUp(17000);
            targetTask.AddTask.EnterVehicle(PoliceCar, VehicleSeat.RightRear);
            officerTask.Perform(officer);
            targetTask.Perform(target);
            Game.WaitInCurrentScript(18000);
            TransportDriver.Task.CruiseWithVehicle(PoliceCar, 15, true);
            TransportDriver.NoLongerNeeded();
            PoliceCar.isRequiredForMission = false;
            PoliceCar.NoLongerNeeded();
            target.NoLongerNeeded();
            officer.NoLongerNeeded();
        }

        public static void ArrestPed(NPed suspect)
        {
            suspect.isArrested = true;
        }
    }
}
