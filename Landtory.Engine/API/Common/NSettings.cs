using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Landtory.Engine.API.Common
{
    public class NSettings
    {
        public int CalloutInterval { get; set; }
        public bool CalloutEnabled { get; set; }
        public bool EnableDangerCallouts { get; set; }
        public bool UsesFastMethodToArrestPed { get; set; }
    }
}
