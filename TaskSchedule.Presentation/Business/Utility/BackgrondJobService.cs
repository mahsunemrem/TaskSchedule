using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskSchedule.Presentation.Business.Utility
{
    public static class BackgrondJobService
    {
        [Obsolete]
        public static void FireAndForgetJobs(Expression<Action> action)
        {
            Hangfire.BackgroundJob.Enqueue(action);
        }

        [Obsolete]
        public static void DelayedJobs(Expression<Action> action, TimeSpan delayedTimeSecond)
        {
            Hangfire.BackgroundJob.Schedule(action, delayedTimeSecond);
        }

        [Obsolete]
        public static void RecurrringJobs(Expression<Action> action , string jobName)
        {
            RecurringJob.RemoveIfExists(jobName);
            RecurringJob.AddOrUpdate(jobName, action,Cron.Minutely, TimeZoneInfo.Local);
                
        }
    }
}
