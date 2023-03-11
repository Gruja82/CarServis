using CarServis.Services.Repositories.UoW;
using CarServis.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CarServis.Mvc.Controllers
{
    public class RepairController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public RepairController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
            ViewData["Customers"] = ReturnCustomers();
            return View(await unitOfWork.Repairs.GetAllRepairsAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchText,string customer,int pageIndex,int pageSize)
        {
            ViewData["Customers"] = ReturnCustomers();
            return View(await unitOfWork.Repairs.GetFilteredRepairsAsync(searchText, customer, pageIndex, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Customers"] = ReturnCustomers();
            ViewData["Cars"] = await unitOfWork.Repairs.ReturnCarMakeAndModel();
            ViewData["Parts"] = (await unitOfWork.Parts.GetAllAsync()).Select(e => e.Name).ToHashSet();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepairViewModel repairModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.Repairs.ValidateRepairAsync(repairModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    ViewData["Customers"] = ReturnCustomers();
                    ViewData["Cars"] = await unitOfWork.Repairs.ReturnCarMakeAndModel();
                    ViewData["Parts"] = (await unitOfWork.Parts.GetAllAsync()).Select(e => e.Name).ToHashSet();
                    return View(repairModel);
                }

                await unitOfWork.Repairs.CreateNewRepairAsync(repairModel);
                await unitOfWork.ConfirmChangesAsync();
                int lastReapirId = (await unitOfWork.Repairs.GetAllAsync()).LastOrDefault().Id;
                return RedirectToAction(nameof(Edit), new { id = lastReapirId });
            }
            else
            {
                ViewData["Customers"] = ReturnCustomers();
                ViewData["Cars"] = await unitOfWork.Repairs.ReturnCarMakeAndModel();
                ViewData["Parts"] = (await unitOfWork.Parts.GetAllAsync()).Select(e => e.Name).ToHashSet();
                return View(repairModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Customers"] = ReturnCustomers();
            ViewData["Cars"] = await unitOfWork.Repairs.ReturnCarMakeAndModel();
            ViewData["Parts"] = (await unitOfWork.Parts.GetAllAsync()).Select(e => e.Name).ToHashSet();
            return View(await unitOfWork.Repairs.GetSingleRepairAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RepairViewModel repairModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.Repairs.ValidateRepairAsync(repairModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    ViewData["Customers"] = ReturnCustomers();
                    ViewData["Cars"] = await unitOfWork.Repairs.ReturnCarMakeAndModel();
                    ViewData["Parts"] = (await unitOfWork.Parts.GetAllAsync()).Select(e => e.Name).ToHashSet();
                    return View(repairModel);
                }

                await unitOfWork.Repairs.UpdateRepairAsync(repairModel);
                await unitOfWork.ConfirmChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Customers"] = ReturnCustomers();
                ViewData["Cars"] = await unitOfWork.Repairs.ReturnCarMakeAndModel();
                ViewData["Parts"] = (await unitOfWork.Parts.GetAllAsync()).Select(e => e.Name).ToHashSet();
                return View(repairModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await unitOfWork.Repairs.DeleteRepairAsync(id);
            await unitOfWork.ConfirmChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
