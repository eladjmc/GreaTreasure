using GreaTreasure.DAL;
using GreaTreasure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Xml;

namespace GreaTreasure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View(Data.Get.Libraries);
        }
        public IActionResult CreateLibrary()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateLibrary(Library library)
        {
            if ((library != null))
            {
                Data.Get.Libraries.Add(library);
                Data.Get.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Shelves(int? id)
        {
            if (!IsNull(id))
            {
                Library? library = Data.Get.Libraries.Include(x =>x.Shelves).ThenInclude(x=>x.Books).FirstOrDefault(x => x.Id == id);
                if (library != null)
                {
                    TempData["libID"] = library.Id;
                    return View(library.Shelves);
                }
            }
            return RedirectToAction("Index");


        }
        public IActionResult CreateShelf(int? id)
        {
            ViewBag.libID = id;
            return View();
        }
        [HttpPost]
        public IActionResult CreateShelf(Shelf shelf, int libID)
        {
            var library = Data.Get.Libraries.Include(s => s.Shelves).FirstOrDefault(x => x.Id == libID);
            if (library != null)
            {
                shelf.Library = library; // Set the library instance
               library.Shelves.Add(new Shelf(shelf.Name, shelf.Height,shelf.Library));


                try
                {
                    Data.Get.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error saving shelf: {0}", ex.Message);
                    ViewBag.ErrorMessage = "An error occurred while saving the shelf. Please try again.";
                 
                }      
            }
            return RedirectToAction("Shelves", new { id = libID });
        }
        public IActionResult Books(int? id)
        {
            if (!IsNull(id))
            {
                Shelf? shelf = Data.Get.Shelves.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
                if (shelf != null)
                {
                    TempData["shelfID"] = shelf.Id;
                    return View(shelf.Books);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult CreateBook(int? id)
        {
            ViewBag.shelfID = id;
            return View(new Book());
        }
        [HttpPost]
        public IActionResult CreateBook(Book book, int shelfID)
        {
            var shelf = Data.Get.Shelves.Include(s => s.Books).FirstOrDefault(x => x.Id == shelfID);
            if (shelf != null)
            {
                if (shelf.Height < book.Height || shelf.Width - shelf.OccupiedWidth < book.Width)
                {
                    TempData["NoSpaceError"] = "true";
                    return RedirectToAction("Books", new { id = shelfID });
                }
                else if (shelf.Height - 10 >= book.Height)
                {
                    TempData["TooMuchSpaceAlert"] = "true";
                }

                book.shelf = shelf;
                shelf.Books.Add(new Book(book.Title, book.Description, book.shelf, book.Width, book.Height));

            
                    Data.Get.SaveChanges();  
            }

            return RedirectToAction("Books", new { id = shelfID });
        }

        public bool IsNull(int? num)
        {
            return num == null;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
