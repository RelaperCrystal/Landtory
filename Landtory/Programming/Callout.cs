using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;

namespace Landtory.Programming
{
    public abstract class Callout
    {
        /// <summary>
        /// Create all the objects.
        /// </summary>
        /// <returns>If false, the callout disregard.</returns>
        public bool OnCalloutAccepted();
        public bool OnCalloutWaitingForAccept();
        public bool OnCalloutCreated();
    }
}
