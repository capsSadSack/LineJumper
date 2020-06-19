using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

public static class EnumsProcessor
{
    public static IEnumerable<string> GetAllDescriptions<T>(T anyValue) where T : IConvertible
    {
        if (anyValue is Enum)
        {
            Type type = anyValue.GetType();
            Array values = Enum.GetValues(type);

            List<string> output = new List<string>();

            foreach (var element in values)
            {
                output.Add(GetDescription((T)element));
            }

            return output;
        }

        return new List<string>();
    }

    public static IEnumerable<T> GetAllValues<T>(T anyValue) where T : IConvertible
    {
        if (anyValue is Enum)
        {
            Type type = anyValue.GetType();
            Array values = Enum.GetValues(type);

            List<T> output = new List<T>();

            foreach (var element in values)
            {
                output.Add((T)element);
            }

            return output;
        }

        return new List<T>();
    }

    public static string GetDescription<T>(T e) where T : IConvertible
    {
        if (e is Enum)
        {
            Type type = e.GetType();
            Array values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }
        }

        return null;
    }

    public static T GetValueFromDescription<T>(string description)
    {
        var type = typeof(T);
        if (!type.IsEnum)
        {
            throw new InvalidOperationException();
        }
        foreach (var field in type.GetFields())
        {
            var attribute = Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null)
            {
                if (attribute.Description == description)
                {
                    return (T)field.GetValue(null);
                }
            }
            else
            {
                if (field.Name == description)
                {
                    return (T)field.GetValue(null);
                }
            }
        }
        throw new ArgumentException("Not found.", nameof(description));
    }
}

