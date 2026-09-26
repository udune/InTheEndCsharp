namespace InTheEndCsharp.어트리뷰트;

public class CustomAttribute생성
{
    public static void 실행()
    {
        
    }

    [MyCustom]
    class MyClass
    {
        [MyCustom]
        public void MyMethod()
        {
            
        }
    }

    class MyCustomAttribute : Attribute
    {
        
    }  
}