namespace ShopDev.Client.Models;

public class TableData<T>
{
    public List<TableHeaderData> Headers { get; }
    public List<T> Data { get; }
    public PaginationData? Pagination { get; set; }

    public TableData(List<TableHeaderData> headers, List<T> data)
    {
        Headers = headers;
        Data = data;
    }
}