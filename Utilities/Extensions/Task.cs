using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utilities.Extensions
{
    public static class TaskEx
    {
        public static IEnumerable<Task> ConfigureAwait(this IEnumerable<Task> tasks, bool continueOnCapturedContext)
        {
            foreach (var task in tasks)
            {
                task.ConfigureAwait(continueOnCapturedContext);
            }
            return tasks;
        }

        public static IEnumerable<Task<T>> ConfigureAwait<T>(this IEnumerable<Task<T>> tasks, bool continueOnCapturedContext)
        {
            foreach (var task in tasks)
            {
                task.ConfigureAwait(continueOnCapturedContext);
            }
            return tasks;
        }
    }
}
