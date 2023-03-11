using CarServis.Services.Repositories.UoW;
using CarServis.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarServis.Mvc.Controllers
{
    public class MakeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MakeController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await unitOfWork.Makes.GetAllMakesAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchText,int pageIndex,int pageSize)
        {
            return View(await unitOfWork.Makes.GetFilteredMakesAsync(searchText,pageIndex,pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MakeViewModel makeModel)
        {
            if (ModelState.IsValid)
            {
                var errors=await unitOfWork.Makes.ValidateMakeAsync(makeModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(makeModel);
                }

                string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Makes");

                await unitOfWork.Makes.CreateNewMakeAsync(makeModel, imagesFolder);
                await unitOfWork.ConfirmChangesAsync();
                int lastMakeId = (await unitOfWork.Makes.GetAllAsync()).LastOrDefault().Id;
                return RedirectToAction(nameof(Edit), new { id = lastMakeId });
            }
            else
            {
                return View(makeModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await unitOfWork.Makes.GetSingleMakeAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MakeViewModel makeModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.Makes.ValidateMakeAsync(makeModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(makeModel);
                }

                string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Makes");

                await unitOfWork.Makes.UpdateMakeAsync(makeModel, imagesFolder);
                await unitOfWork.ConfirmChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(makeModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Makes");

            await unitOfWork.Makes.DeleteMakeAsync(id, imagesFolder);
            await unitOfWork.ConfirmChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
