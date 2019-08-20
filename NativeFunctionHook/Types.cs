using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeFunctionHook
{
    public enum ClearType
    {
        /// <summary>
        /// Clear all: uses CLEAR_AREA
        /// </summary>
        All,
        /// <summary>
        /// Clear only cars: uses CLEAR_AREA_OF_CARS
        /// </summary>
        Cars,
        /// <summary>
        /// Clear only peds: uses CLEAR_AREA_OF_CHARS
        /// </summary>
        Chars,
        /// <summary>
        /// Clear only cops: uses CLEAR_AREA_OF_COPS
        /// </summary>
        Cops,
        /// <summary>
        /// Clear only objects: uses CLEAR_AREA_OF_OBJECTS
        /// </summary>
        Objects
    }
}
