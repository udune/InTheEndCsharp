namespace InTheEndCsharp.어트리뷰트;

public class CustomAttribute동적제어
{
    public static void 실행()
    {
        
    }
    
    [MyCustom(name: "custom", Description="내용")]
    [MyCustom(name: "custom", Description="내용")]
    class MyClass
    {
        private string? TestProperty { get; set; }
        
        public void MyMethod()
        {
            
        }
    }

    class MyCustom2 : MyClass
    {
        
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
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