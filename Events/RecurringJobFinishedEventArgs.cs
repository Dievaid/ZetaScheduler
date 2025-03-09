using System;
using ZetaScheduler.Core;

namespace ZetaScheduler.Events
{
    internal class RecurringJobFinishedEventArgs : EventArgs
    {
        public IJob Job { get; set; }

        public TimeSpan Interval { get; set; }
    }
}
