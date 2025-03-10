using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZetaScheduler.Core
{
    internal interface IPoolHandler<T> : IDisposable
    {
        Task ScheduleInInternalPool(T handler, DateTime time);
        
        Task ScheduleInInternalPoolRecurring(T handler, DateTime time, TimeSpan interval);
    }
}
