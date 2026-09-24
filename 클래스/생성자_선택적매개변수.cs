namespace InTheEndCsharp.클래스;

public class 생성자_선택적매개변수
{
    public static void 실행()
    {
        Car car = new Car("현대", "소나타");
        Console.WriteLine();        
        
        car.ShowInfo(displayBrand: true, displayModel: true, displayColor: true);
    }

    public class Car
    {
        private string brand;
        private string model;
        private string color;

        public Car(string brand, string model, string color = "파랑")
        {
            this.brand = brand;
            this.model = model;
            this.color = color;
        }

        public void ShowInfo(bool displayBrand = true, bool displayModel = true, bool displayColor = true)
        {
            if (displayBrand)
                Console.WriteLine($"브랜드는 {brand}입니다.");
            
            if (displayModel)
                Console.WriteLine($"모델은 {model}입니다.");
            
            if (displayColor)
                Console.WriteLine($"컬러는 {color}입니다.");
        }
    }
}