using Microsoft.AspNetCore.Mvc;
using RedisProject.UI.Services;
using StackExchange.Redis;

namespace RedisProject.UI.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private readonly string listeKey = "listKey";

        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _redisService.Connect();
            _db = _redisService.GetDatabse(2);
        }

        public IActionResult Index()
        {
            string data = "deneme2";
            _db.ListLeftPush(listeKey, data);

            List<string> list = new List<string>();

            if (_db.KeyExists("listKey"))
            {

                _db.ListRange("listKey").ToList().ForEach(x =>
                {
                    list.Add(x);
                });

            }
            return View(list);
        }

        public IActionResult Remove()
        {

            //_db.ListRemove(listeKey, "deneme"); listeden o veriyi bulup siler..

            _db.ListRightPop(listeKey);
            return RedirectToAction("Index");
        }
    }
}
