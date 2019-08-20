using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA.Native;
using GTA;

namespace NativeFunctionHook
{
    public class NWorld
    {
        /// <summary>
        /// Get all injured ped number in range.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="Range"></param>
        /// <returns></returns>
        public static int GetInjuredPedInRange(Vector3 pos, float Range)
        {
            int result = Function.Call<int>("GET_NUMBER_OF_INJURED_PEDS_IN_RANGE", new Parameter[]{
            pos.X, pos.Y, pos.Z, Range
            });
            return result;
        }

        /// <summary>
        /// Adds a Police Station spawn point when the player gets busted.
        /// </summary>
        /// <param name="Position">The coordinates of spawnpoint.</param>
        /// <param name="Facing">Facing when player respawn.</param>
        /// <param name="StationID">Police Station ID.</param>
        public static void AddBustedRespawnPoint(Vector3 Position, float Facing, int StationID)
        {
            float X = Position.X;
            float Y = Position.Y;
            float Z = Position.Z;
            Function.Call("ADD_POLICE_RESTART", new Parameter[]{
                X, Y, Z, Facing, StationID
            });
        }

        /// <summary>
        /// This function adds a string to the news scrollbar as seen in The Triangle, south of Star Junction.
        /// The news scrollbar in northern Star Junction is controlled by scrollbars.dat. 
        /// Recommonded to call <see cref="NativeFunctionHook.Functions.ClearNewsScrollBar()"/> before calling this function. If you don't, 
        /// the string will be added next to the previous one that was created.
        /// </summary>
        /// <param name="text"></param>
        public static void AddStringToNewsScrollBar(string text)
        {
            Function.Call("ADD_STRING_TO_NEWS_SCROLLBAR", new Parameter[]{
                text
            });
        }

        /// <summary>
        /// This function removes all text on the news scrollbar as seen in The Triangle, south of Star Junction.
        /// The news scrollbar in northern Star Junction is controlled by scrollbars.dat. 
        /// </summary>
        /// <param name="text"></param>
        public static void ClearNewsScrollBar(string text)
        {
            Function.Call("CLEAR_NEWS_SCROLLBAR");
        }
    }
}
