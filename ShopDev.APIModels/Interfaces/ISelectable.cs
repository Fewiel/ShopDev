namespace ShopDev.APIModels.Interfaces;

public interface ISelectable { }

public interface ISelectable<T> : ISelectable
{
    string Text { get; }
    T Value { get; }
}