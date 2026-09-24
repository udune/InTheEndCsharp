namespace InTheEndCsharp.클래스;

public class 메서드
{
    public static void 실행()
    {
        Car car = new Car();

        car.ShowInfo();
        string brand = car.GetBrand();
        Console.WriteLine(brand);
    }

    class Car
    {
        private string brand;
        private string model;
        private string color;

        public Car()
        {
            brand = "현대";
            model = "소나타";
            color = "검정";
        }

        public void ShowInfo()
        {
            Console.WriteLine($"브랜드는 {brand}입니다.");
            Console.WriteLine($"모델은 {model}입니다.");
            Console.WriteLine($"컬러는 {color}입니다.");
        }

        public string GetBrand()
        {
            return brand;
        }
    }
}