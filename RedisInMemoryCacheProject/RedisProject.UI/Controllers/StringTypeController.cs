using Microsoft.AspNetCore.Mvc;
using RedisProject.UI.Services;
using StackExchange.Redis;

namespace RedisProject.UI.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;

        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _redisService.Connect();
            _db = _redisService.GetDatabse(1);
        }

        public IActionResult Index()
        {

            _db.StringSet("name", "Yasin Kalender");

            return View();
        }

        public IActionResult Show()
        {
            var data = _db.StringGet("name");

            if (data.HasValue)
            {
                ViewBag.data = data.ToString();
            }

            return View();
        }
    }
}
