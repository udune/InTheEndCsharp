using InTheEndCsharp.값타입및선언;
using InTheEndCsharp.구조체;
using InTheEndCsharp.문자열;
using InTheEndCsharp.배열;
using InTheEndCsharp.연산자;
using InTheEndCsharp.인터페이스;
using InTheEndCsharp.조건문;
using InTheEndCsharp.클래스;
using InTheEndCsharp.타입캐스팅;

const string line = "==============================";

Action[] practiceArray =
[
    // 정수형타입.실행(),
    // 소수형타입.실행(),
    // bool_char타입.실행(),
    // Enum타입.실행(),
    // 명시적_암시적선언.실행(),

    // 비교연산자.실행(),
    // 산술연산자.실행(),
    // 할당연산자.실행(),
    // 논리연산자.실행(),
    // 비트연산자.실행(),
    // Null병합연산자.실행(),

    // if_삼항연산자.실행(),
    // switch문.실행(),

    // 배열선언및초기화.실행(),
    // 배열요소접근.실행(),
    // 반복문.실행(),
    // 다차원배열_Array클래스.실행(),

    // 문자열.실행(),
    
    // 메서드.실행,
    // 메서드_매개변수.실행,
    // 메서드_가변매개변수.실행,
    // 접근제어자.실행,
    // 정의_객체생성.실행,
    // 필드_생성자.실행,
    // 생성자_매개변수.실행,
    // 생성자_선택적매개변수.실행,
    // 메서드_매개변수의특징.실행,
    // 메서드_ref키워드.실행,
    // 메서드_out키워드.실행,
    // 속성_선언.실행,
    // 속성_getter.실행,
    // 속성_setter.실행,
    // 속성_setter접근제어자.실행,
    // 소멸자.실행,
    // const_readonly.실행,
    // 정적타입_static.실행,
    // 정적클래스_확장함수.실행,
    // 상속.실행,
    // 상속_재정의.실행,
    // 상속_재정의_override_new차이.실행,
    // 상속_접근제어자.실행,
    // 상속_추상클래스.실행,
    // 상속_상속체인.실행,
    // 상속_sealed.실행
    
    // 인터페이스.실행,
    // 인터페이스_다중구현.실행,
    // 인터페이스_명시적구현.실행,
    // 인터페이스_디폴트구현.실행
    
    // 타입명시적변환.실행,
    // 오브젝트_박싱_언박싱.실행,
    // 타입변환.실행,
    // 타입변환_ConvertClass.실행,
    // 타입변환_is.실행
    
    구조체_class와비교.실행
];

foreach (var practice in practiceArray)
{
    Console.WriteLine($"\n{line} {practice.Method.DeclaringType?.Name} {line}");
    practice();
}