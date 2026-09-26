using System.Reflection;
using InTheEndCsharp.어트리뷰트.Attributes;

namespace InTheEndCsharp.어트리뷰트.Extensions;

public static class AttributeExtensions
{
    public static void ApplyAttributes(this object obj)
    {
        var properties = obj.GetType().GetProperties();
        foreach (var property in properties)
        {
            var attributes = property.GetCustomAttributes().OfType<ITransformerAttribute<string>>();
            foreach (var attribute in attributes)
            {
                string? value = property.GetValue(obj) as string;

                if (value != null)
                {
                    property.SetValue(obj, attribute.Transform(value));
                }
            }
        }
    }
}