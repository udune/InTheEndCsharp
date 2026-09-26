namespace InTheEndCsharp.어트리뷰트.Attributes;

public interface ITransformerAttribute<T>
{
    T Transform(T value);
}