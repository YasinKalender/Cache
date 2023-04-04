using Microsoft.AspNetCore.Mvc;
using SampleProjectRedis.Models;
using SampleProjectRedis.Services;

namespace SampleProjectRedis.Controllers
{
    public class CacheController : Controller
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<IActionResult> Index()
        {
            var data = GetProducts();


            return View(data);
        }

        private List<Product> GetProducts()
        {
            return _cacheService.GetOrAdd("products", () => { return ProductList.GetAllProduct(); });
        }
    }
}
