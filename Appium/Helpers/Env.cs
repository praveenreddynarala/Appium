using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Appium.Helpers
{
    public class Env
    {
        public static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(360);
		public static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(5);
		//public static string ASSETS_ROOT_DIR = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "../../assets");
        public static string ASSETS_ROOT_DIR = Path.GetFullPath(Environment.CurrentDirectory);

		private static Dictionary<string, string> env;
		private static bool initialized = false;
		private static void Init() {
			try {
				if(!initialized) 
				{
					initialized = true;
					string path = AppDomain.CurrentDomain.BaseDirectory + "../../";
					StreamReader sr = new StreamReader(path + "env.json");
					string jsonString = sr.ReadToEnd();
					JavaScriptSerializer ser = new JavaScriptSerializer();
					env = ser.Deserialize<Dictionary<string, string>>(jsonString);
				}
			} catch {
				env = new Dictionary<string, string> ();
			}
		}

		private static bool isTrue(string val) {
			if (val != null) {
				val = val.ToLower ().Trim ();
			}
			//return (val == "true") || (val == "1") || (val == Environment.GetEnvironmentVariable("Computername").ToLower());
            return (val == "true") || (val == "1");
		}

		static public bool isLocal() {
			Init ();
            return (env.ContainsKey("Local") && isTrue(env["Local"])) || isTrue( Environment.GetEnvironmentVariable("LocalComputername") );
		}

        static public bool isServer()
        {
            Init();
            return (env.ContainsKey("Server") && isTrue(env["Server"])) || isTrue(Environment.GetEnvironmentVariable("Computername"));
        }

		static public string getEnvVar(string name){
			if (env.ContainsKey(name) && (env [name] != null)) {
				return env [name];
			} else {
				return Environment.GetEnvironmentVariable (name);
			}
		}
	}
}
