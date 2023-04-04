using Microsoft.AspNetCore.Mvc;
using RedisProject.UI.Services;
using StackExchange.Redis;

namespace RedisProject.UI.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        public string listKey = "sortedSetType";

        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _redisService.Connect();
            _db = _redisService.GetDatabse(4);
        }
        public IActionResult Index()
        {
            List<string> keys = new List<string>();
            _db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));
            _db.SortedSetAdd(listKey, "deneme1", 20);

            if (_db.KeyExists(listKey))
            {
                //_db.SortedSetScan(listKey).ToList().ForEach(i => { keys.Add(i.ToString()); });

                _db.SortedSetRangeByRank(listKey, order: Order.Descending).ToList().ForEach(i => { keys.Add(i.ToString()); });
            }


            return View(keys);
        }

        public IActionResult Remove()
        {
            _db.SortedSetRemove(listKey, "deneme");

            return RedirectToAction("Index");
        }
    }
}
