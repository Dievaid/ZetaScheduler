using System;
using System.Threading.Tasks;

namespace ZetaScheduler.Core
{
    internal interface IPoolHandler<T> : IDisposable
    {
        /// <summary>
        /// Schedule a handler to run at a specific time.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task ScheduleInInternalPool(T handler, DateTime time);

        /// <summary>
        /// Schedule a handler to run at a specific time and repeat at a specific interval.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="time"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        Task ScheduleInInternalPoolRecurring(T handler, DateTime time, TimeSpan interval);
    }
}
