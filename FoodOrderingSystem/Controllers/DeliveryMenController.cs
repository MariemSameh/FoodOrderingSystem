using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ITIProject.Controllers
{
    public class DeliveryMenController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public DeliveryMenController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;

            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Index", _context.DeliveryMen.ToList());
            }
            else
            {
                return View("Index", _context.DeliveryMen.Where(d => d.Name.Contains(search)).ToList());
            }
        }

        public IActionResult GetDetailsView(int id)
        {
            DeliveryMan del = _context.DeliveryMen.Include(d => d.Restaurant).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDelivery = del;

            if (del == null)
                return NotFound();
            return View("Details", del);
        }


        public IActionResult GetCreateView()
        {
            ViewBag.AllRestaurants = _context.Restaurants.ToList();

            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(DeliveryMan del, IFormFile? imageFormFile)
        {
            if (del.BirthDate > new DateTime(2004, 1, 1))
            {
                ModelState.AddModelError(String.Empty, "Birth Date must be Befor 31 Decamber 2003");
            }

            if (ModelState.IsValid)
            {
                if (imageFormFile != null)
                {

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    del.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    del.ImagePath = "\\images\\NoDImage.png";
                }


                _context.DeliveryMen.Add(del);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllRestaurants = _context.Restaurants.ToList();

                return View("Create");
            }
        }

        public IActionResult GetEditView(int id)
        {
            DeliveryMan del = _context.DeliveryMen.FirstOrDefault(d => d.Id == id);
            if (del == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllRestaurants = _context.Restaurants.ToList();

                return View("Edit", del);
            }

        }

        [HttpPost]
        public IActionResult EditCurrent(DeliveryMan del, IFormFile? imageFormFile)
        {
            if (del.BirthDate > new DateTime(2004, 1, 1))
            {
                ModelState.AddModelError(String.Empty, "Birth Date must be Befor 31 Decamber 2003");
            }

            if (ModelState.IsValid)
            {
                if (imageFormFile != null)
                {
                    if (del.ImagePath != "\\images\\NoDImage.png")
                    {
                        String OldImgFullPath = _webHostEnvironment.WebRootPath + del.ImagePath;
                        System.IO.File.Delete(OldImgFullPath);
                    }

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    del.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }

                _context.DeliveryMen.Update(del);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllRestaurants = _context.Restaurants.ToList();

                return View("Edit");
            }
        }


        public IActionResult GetDeleteView(int id)
        {
            DeliveryMan del = _context.DeliveryMen.Include(d => d.Restaurant).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDelivery = del;

            if (del == null)
                return NotFound();
            return View("Delete", del);
        }


        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            DeliveryMan del = _context.DeliveryMen.Find(id);

            if (del != null && del.ImagePath != "\\images\\NoDImage.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + del.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.DeliveryMen.Remove(del);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }
    }
}
