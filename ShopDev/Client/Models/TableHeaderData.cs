namespace ShopDev.Client.Models;

public class TableHeaderData
{
    public string? Content { get; set; }
    public SortType SortType { get; set; }
}

public enum SortType
{
    Unsortable,
    Unsorted,
    Ascending,
    Descending
}