using System;
using ZetaScheduler.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ZetaScheduler.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddZetaScheduler(this IServiceCollection services)
        {
            services.AddSingleton<IScheduler, Core.ZetaScheduler>();
            return services;
        }

        public static IServiceProvider WithRecurringJobAsOfNowEvery(
            this IServiceProvider provider,
            IJob job,
            TimeSpan interval)
        {
            IScheduler scheduler = provider.GetService<IScheduler>();
            scheduler.ScheduleRecurringJob(job, DateTime.Now, interval);
            return provider;
        }

        public static IServiceProvider WithRecurringJobStartingFromEvery(
            this IServiceProvider provider,
            IJob job,
            DateTime start,
            TimeSpan interval)
        {
            IScheduler scheduler = provider.GetService<IScheduler>();
            scheduler.ScheduleRecurringJob(job, start, interval);
            return provider;
        }
    }
}
