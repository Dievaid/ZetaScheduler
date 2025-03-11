using System;
using System.Threading;
using System.Threading.Tasks;
using ZetaScheduler.Events;

namespace ZetaScheduler.Core
{
    internal class ActionPoolHandler : IPoolHandler<Action>
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private event EventHandler<RecurringActionFinishedEventArgs> _recurringActionFinished;

        public ActionPoolHandler()
        {
            _recurringActionFinished += this.OnRecurringActionFinished;
        }

        ~ActionPoolHandler()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            _recurringActionFinished -= this.OnRecurringActionFinished;
            _cancellationTokenSource.Cancel();
        }

        public Task ScheduleInInternalPool(Action handler, DateTime time)
        {
            return Task.Run(async () =>
            {
                TimeSpan timeDifference = time - DateTime.Now;

                int timeMillis = Convert.ToInt32(timeDifference.TotalMilliseconds);
                if (timeMillis > 0)
                {
                    await Task.Delay(timeMillis, _cancellationTokenSource.Token);
                }

                await Task.Run(handler, cancellationToken: _cancellationTokenSource.Token);
            }, cancellationToken: _cancellationTokenSource.Token);
        }

        public Task ScheduleInInternalPoolRecurring(Action handler, DateTime time, TimeSpan interval)
        {
            return this.ScheduleInInternalPool(handler, time)
                .ContinueWith(_ =>
                {
                    _recurringActionFinished?.Invoke(this, new RecurringActionFinishedEventArgs
                    {
                        Task = handler,
                        Interval = interval
                    });
                }, cancellationToken: _cancellationTokenSource.Token);
        }

        private void OnRecurringActionFinished(object sender, RecurringActionFinishedEventArgs e)
        {
            this.ScheduleInInternalPoolRecurring(e.Task, DateTime.Now.Add(e.Interval), e.Interval);
        }
    }
}
