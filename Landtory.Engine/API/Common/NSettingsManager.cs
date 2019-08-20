using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Landtory.Engine.API.Common
{
    public class NSettingsManager
    {
        static string PublicStr = File.ReadAllText("Landtory\\LandtorySettings.json");
    }
}
