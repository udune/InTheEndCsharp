namespace InTheEndCsharp.값타입및선언;

public class Enum타입
{
    public static void 실행()
    {
        const int SUNDAY = 0;
        const int MONDAY = 1;
        const int TUESDAY = 2;
        const int WEDNESDAY = 3;
        const int THURSDAY = 4;
        const int FRIDAY = 5;
        const int SATURDAY = 6;
     
        Days days = Days.Sunday;

        if (days == Days.Sunday)
        {
            Console.WriteLine("It's sunday");
        }

        if (days == Days.Monday)
        {
            Console.WriteLine("It's monday");
        }
        
        Console.WriteLine(days);
    }
    
    enum Days
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6,
        Saturday = 7
    }
}