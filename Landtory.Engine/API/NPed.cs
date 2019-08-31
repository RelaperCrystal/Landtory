using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;

namespace Landtory.Engine.API
{
    public class NPed
    {
        /// <summary>
        /// Create NPed from <see cref="GTA.Model"/> and a position.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="position"></param>
        public NPed(Model model, Vector3 position)
        {
            GTAPed = World.CreatePed(model, position);
        }
        /// <summary>
        /// Create NPed from string model name, and a position.
        /// </summary>
        /// <param name="model">Model string name, such as "M_Y_COP". Must be a available name.</param>
        /// <param name="position">The Vector.</param>
        public NPed(string model, Vector3 position)
        {
            GTAPed = World.CreatePed(model, position);
        }
        /// <summary>
        /// Create NPed from a position and a random street model, depends on areas.
        /// </summary>
        /// <param name="position">The Vector.</param>
        public NPed(Vector3 position)
        {
            GTAPed = World.CreatePed(position);
        }
        /// <summary>
        /// Create NPed from GTA Ped.
        /// </summary>
        /// <param name="GTAPedT">For no confusing beetwen coding, use GTAPedT as name.</param>
        public NPed(Ped GTAPedT)
        {
            GTAPed = GTAPedT;
        }
        public Ped GTAPed { get; private set; }
        public bool isInVehicle()
        {
            return GTAPed.isInVehicle();
        }
        public bool isInWater
        {
            get
            {
                return GTAPed.isInWater;
            }
        }
        public bool isInMeleeCombat
        {
            get
            {
                return GTAPed.isInMeleeCombat;
            }
        }
        public bool isInjured
        {
            get
            {
                return GTAPed.isInjured;
            }
        }
        public bool isInCombat
        {
            get
            {
                return GTAPed.isInCombat;
            }
        }
        public bool isOnFire
        {
            get
            {
                return GTAPed.isOnFire;
            }
        }
        public bool isOnScreen
        {
            get
            {
                return GTAPed.isOnScreen;
            }
        }
        public bool isRagdoll
        {
            get
            {
                return GTAPed.isRagdoll;
            }
        }
        public bool isRequiredForMission
        {
            get
            {
                return GTAPed.isRequiredForMission;
            }
            set
            {
                GTAPed.isRequiredForMission = value;
            }
        }
        public bool isShooting
        {
            get
            {
                return GTAPed.isShooting;
            }
        }
        /// <summary>
        /// Please use <seealso cref="isRequiredForMission"/> instead.
        /// </summary>
        [Obsolete("Please use isRequiredForMission instead.")]
        public bool isMissionCharacter
        {
            get
            {
                return GTAPed.isMissionCharacter;
            }
        }

        private bool isArrested_;

        public bool isArrested
        {
            get
            {
                return isArrested_;
            }
            internal set
            {
                isArrested_ = value;
            }
        }

        public PersonalInfo info { get; internal set; }

        public GTA.value.PedTasks Tasks
        {
            // How i can set the tasks, i wandered
            get
            {
                return GTAPed.Task;
            }
        }
    }
}
