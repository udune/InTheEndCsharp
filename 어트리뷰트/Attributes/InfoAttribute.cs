namespace InTheEndCsharp.어트리뷰트.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class InfoAttribute : Attribute
{
    public string Description { get; set; }
    public string Author { get; }
    public string Version { get; }
    public string Date { get; }
    
    public InfoAttribute(string author, string version, string date)
    {
        Author = author;
        Version = version;
        Date = date;
    }
}