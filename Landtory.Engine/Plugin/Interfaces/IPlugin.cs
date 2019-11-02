using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Landtory.Engine.Plugin.Interfaces
{
    public interface IPlugin
    {
        /// <summary>
        /// Called when plugin is initialized.
        /// </summary>
        void OnInitialized();

        /// <summary>
        /// Called when plugin ending accidently.
        /// </summary>
        void OnEnding();

        /// <summary>
        /// Called when player is on duty.
        /// </summary>
        void WhenOnDuty();
    }
}
