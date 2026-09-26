namespace InTheEndCsharp.어트리뷰트.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class LeftAttribute : Attribute, ITransformerAttribute<string>
{
    public int Length { get; }
    
    public LeftAttribute(int length)
    {
        Length = length;
    }
    
    public string Transform(string value)
    {
        return value.Length > Length ? value.Substring(0, Length) : value;
    }
}