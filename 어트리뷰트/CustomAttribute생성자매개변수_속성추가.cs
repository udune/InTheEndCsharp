using System.Runtime.CompilerServices;

namespace InTheEndCsharp.어트리뷰트;

public class CustomAttribute생성자매개변수_속성추가
{
    public static void 실행()
    {
        
    }
    
    [MyCustom(name: "custom", Description="내용")]
    class MyClass
    {
        [MyCustom(name: "custom", Description="내용")]
        private string? TestProperty { get; set; }
        
        [MyCustom(name: "custom", Description="내용")]
        public void MyMethod()
        {
            
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
    class MyCustomAttribute : Attribute
    {
        public string Description { get; set; }
        
        public MyCustomAttribute()
        {
            
        }
        
        public MyCustomAttribute(string name)
        {
            
        }
    }  
}