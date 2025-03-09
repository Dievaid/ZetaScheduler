using System;
using System.Threading;
using System.Threading.Tasks;
using ZetaScheduler.Events;

namespace ZetaScheduler.Core
{
    public class ZetaScheduler : IScheduler
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private event EventHandler<RecurringJobFinishedEventArgs> _recurringJobFinished;

        public ZetaScheduler()
        {
            _recurringJobFinished += OnRecurringJobFinished;
        }

        ~ZetaScheduler()
        {
            this.Dispose();
        }

        public void Schedule(IJob job, DateTime time)
        {
            this.ScheduleInInternalPool(job, time);
        }

        public void ScheduleRecurringJob(IJob job, DateTime time, TimeSpan interval)
        {
            this.ScheduleInInternalPoolRecurring(job, time, interval);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _recurringJobFinished -= OnRecurringJobFinished;
        }

        private Task ScheduleInInternalPool(IJob job, DateTime time)
        {
            return Task.Run(async () =>
            {
                TimeSpan timeDifference = time - DateTime.Now;

                int timeMillis = Convert.ToInt32(timeDifference.TotalMilliseconds);
                if (timeMillis > 0)
                {
                    await Task.Delay(timeMillis, _cancellationTokenSource.Token);
                }

                job.Execute();

            }, cancellationToken: _cancellationTokenSource.Token);
        }

        private Task ScheduleInInternalPoolRecurring(IJob job, DateTime time, TimeSpan interval)
        {
            return this.ScheduleInInternalPool(job, time)
                .ContinueWith(_ =>
                {
                    _recurringJobFinished?.Invoke(this, new RecurringJobFinishedEventArgs
                    {
                        Job = job,
                        Interval = interval
                    });
                }, cancellationToken: _cancellationTokenSource.Token);
        }

        private void OnRecurringJobFinished(object sender, RecurringJobFinishedEventArgs e)
        {
            this.ScheduleInInternalPoolRecurring(e.Job, DateTime.Now.Add(e.Interval), e.Interval);
        }
    }
}
