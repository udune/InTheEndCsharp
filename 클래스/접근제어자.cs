namespace InTheEndCsharp.클래스;

public class 접근제어자
{
    public static void 실행()
    {
        Car car = new Car();
        // Console.WriteLine(car.brand);
    }

    class Car
    {
        string brand;
        string model;
        string color;

        public Car()
        {
            brand = "현대";
            model = "소나타";
            color = "검정";
        }
    }
}