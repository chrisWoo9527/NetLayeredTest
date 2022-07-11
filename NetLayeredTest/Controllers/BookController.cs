using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetLayeredTest.Dto;
using Sql.Data;

namespace NetLayeredTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly TestDbContext _testDbContext;

        public BookController(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddBook(BookDto input)
        {
            await _testDbContext.Books.AddAsync(new Book {  Title = input.Title, Author = input.Author, PubTime = input.PubTime });
            await _testDbContext.SaveChangesAsync();
            return "ok";
        }
    }
}
