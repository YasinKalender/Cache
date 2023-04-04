using Microsoft.AspNetCore.Mvc;
using RedisProject.UI.Services;
using StackExchange.Redis;

namespace RedisProject.UI.Controllers
{
    public class HashTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private readonly string listeKey = "hashKey";

        public HashTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _redisService.Connect();
            _db = _redisService.GetDatabse(5);
        }

        public IActionResult Index()
        {
            _db.HashSet(listeKey, "book", "kitap");

            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (_db.KeyExists(listeKey))
            {
                _db.HashGetAll(listeKey).ToList().ForEach(i => { dic.Add(i.Name, i.Value); });
            }

            return View(dic);
        }

        public IActionResult Remove()
        {
            _db.HashDelete(listeKey, "book");

            return RedirectToAction("Index");
        }
    }
}
