using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AMDespachante.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDisplayName(this Enum value)
        {
            if (value is null) return null;

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return value.ToString();
        }
    }
}
