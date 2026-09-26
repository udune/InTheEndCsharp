using System.Reflection;
using InTheEndCsharp.어트리뷰트.Attributes;
using InTheEndCsharp.어트리뷰트.Models;

namespace InTheEndCsharp.어트리뷰트;

public class CustomAttribute활용예제_메타정보읽어오기
{
    public static void 실행()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        
        AttributeReader.ReadInfoAttributes(typeof(User), (attribute) => attribute.Version == "1.0.1");
        AttributeReader.ReadInfoAttributes([typeof(User), typeof(Cart)], (attribute) => attribute.Version == "1.0.0");
        AttributeReader.ReadInfoAttributes([typeof(User), typeof(Cart)], (attribute) => attribute.Author == "Kaburi");
        AttributeReader.ReadInfoAttributes(types, (attribute) => attribute.Author == "Kaburi");
        AttributeReader.ReadInfoAttributes(types, (attribute) => true);
        AttributeReader.ReadInfoAttributes(types, (attribute) => attribute.Date == "2024-12-27");
    }
}