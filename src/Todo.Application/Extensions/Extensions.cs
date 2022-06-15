using FluentValidation.Results;

namespace Todo.Application.Extensions
{
    public static class Extensions
    {
        public static void UpdateAll<T>(this List<T> source, IEnumerable<Action<T>> actions)
        {
            foreach (var item in source)
                foreach (var action in actions)
                {
                    action(item);
                }
        }

        public static void UpdateAll<T>(this List<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static string BuildNotifications(this ValidationResult result)
            => string.Join(" | ", result.Errors.Select(x => x.ErrorMessage));
    }

}