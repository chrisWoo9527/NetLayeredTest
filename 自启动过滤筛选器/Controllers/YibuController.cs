using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sql.Data;

namespace 自启动过滤筛选器.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YibuController : ControllerBase
    {
        private readonly TestDbContext _testDbContext;

        public YibuController(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddBookPersion()
        {
            await _testDbContext.Books.AddAsync(new Book { Title = "", Author = "", PubTime = DateTime.Now });
            await _testDbContext.SaveChangesAsync();

            _testDbContext.Persons.Add(new Person { Name = "测试" });
            _testDbContext.SaveChanges();
            return "ok";
        }
    }
}
