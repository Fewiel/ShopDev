namespace ShopDev.APIModels;

public class GenericRepsonse<T> : ResponseBase<GenericRepsonse<T>>
{
    public T? Value { get; set; }

    public GenericRepsonse<T> WithValue(T value)
    {
        Value = value;
        return this;
    }
}