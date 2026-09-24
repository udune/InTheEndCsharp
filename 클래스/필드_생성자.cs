namespace InTheEndCsharp.클래스;

public class 필드_생성자
{
    public static void 실행()
    {
        Car car = new Car();

        Console.WriteLine(car.brand);
        Console.WriteLine(car.model);
        Console.WriteLine(car.color);
    }

    class Car
    {
        public string brand;
        public string model;
        public string color;

        public Car()
        {
            brand = "현대";
            model = "소나타";
            color = "검정";
        }
    }
}