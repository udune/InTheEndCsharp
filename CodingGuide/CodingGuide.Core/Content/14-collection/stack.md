---
id: stack
title: Stack<T> - 후입선출(LIFO)
category: 열거자와 컬렉션
order: 1411
summary: 마지막에 넣은 것이 먼저 나오는 Stack의 Push, Pop, Peek, TryPop 사용법과 활용 예(되돌리기, 괄호 검사)입니다.
keywords: 스택, Stack, 후입선출, LIFO, Push, Pop, Peek, TryPop, 되돌리기, undo, 뒤로가기, 괄호 검사, DFS
lesson: 컬렉션_Stack
related: queue, list-create
---
## 핵심
접시 쌓기와 같습니다. **마지막에 올린 것이 먼저 나옵니다.**

```csharp
var s = new Stack<string>();
s.Push("Apple");     // 넣기
s.Push("Banana");
s.Push("Orange");

Console.WriteLine(s.Pop());   // Orange (꺼내면서 제거)
Console.WriteLine(s.Pop());   // Banana
Console.WriteLine(s.Peek());  // Apple  (보기만)

if (s.TryPop(out string? item))
    Console.WriteLine(item);  // Apple
```

## 활용: 괄호 짝 검사
```csharp
bool IsBalanced(string text)
{
    var stack = new Stack<char>();
    foreach (char c in text)
    {
        if (c == '(') stack.Push(c);
        else if (c == ')')
        {
            if (!stack.TryPop(out _)) return false;
        }
    }
    return stack.Count == 0;
}
```

## 언제 쓰나요?
- 되돌리기(Undo), 브라우저 뒤로 가기
- 괄호/태그 짝 검사, 깊이 우선 탐색(DFS), 재귀를 반복문으로 바꿀 때
