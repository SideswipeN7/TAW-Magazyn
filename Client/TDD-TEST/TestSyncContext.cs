using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TDD_TEST
{
    public class TestSyncContext : SynchronizationContext
    {
        public event EventHandler NotifyCompleted;

        public override void Post(SendOrPostCallback d, object state)
        {
            d.Invoke(state);
            NotifyCompleted(this, System.EventArgs.Empty);
        }

        //other methods omitted for brevity
    }
}
