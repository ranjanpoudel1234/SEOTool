using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InfoTrack.Tools.Http.Repositories
{
    public static class SimpleRetryHelper
    {
        public static async Task RetryOnExceptionAsync(Func<Task> operation, ILogger logger, int totalAttemptsAllowed = 3, TimeSpan? pauseBetweenFailures = null)
        {
            pauseBetweenFailures ??= TimeSpan.FromSeconds(1);

            var currentAttempt = 0;
            do
            {
                try
                {
                    currentAttempt++;
                    await operation();
                    break;
                }
                catch (Exception ex)
                {
                    if (currentAttempt == totalAttemptsAllowed)
                        throw;

                    logger.LogError($"Exception caught on attempt {currentAttempt} - will retry after {pauseBetweenFailures} seconds", ex);

                    Task.Delay(pauseBetweenFailures.Value).Wait();
                }
            } while (true);
        }
    }
}
