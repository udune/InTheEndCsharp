namespace InTheEndCsharp.클래스;

public class 메서드_가변매개변수
{
    public static void 실행()
    {
        Car car = new Car("현대", "소나타");
        Console.WriteLine();        
        
        car.ShowInfo("brand", "model", "color");
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

        public void ShowInfo(params string[] options)
        {
            foreach (string option in options)
            {
                Console.WriteLine(option);

                if (option == "brand")
                    Console.WriteLine($"브랜드는 {brand}입니다.");

                if (option == "model")
                    Console.WriteLine($"모델은 {model}입니다.");

                if (option == "color")
                    Console.WriteLine($"컬러는 {color}입니다.");
            }

        }
    }
}