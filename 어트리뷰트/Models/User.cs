using InTheEndCsharp.어트리뷰트.Attributes;

namespace InTheEndCsharp.어트리뷰트.Models;

[Info("Kaburi", "1.0.0", "2024-12-26", Description = "사용자 모델 클래스 생성")]
public class User
{
    [ToUpper] 
    public string Email { get; set; } = "";
    
    [ToLower]
    public string Name { get; set; } = "";
    
    [Left(5)]
    public string Address { get; set; } = "";

    public override string ToString()
    {
        return $"Email: {Email}, Name: {Name}, Address: {Address}";
    }
}