using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductServices : IBaseService
    {
        Task<T> GetAllProductsAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int id, string token);
        Task<T> CreateProductAsync<T>(ProductDto product, string token);
        Task<T> UpdateProductAsyn<T>(ProductDto product, string token);
        Task<T> DeleteProductAsyn<T>(int id, string token);

    }
}
