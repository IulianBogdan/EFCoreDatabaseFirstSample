using Microsoft.AspNetCore.Mvc;

namespace EFCoreDatabaseFirstSample.Controllers
{
    public class BookAuthorsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}