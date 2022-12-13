namespace Logiwa.Repository.Abstract
{
    public interface ICategoryRepository
    {
        Task<bool> IsValid(int categoryId);
        Task<int> GetMinimumStockQuantity(int categoryId);
    }
}
