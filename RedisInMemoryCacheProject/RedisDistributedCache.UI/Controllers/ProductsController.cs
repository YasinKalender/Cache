using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisDistributedCache.UI.Models;

namespace RedisDistributedCache.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            DistributedCacheEntryOptions opt = new();
            opt.AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(20);

            _distributedCache.SetString("Products", "Product1", opt);

            ViewBag.data = _distributedCache.GetString("Products");

            return View();
        }

        public IActionResult RemoveCache()
        {
            _distributedCache.Remove("Products");

            return View();
        }
        public IActionResult ComplexTypeCache()
        {
            Product product = new() { Id = 1, Name = "Product" };
            var jsonObject = JsonConvert.SerializeObject(product);
            _distributedCache.SetString("Products1", jsonObject);

            var data = _distributedCache.GetString("Products1");
            var jsonObject1 = JsonConvert.DeserializeObject<Product>(data);
            ViewBag.data = jsonObject1;

            return View();
        }

        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/BesiktasJK-Logo.svg.png");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);
            _distributedCache.Set("Images", imageByte);

            return View();
        }

        public IActionResult ImageUrl()
        {
            var images = _distributedCache.Get("Images");

            return File(images, "image/png");
        }

    }
}
