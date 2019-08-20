using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA.Native;

namespace NativeFunctionHook
{
    public class NGame
    {
        /// <summary>
        /// Returns the Current Player's ID. 
        /// The Player ID is meaningless for other script functions and should be converted to a Player Index.
        /// </summary>
        /// <returns>Player ID.</returns>
        public static int GetPlayerId()
        {
            int result = Function.Call<int>("GET_PLAYER_ID");
            return result;
        }

        public static int TimeSincePlayerLastArrested
        {
            get
            {
                int result = Function.Call<int>("GET_TIME_SINCE_LAST_ARREST");
                return result;
            }
        }

        public static int TimeSincePlayerLastDied
        {
            get
            {
                int result = Function.Call<int>("GET_TIME_SINCE_LAST_DEATH");
                return result;
            }
        }

        /// <summary>
        /// Removes the player's current prostitute. Used before initiating a date.
        /// Use with caution.
        /// </summary>
        public static void RemovePlayerProstitute()
        {
            Function.Call("GET_RID_OF_PLAYER_PROSTITUTE");
        }

        /// <summary>
        /// Print a text on subtitle position, at bottom of the screen.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        public static void PrintSubtitle(string text, int duration = 7000)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", text, duration });
        }
    }
}
