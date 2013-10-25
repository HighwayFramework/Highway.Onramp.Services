// [[Highway.Onramp.Services]]
// Write your service here, everything in here other than
// the having to implement IHostedService demo code.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using System.Timers;
using Templates.App_Architecture.Services;

namespace Templates
{
    public class HostedService : IHostedService, IDisposable
    {
        private readonly Timer timer;

        public HostedService()
        {
            Logger = NullLogger.Instance;

            timer = new Timer(1000) { AutoReset = true };
            timer.Elapsed += OnTimerFired;
        }

        private void OnTimerFired(object sender, ElapsedEventArgs eventArgs)
        {
            Logger.Info("Tick Tock goes the clock");
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public ILogger Logger { get; set; }

        public void Dispose()
        {
            if (timer != null)
                timer.Dispose();
        }
    }
}
