using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Controllers
{
    public class RestaurantsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public RestaurantsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;

            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Index", _context.Restaurants.ToList());
            }
            else
            {
                return View("Index", _context.Restaurants.Where(r => r.Name.Contains(search) || r.Category.Contains(search)).ToList());
            }
        }


        public IActionResult GetDetailsView(int id)
        {
            Restaurant rest = _context.Restaurants.Include(d => d.DeliveryMen).FirstOrDefault(r => r.Id == id);

            ViewBag.CurrentRest = rest;

            if (rest == null)
                return NotFound();
            return View("Details", rest);
        }


        public IActionResult GetCreateView()
        {
            return View("Create");
        }


        [HttpPost]
        public IActionResult AddNew(Restaurant rest, IFormFile? imageFormFile)
        {

            if (rest.StartDate < new DateTime(1995, 1, 1))
            {
                ModelState.AddModelError(String.Empty, "Start Date must be after 31 Decamber 1994");
            }

            if (ModelState.IsValid)
            {
                if (imageFormFile != null)
                {

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    rest.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    rest.ImagePath = "\\images\\NoRImage.png";
                }


                _context.Restaurants.Add(rest);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Create");
            }
        }

        public IActionResult GetEditView(int id)
        {
            Restaurant rest = _context.Restaurants.FirstOrDefault(r => r.Id == id);
            if (rest == null)
                return NotFound();
            return View("Edit", rest);
        }

        [HttpPost]
        public IActionResult EditCurrent(Restaurant rest, IFormFile? imageFormFile)
        {
            if (rest.StartDate < new DateTime(1995, 1, 1))
            {
                ModelState.AddModelError(String.Empty, "Start Date must be after 31 Decamber 1994");
            }

            if (ModelState.IsValid)
            {
                if (imageFormFile != null)
                {
                    if (rest.ImagePath != "\\images\\NoRImage.png")
                    {
                        String OldImgFullPath = _webHostEnvironment.WebRootPath + rest.ImagePath;
                        System.IO.File.Delete(OldImgFullPath);
                    }

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    rest.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }


                _context.Restaurants.Update(rest);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit");
            }
        }


        public IActionResult GetDeleteView(int id)
        {
            Restaurant rest = _context.Restaurants.Include(d => d.DeliveryMen).FirstOrDefault(r => r.Id == id);

            ViewBag.CurrentRest = rest;

            if (rest == null)
                return NotFound();
            return View("Delete", rest);
        }


        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Restaurant rest = _context.Restaurants.Find(id);

            if (rest != null && rest.ImagePath != "\\images\\NoRImage.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + rest.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.Restaurants.Remove(rest);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }
    }
}
