using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using GTA.Native;

namespace NativeFunctionHook
{
    public class Functions
    {
        /// <summary>
        /// Adds a Police Station spawn point when the player gets busted.
        /// </summary>
        /// <param name="Position">The coordinates of spawnpoint.</param>
        /// <param name="Facing">Facing when player respawn.</param>
        /// <param name="StationID">Police Station ID.</param>
        [Obsolete("Should use NWorld.AddBustedRespawnPoint. Don't worry, they uses same logic.")]
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
        [Obsolete]
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
        [Obsolete]
        public static void ClearNewsScrollBar(string text)
        {
            Function.Call("CLEAR_NEWS_SCROLLBAR");
        }

        public static void ClearArea(ClearType type, Vector3 position, float radius)
        {
            switch(type)
            {
                case ClearType.All :
                    Function.Call("CLEAR_AREA", new Parameter[]{
                        position.X, position.Y, position.Z, radius, true
                    });
                    return;
                case ClearType.Chars :
                    Function.Call("CLEAR_AREA_OF_CHARS", new Parameter[]{
                        position.X, position.Y, position.Z, radius   
                    });
                    return;
                case ClearType.Cars :
                    Function.Call("CLEAR_AREA_OF_CARS", new Parameter[]{
                        position.X, position.Y, position.Z, radius
                    });
                    return;
                case ClearType.Cops :
                    Function.Call("CLEAR_AREA_OF_COPS", new Parameter[]{
                        position.X, position.Y, position.Z, radius
                    });
                    return;
                case ClearType.Objects :
                    Function.Call("CLEAR_AREA_OF_OBJECTS", new Parameter[]{
                        position.X, position.Y, position.Z, radius
                    });
                    return;
            }
        }

        [Obsolete]
        public static void SwitchPoliceScanner(bool Switch)
        {
            if (!Switch) Function.Call("DISABLE_POLICE_SCANNER");
            else Function.Call("ENABLE_POLICE_SCANNER");
        }

        /// <summary>
        /// Switch the on or off of the save house.
        /// </summary>
        /// <param name="id">The ID of the save house.</param>
        /// <param name="Switch">Turn on or off the save house.</param>
        public static void SwitchSaveHouse(int id, bool Switch)
        {
            Function.Call("ENABLE_SAVE_HOUSE", new Parameter[]{
                id, Switch
            });
        }

        /// <summary>
        /// This function forces the whole screen to go black and show the "LOADING..." text on the bottom right corner of the screen. 
        /// The player is frozen and the game can't be paused while it's on. 
        /// </summary>
        /// <param name="Switch"></param>
        [Obsolete("This will make the game freeze and cannot be played.", true)]
        public static void ForceLoadingScreen(bool Switch)
        {
            int ISwitch = Common.ConvertBoolToInt(Switch);
            Function.Call("FORCE_LOADING_SCREEN", new Parameter[]{
                ISwitch
            });
        }

        public static void OpenGarage(string name)
        {
            Function.Call("OPEN_GARAGE", new Parameter[]{
                name
            });
        }

        [Obsolete("Should use NGame.PrintSubtitle")]
        public static void PrintSubtitle(string text, int duration = 7000)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", text, duration });
        }

        public static void SetPedAlpha(Ped p, int value)
        {
            if (value > 255)
            {
                value = 255;
            }
            else if (value < 0)
            {
                value = 0;
            }
            Function.Call("SET_PED_ALPHA", p, value);
        }

        public static int ConvertPlayerIDToIndex(int ID)
        {
            int result = Function.Call<int>("CONVERT_INT_TO_PLAYERINDEX", ID);
            return result;
        }
    }
}
