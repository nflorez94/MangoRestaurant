using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public class ProductService : BaseService, IProductServices
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType=SD.ApiType.POST,
                Data = product,
                Url=SD.ProductAPIBase+"/api/products",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsyn<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/products/{id}?productId=" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + $"/api/products/{id}?productId=" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsyn<T>(ProductDto product, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = product,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }
    }
}
