using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.Helpers
{
    public class AppiumServers
    {
        public static Uri localURI = new Uri("http://127.0.0.1:4723/wd/hub");
        public static Uri serverURI = new Uri("http://0.0.0.0:4723/wd/hub");
    }
}
