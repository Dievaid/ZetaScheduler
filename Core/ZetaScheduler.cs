using System;

namespace ZetaScheduler.Core
{
    public class ZetaScheduler : IScheduler
    {
        private readonly IPoolHandler<IJob> _jobHandler = new JobPoolHandler();

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

        public void Dispose()
        {
            _jobHandler.Dispose();
        }
    }
}
