using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NativeFunctionHook
{
    public class Common
    {
        public static int ConvertBoolToInt(bool source)
        {
            switch (source)
            {
                case true:
                    return 0;
                case false:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
