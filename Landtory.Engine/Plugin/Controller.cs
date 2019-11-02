using Landtory.Engine.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Landtory.Engine.Plugin
{
    public class Controller
    {
        public static ArrayList plugins = new ArrayList();
        static Logger logger = new Logger();
        public static void LoadAllPlugins()
        {
            try
            {
                if (!Directory.Exists("Landtory\\plugins\\"))
                {
                    logger.Log("Plugin folder not exist. Program will stop load plugins", "Plugin Controller", Logger.LogLevel.Warning);
                    return;
                }

                string[] pluginFiles = Directory.GetFiles("Landtory\\plugins\\");
                foreach (string file in pluginFiles)
                {
                    if (Path.GetExtension(file).ToLower() == "dll")
                    {
                        Assembly pluginAssembly = Assembly.LoadFile(file);
                        Type[] types = pluginAssembly.GetTypes();
                        foreach (Type type in types)
                        {
                            if (type.GetInterface("IPlugin") != null)
                            {
                                plugins.Add(pluginAssembly.CreateInstance(type.FullName));
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Log("Error when loading plugins. No plugin will be loaded.", "Plugin Controller", Logger.LogLevel.Error);
                logger.Log("Error Details: \r\n" + ex.ToString(), "Plugin Controller", Logger.LogLevel.Error);
            }
        }

        public static void ExecuteAllPluginMethod(string name)
        {
            foreach(object obj in plugins)
            {
                Type objType = obj.GetType();
                MethodInfo OnInitialized = objType.GetMethod(name);
                OnInitialized.Invoke(obj, null);
            }
        }
    }
}
