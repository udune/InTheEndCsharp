namespace InTheEndCsharp.클래스;

public class 생성자_매개변수
{
    public static void 실행()
    {
        Car car = new Car("현대", "소나타", "검정");
        Console.WriteLine();        
        
        car.ShowInfo();
    }

    public class Car
    {
        private string brand;
        private string model;
        private string color;

        public Car(string brand, string model, string color)
        {
            this.brand = brand;
            this.model = model;
            this.color = color;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"브랜드는 {brand}입니다.");
            Console.WriteLine($"모델은 {model}입니다.");
            Console.WriteLine($"컬러는 {color}입니다.");
        }
    }
}