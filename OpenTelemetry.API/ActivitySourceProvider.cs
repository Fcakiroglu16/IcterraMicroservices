using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTelemetryApp
{
    internal class ActivitySourceProvider
    {
        public static ActivitySource ActivitySource = new ActivitySource("ActivitySourceToConsole");

      
    }
}
