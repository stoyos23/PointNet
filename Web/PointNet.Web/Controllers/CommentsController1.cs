// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointNet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController1 : Controller
    {
        // GET: /<controller>/
        public IActionResult AddComment()
        {
            return this.View();
        }
    }
}
