using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Landtory.Engine;
using Landtory.Engine.API;
using GTA;

namespace Landtory.Engine.API.Backup
{

    public enum ENormalUnitState
    {
        Start,
        EnRoute,
        Arrived,
        Leave,
        Kill
    }
    /// <summary>
    /// A normal unit.
    /// </summary>
    public class NormalUnit
    {
        private NPed officer;
        private NPed secOfficer;

        private Vehicle CopCar;

        private TaskSequence officerSequence;
#pragma warning disable IDE0044 // 添加只读修饰符
        private TaskSequence secOfficerSequence;
#pragma warning restore IDE0044 // 添加只读修饰符

        private Timer time;

#pragma warning disable IDE0044 // 添加只读修饰符
        private ENormalUnitState state;
#pragma warning restore IDE0044 // 添加只读修饰符
        public NormalUnit()
        {
            officer = new NPed(Model.BasicCopModel, Game.LocalPlayer.Character.Position.Around(40.0f));
            secOfficer = new NPed(Model.BasicCopModel, officer.GTAPed.Position.Around(5.0f));

            CopCar = World.CreateVehicle(Model.BasicPoliceCarModel, secOfficer.GTAPed.Position.Around(7.0f));

            officer.GTAPed.Task.WarpIntoVehicle(CopCar, VehicleSeat.Driver);
            secOfficer.GTAPed.Task.WarpIntoVehicle(CopCar, VehicleSeat.RightFront);

            officerSequence = new TaskSequence();
            secOfficerSequence = new TaskSequence();

            CopCar.SirenActive = true;
            time = new Timer
            {
                Interval = 100
            };
            time.Start();
            time.Tick += Time_Tick;
        }

        void Time_Tick(object sender, EventArgs e)
        {
            switch(state)
            {
                case ENormalUnitState.Start :
                    officerSequence.AddTask.DriveTo(Game.LocalPlayer.Character.Position.Around(10.0f), 25, false);
                    officerSequence.AddTask.LeaveVehicle();
                    officerSequence.Perform(officer.GTAPed);
                    goto case ENormalUnitState.Arrived;
                case ENormalUnitState.Arrived :
                    CopCar.SirenActive = false;
                    secOfficer.GTAPed.Task.LeaveVehicle();
                    officer.Tasks.ClearAll();
                    secOfficer.Tasks.ClearAll();
                    break;
            }
        }
    }
}
