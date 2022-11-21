namespace ShopDev.APIModels;

public class GenericRequest<T> : RequestBase
{
    public T? Value { get; set; }
}