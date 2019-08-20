using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using GTA.Native;

namespace NativeFunctionHook
{
    public class NDisplay
    {
        /// <summary>
        /// Draws a the string to the screen at (x, y). 
        /// </summary>
        /// <param name="position">The X and Y pos.</param>
        /// <param name="text">String to print/draw.</param>
        public static void DisplayTextWithLiteralString(Vector2 position, string text)
        {
            Function.Call("DISPLAY_TEXT_WITH_LITERAL_STRING", new Parameter[]{
                position.X, position.Y, "STRING", text
            });
        }
    }
}
