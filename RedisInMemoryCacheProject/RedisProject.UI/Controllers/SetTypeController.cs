using Microsoft.AspNetCore.Mvc;
using RedisProject.UI.Services;
using StackExchange.Redis;

namespace RedisProject.UI.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        public string listKey = "setType";

        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _redisService.Connect();
            _db = _redisService.GetDatabse(3);
        }
        public IActionResult Index()
        {
            string data = "deneme2";
            _db.KeyExpire(listKey, DateTime.Now);
            _db.SetAdd(listKey, data);

            List<string> list = new List<string>();

            if (_db.KeyExists(listKey))
            {
                _db.SetMembers(listKey).ToList().ForEach(x => list.Add(x));
            }

            return View(list);
        }

        public IActionResult Remove()
        {
            _db.SetRemove(listKey, "deneme2");
            return RedirectToAction("Index");
        }
    }
}
