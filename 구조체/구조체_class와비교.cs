using System.Drawing;

namespace InTheEndCsharp.구조체;

public class 구조체_class와비교
{
    public static void 실행()
    {
        Point point = new Point { X = 10, Y = 20 };
        Console.WriteLine($"{point.X},{point.Y}");

        void ChangePoint(Point point)
        {
            point.X = 100;
            point.Y = 200;
        }
        
        ChangePoint(point);
        Console.WriteLine($"{point.X},{point.Y}");
    }

    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}