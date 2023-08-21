namespace NetPlayground.Tests;

public class Item : IKey<int>
{
    public int Id;

    int IKey<int>.Key => Id;
}

public interface IKey<T>
{
    T Key { get; }
}