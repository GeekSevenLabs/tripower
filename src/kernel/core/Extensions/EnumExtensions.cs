using System.ComponentModel.DataAnnotations;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace TriPower;

public static class EnumExtensions
{
    extension<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        public string DisplayName => GetDisplayNameInternal(value);
        public string DisplayShortName => GetDisplayShortNameInternal(value);
    }

    extension<TEnum>(TEnum) where TEnum : struct, Enum
    {
        public static TEnum[] Values => Enum.GetValues<TEnum>();
    }
    
    
    private static string GetDisplayNameInternal<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attribute = fieldInfo?.GetCustomAttribute(typeof(DisplayAttribute), false);
        var name = attribute is DisplayAttribute displayAttribute ? displayAttribute.Name : null;
        return name?.IsNotEmptyOrWhiteSpace is true ? name : value.ToString();
    }
    
    private static string GetDisplayShortNameInternal<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attribute = fieldInfo?.GetCustomAttribute(typeof(DisplayAttribute), false);
        var shortName = attribute is DisplayAttribute displayAttribute ? displayAttribute.ShortName : null;
        return shortName?.IsNotEmptyOrWhiteSpace is true ? shortName : value.ToString();
    }
    
}