using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChurchWeb.Models;
using Repository;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace ChurchWeb.Controllers
{
    public class HomeController : Controller
    {
        ICarouselItemRepository _carouselItemRepository;
        INavBarItemRepository _navBarItemRepository;

        public HomeController(
            INavBarItemRepository navBarItemRepository,
            ICarouselItemRepository carouselItemRepository)
        {
            _carouselItemRepository = carouselItemRepository;
            _navBarItemRepository = navBarItemRepository;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View(model: new LayoutViewModel
            {
                NavBarItems = _navBarItemRepository.GetAll().ToList()
            });
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View(model: new LayoutViewModel
            {
                NavBarItems = _navBarItemRepository.GetAll().ToList()
            });
        }

        [Authorize(Roles = "ChurchMember")]
        public IActionResult Directory() => View(model: new LayoutViewModel
        {
            NavBarItems = _navBarItemRepository.GetAll().ToList()
        });

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorNotLoggedIn() => View(model: new LayoutViewModel
        {
            NavBarItems = _navBarItemRepository.GetAll().ToList()
        });

        public IActionResult ErrorForbidden() => View(model: new LayoutViewModel
        {
            NavBarItems = _navBarItemRepository.GetAll().ToList()
        });

        public IActionResult Index()
        {
            return View(model: new IndexViewModel
            {
                NavBarItems = _navBarItemRepository.GetAll().ToList(),
                CarouselItems = _carouselItemRepository.GetAll().ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index");
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "ChurchMember")
            },
            CookieAuthenticationDefaults.AuthenticationScheme);

            var principle = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principle);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login() => View(model: new LayoutViewModel
        {
            NavBarItems = _navBarItemRepository.GetAll().ToList()
        });

        [HttpGet]
        public async Task<IActionResult> Logout(string name)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }
    }
}
