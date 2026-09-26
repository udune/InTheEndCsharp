namespace InTheEndCsharp.어트리뷰트.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class ToUpperAttribute : Attribute, ITransformerAttribute<string>
{
    public string Transform(string value)
    {
        return value.ToUpper();
    }
}