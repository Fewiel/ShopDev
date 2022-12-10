using ShopDev.APIModels.Interfaces;

namespace ShopDev.APIModels.Models;

public class Role : ISelectable<Guid>
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public string Text => Name ?? "";

    public Guid Value => Id;
}