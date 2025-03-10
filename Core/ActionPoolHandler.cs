using System;
using System.Threading.Tasks;

namespace ZetaScheduler.Core
{
    internal class ActionPoolHandler : IPoolHandler<Action>
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task ScheduleInInternalPool(Action handler, DateTime time)
        {
            throw new NotImplementedException();
        }

        public Task ScheduleInInternalPoolRecurring(Action handler, DateTime time, TimeSpan interval)
        {
            throw new NotImplementedException();
        }
    }
}
