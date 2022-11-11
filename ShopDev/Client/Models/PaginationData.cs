namespace ShopDev.Client.Models;

public class PaginationData
{
    public int Current { get; set; }
    public int PageCount { get; set; }
    public int ElementsPerPage { get; set; }
    
    public bool CanNavigateForwards => Current != PageCount;
    public bool CanNavigateBackwords => Current != 0;
}