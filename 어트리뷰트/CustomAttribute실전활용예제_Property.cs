using System.Reflection;
using InTheEndCsharp.어트리뷰트.Extensions;
using InTheEndCsharp.어트리뷰트.Models;

namespace InTheEndCsharp.어트리뷰트;

public class CustomAttribute실전활용예제_Property
{
    public static void 실행()
    {
        User user = new User
        {
            Email = "Test@tEsT.Com",
            Name = "KaBurI",
            Address = "서울 종로구 세종대로"
        };

        Console.WriteLine(user);
        Console.WriteLine("-----------");

        user.ApplyAttributes();
        
        Console.WriteLine(user);
    }
}