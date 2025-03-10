using System;

namespace ZetaScheduler.Core
{
    public class ZetaScheduler : IScheduler
    {
        private readonly IPoolHandler<IJob> _jobHandler = new JobPoolHandler();
        private readonly IPoolHandler<Action> _actionHandler = new ActionPoolHandler();

        ~ZetaScheduler()
        {
            this.Dispose();
        }

        public void Schedule(IJob job, DateTime time)
        {
            _jobHandler.ScheduleInInternalPool(job, time);
        }

        public void ScheduleRecurringJob(IJob job, DateTime time, TimeSpan interval)
        {
            _jobHandler.ScheduleInInternalPoolRecurring(job, time, interval);
        }

        public void Schedule(Action action, DateTime time)
        {
            _actionHandler.ScheduleInInternalPool(action, time);
        }

        public void ScheduleRecurringAction(Action action, DateTime time, TimeSpan interval)
        {
            _actionHandler.ScheduleInInternalPoolRecurring(action, time, interval);
        }

        public void Dispose()
        {
            _jobHandler.Dispose();
        }
    }
}
