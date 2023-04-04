using InMemory.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.UI.Controllers
{
    public class InMemoryController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public InMemoryController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            var cacheValue = String.Empty;

            //if (!string.IsNullOrEmpty(_memoryCache.Get<string>("Datetime")))
            //{
            //    cacheValue = _memoryCache.Set<string>("Datetime", DateTime.Now.ToString());
            //}

            if (!_memoryCache.TryGetValue("Datetime", out string datetime))
            {
                MemoryCacheEntryOptions options = new();
                options.AbsoluteExpiration = DateTime.Now.AddSeconds(30);
                options.SlidingExpiration = TimeSpan.FromSeconds(10);
                options.Priority = CacheItemPriority.Normal;

                options.RegisterPostEvictionCallback((key, value, reason, state) =>
                {

                    _memoryCache.Set("callback", $"{key}-{value}-{reason}-{state}");

                });

                cacheValue = _memoryCache.Set<string>("Datetime", DateTime.Now.ToString(), options);
            }

            cacheValue = _memoryCache.Get<string>("Datetime");

            TempData["cacheValue"] = cacheValue;


            Student student = new() { Id = 1, FirstName = "Yasin" };

            var studentCahce = _memoryCache.Set<Student>("student", student);

            ViewBag.Student = studentCahce;


            return View();
        }

        public IActionResult DeleteCache()
        {
            //_memoryCache.Remove("Datetime");

            //TempData["cacheValue"] = _memoryCache.Get<string>("Datetime");


            //_memoryCache.GetOrCreate<string>("Datetime", func =>
            //{

            //    return DateTime.Now.ToString();
            //});

            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.callback = callback;


            return View();
        }
    }
}
