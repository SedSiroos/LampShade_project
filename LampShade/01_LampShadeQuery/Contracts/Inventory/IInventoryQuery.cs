namespace _01_LampShadeQuery.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStatus(IsInStock command); 
    }
}
