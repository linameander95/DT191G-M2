using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using moment2.Models;
using System.Text.Json;

namespace moment2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult About(Person person)
        {
            ViewBag.PersonName = person.Name;
            ViewData["PersonAge"] = person.Age;

            if (!string.IsNullOrEmpty(person.Name))
            {
                HttpContext.Session.SetString("PersonName", person.Name);
                HttpContext.Session.SetInt32("PersonAge", person.Age);

                // Save data in a cookie.
                Response.Cookies.Append("PersonName", person.Name);
                Response.Cookies.Append("PersonAge", person.Age.ToString());
            }

            var peopleJson = HttpContext.Session.GetString("People");
            var people = string.IsNullOrEmpty(peopleJson) ? new List<Person>() : JsonSerializer.Deserialize<List<Person>>(peopleJson);
            people.Add(person);
            HttpContext.Session.SetString("People", JsonSerializer.Serialize(people));

            return View(person);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult PreviousPeople()
        {
            var peopleJson = HttpContext.Session.GetString("People");
            var people = string.IsNullOrEmpty(peopleJson) ? new List<Person>() : JsonSerializer.Deserialize<List<Person>>(peopleJson);
            return View(people);
        }
    }
}