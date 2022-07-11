using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetLayeredTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterTestExceptionController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> ReadExcepiton()
        {
            return await System.IO.File.ReadAllTextAsync(@"D:\DeskTop\FileUpdate\256.txt");
        }


    }
}
