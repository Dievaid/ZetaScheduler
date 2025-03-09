using System;

namespace ZetaScheduler.Core
{
    public interface IScheduler : IDisposable
    {
        /// <summary>
        /// Schedules a job to run at a specific time.
        /// </summary>
        /// <param name="job">The job to schedule.</param>
        /// <param name="time">The time to run the job.</param>
        void Schedule(IJob job, DateTime time);

        /// <summary>
        /// Schedules a job to run at a specific time and repeat at a specific interval.
        /// </summary>
        /// <param name="job">Job to be scheduled</param>
        /// <param name="time">Time of the first run</param>
        /// <param name="interval">Time interval for recurring job</param>
        void ScheduleRecurringJob(IJob job, DateTime time, TimeSpan interval);
    }
}
