namespace ShopDev.NodeModels.Models;

public abstract class Unit
{
    public abstract string UnitName { get; set; }
    public abstract void NodeExecute();
}