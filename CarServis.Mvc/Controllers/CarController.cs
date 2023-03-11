using CarServis.Services.Repositories.UoW;
using CarServis.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CarServis.Mvc.Controllers
{
    public class CarController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CarController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        private HashSet<string> ReturnCustomers()
        {
            HashSet<string> customers = new();

            foreach (var item in (unitOfWork.Customers.GetAllAsync()).Result)
            {
                StringBuilder sb = new();

                sb.Append(item.FirstName);
                sb.Append(" ");
                sb.Append(item.LastName);
                sb.Append(" ");
                sb.Append(item.Code);

                customers.Add(sb.ToString());
            }

            return customers;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
            ViewData["Customers"] = ReturnCustomers();

            return View(await unitOfWork.Cars.GetAllCarsAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchText, string make, string customer, int pageIndex, int pageSize)
        {
            ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
            ViewData["Customers"] = ReturnCustomers();

            return View(await unitOfWork.Cars.GetFilteredCarsAsync(searchText, make, customer, pageIndex, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
            ViewData["Customers"] = ReturnCustomers();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel carModel)
        {
            if (ModelState.IsValid)
            {
                var errors=await unitOfWork.Cars.ValidateCarAsync(carModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }

                    ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
                    ViewData["Customers"] = ReturnCustomers();
                    
                    return View(carModel);
                }

                string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Cars");

                await unitOfWork.Cars.CreateNewCarAsync(carModel, imagesFolder);
                await unitOfWork.ConfirmChangesAsync();
                int lastCarId=(await unitOfWork.Cars.GetAllAsync()).LastOrDefault().Id;
                return RedirectToAction(nameof(Edit), new { id = lastCarId });
            }
            else
            {
                ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
                ViewData["Customers"] = ReturnCustomers();

                return View(carModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
            ViewData["Customers"] = ReturnCustomers();
            return View(await unitOfWork.Cars.GetSingleCarAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarViewModel carModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.Cars.ValidateCarAsync(carModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }

                    ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
                    ViewData["Customers"] = ReturnCustomers();

                    return View(carModel);
                }

                string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Cars");

                await unitOfWork.Cars.UpdateCarAsync(carModel, imagesFolder);
                await unitOfWork.ConfirmChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Makes"] = (await unitOfWork.Makes.GetAllAsync()).Select(e => e.Name).ToHashSet();
                ViewData["Customers"] = ReturnCustomers();

                return View(carModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Cars");

            await unitOfWork.Cars.DeleteCarAsync(id, imagesFolder);
            await unitOfWork.ConfirmChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
