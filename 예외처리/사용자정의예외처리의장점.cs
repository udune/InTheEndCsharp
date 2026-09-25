namespace InTheEndCsharp.예외처리;

public class 사용자정의예외처리의장점
{
    public static void 실행()
    {
        void 출금Exception()
        {
            잔액확인1Exception();
        }
        
        void 잔액확인1Exception()
        {
            잔액확인2Exception();
        }

        void 잔액확인2Exception()
        {
            잔액확인3Exception();
        }

        void 잔액확인3Exception()
        {
            throw new 잔액부족Exception("잔액이 부족합니다.", 500, 1000);
        }
        
        // void 출금(out 잔액부족 부족)
        // {
        //     잔액확인1(out 부족);
        // }
        //
        // void 잔액확인1(out 잔액부족 부족)
        // {
        //     잔액확인2(out 부족);
        // }
        //
        // void 잔액확인2(out 잔액부족 부족)
        // {
        //     잔액확인3(out 부족);
        // }
        //
        // void 잔액확인3(out 잔액부족 부족)
        // {
        //     부족 = new 잔액부족() { 잔액 = 5000, 출금액 = 10000 };
        // }
        //
        // 출금(out 잔액부족 부족);

        try
        {
            출금Exception();
        }
        catch (잔액부족Exception e)
        {
            Console.WriteLine($"Message: {e.Message}, 잔액: {e.잔액}, 출금액: {e.출금액}");
        }

        // Console.WriteLine($"잔액: {부족.잔액} 출금: {부족.출금액}");
    }

    class 잔액부족
    {
        public Decimal 잔액 { get; set; }
        public Decimal 출금액 { get; set; }
    }

    class 잔액부족Exception : Exception
    {
        public Decimal 잔액 { get; set; }
        public Decimal 출금액 { get; set; }
        
        public 잔액부족Exception(string message, decimal 잔액, decimal 출금액) : base(message)
        {
            this.잔액 = 잔액;
            this.출금액 = 출금액;
        }
    }
}