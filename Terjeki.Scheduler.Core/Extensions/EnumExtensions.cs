using System.Reflection;
namespace Terjeki.Scheduler.Core
{
    public static class EnumExtensions
    {
        public static List<string> GetEnumDescriptions<T>()
        {
            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType).Cast<System.Enum>();

            var descriptions = enumValues
                .Select(e => GetDescription(e))
                .ToList();

            return descriptions;
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null) return string.Empty;
            Type type = value.GetType();
            var name = Enum.GetName(type, value);
            // if(name == null ) return string.Empty;
            return type.GetField(name).GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;

        }

        public static T[] GetEnumValues<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("GetValues<T> can only be called for types derived from System.Enum", "T");
            }
            return (T[])Enum.GetValues(typeof(T));
        }

        public static bool TryGetEnumFromText<T>(this T enumType, string text, out T? result) where T : Enum
        {
            foreach (var e in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (!string.Equals(e.GetDisplayName(), text, StringComparison.OrdinalIgnoreCase)) continue;
                result = e;
                return true;
            }

            result = default;
            return false;
        }

        public static IEnumerable<string> GetDisplayNames<T>(this T enumType) where T : Enum
            => Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => e.GetDisplayName());

        public static string GetDisplayName(this Enum value)
            => value.GetType()
                .GetMember(value.ToString())[0]
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? value.ToString();
    }
}
