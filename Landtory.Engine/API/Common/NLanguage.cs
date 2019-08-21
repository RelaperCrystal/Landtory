using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using Landtory.Engine;

namespace Landtory.Engine.API.Common
{
    public class NLanguage
    {
        public static string GetLangStr(string name)
        {
            
            try
            {
                return Language.ResourceManager.GetString(name);
            }
            catch(MissingManifestResourceException)
            {
                Logger log = new Logger();
                log.Log("Language String dosen't seem to be exist: "+name, "LanguageManager");
                return name;
            }
            catch(Exception)
            {
                Logger log = new Logger();
                log.Log("Failed to acquire Language String: "+name,"LanguageManager");
                return name;
            }
        }
    }
}
