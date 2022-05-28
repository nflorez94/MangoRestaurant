using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>(token);
            if (response != null && response.IsSuccess)
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            return View(list);
        }

        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDto model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(model,token);
                if (response != null && response.IsSuccess)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId,token);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsyn<ResponseDto>(model,token);
                if (response != null && response.IsSuccess)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId,token);
            if (response != null && response.IsSuccess && response.Result != null)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductAsyn<ResponseDto>(model.ProductId,token);
            if (response.IsSuccess)
                return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}
