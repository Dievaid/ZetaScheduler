using System;

namespace ZetaScheduler.Events
{
    internal class RecurringActionFinishedEventArgs : EventArgs
    {
        public Action Task { get; set; }

        public TimeSpan Interval { get; set; }
    }
}
