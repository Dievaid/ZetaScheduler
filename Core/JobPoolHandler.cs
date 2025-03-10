using System;
using System.Threading;
using System.Threading.Tasks;
using ZetaScheduler.Events;

namespace ZetaScheduler.Core
{
    internal class JobPoolHandler : IPoolHandler<IJob>
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private event EventHandler<RecurringJobFinishedEventArgs> _recurringJobFinished;

        public JobPoolHandler()
        {
            _recurringJobFinished += OnRecurringJobFinished;
        }

        ~JobPoolHandler()
        {
            this.Dispose();
        }

        public Task ScheduleInInternalPool(IJob job, DateTime time)
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

        public Task ScheduleInInternalPoolRecurring(IJob job, DateTime time, TimeSpan interval)
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

        public void Dispose()
        {
            _recurringJobFinished -= OnRecurringJobFinished;
            _cancellationTokenSource.Cancel();
        }

        private void OnRecurringJobFinished(object sender, RecurringJobFinishedEventArgs e)
        {
            this.ScheduleInInternalPoolRecurring(e.Job, DateTime.Now.Add(e.Interval), e.Interval);
        }
    }
}
