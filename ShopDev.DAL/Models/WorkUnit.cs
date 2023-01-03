namespace ShopDev.DAL.Models;

public class WorkUnit
{
    public Guid Id { get; set; }
    public Guid Node { get; set; }
    public WorkUnitStatus Status { get; set; }
    public string ActionName { get; set; } = null!;
    public string? ActionData { get; set; }
    public string? ResponseData { get; set; }
}

public enum WorkUnitStatus
{
    None,
    Transmitted,
    Done,
    Failed
}