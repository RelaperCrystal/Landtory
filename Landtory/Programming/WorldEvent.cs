using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Landtory.Programming
{
    public abstract class WorldEvent
    {
        private string _name;

        public string name
        {
            get
            {
                return "WorldEvent."+_name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Stop whole world event.
        /// </summary>
        public abstract void Kill();
    }
}
