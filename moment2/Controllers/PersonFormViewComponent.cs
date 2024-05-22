using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class PersonForm : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new Person());
        }
    }
}