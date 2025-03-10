using System;

namespace ZetaScheduler.Events
{
    internal class RecurringTaskFinishedEventArgs : EventArgs
    {
        public Action Task { get; set; }

        public TimeSpan Interval { get; set; }
    }
}
