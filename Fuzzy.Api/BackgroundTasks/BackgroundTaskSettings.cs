using System;

namespace Fuzzy.Api.BackgroundTasks
{
    public class BackgroundTaskSettings
    {
        public BackgroundTaskSettings()
        {
            CheckUpdateTime = 5;
            SubscriptionClientName = "BackgroundTasks";
            ConsumerId = GetGuid();
        }

        public string ConsumerId { get; }

        public int GracePeriodTime { get; }

        public int CheckUpdateTime { get; }

        public string SubscriptionClientName { get; }

        private string GetGuid() => Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
    }
}
