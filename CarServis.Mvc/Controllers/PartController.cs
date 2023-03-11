using CarServis.Services.Repositories.UoW;
using CarServis.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarServis.Mvc.Controllers
{
    public class PartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PartController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await unitOfWork.Parts.GetAllPartsAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchText,int pageIndex,int pageSize)
        {
            return View(await unitOfWork.Parts.GetFilteredPartsAsync(searchText,pageIndex,pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartViewModel partModel)
        {
            if (ModelState.IsValid)
            {
                var errors=await unitOfWork.Parts.ValidatePartAsync(partModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(partModel);
                }

                string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Parts");

                await unitOfWork.Parts.CreateNewPartAsync(partModel, imagesFolder);
                await unitOfWork.ConfirmChangesAsync();
                int lastPartId = (await unitOfWork.Parts.GetAllAsync()).LastOrDefault().Id;
                return RedirectToAction(nameof(Edit), new { id = lastPartId });
            }
            else
            {
                return View(partModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await unitOfWork.Parts.GetSinglePartAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartViewModel partModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.Parts.ValidatePartAsync(partModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(partModel);
                }
                string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Parts");

                await unitOfWork.Parts.UpdatePartAsync(partModel, imagesFolder);
                await unitOfWork.ConfirmChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(partModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string imagesFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Images/Parts");
            await unitOfWork.Parts.DeletePartAsync(id, imagesFolder);
            await unitOfWork.ConfirmChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
