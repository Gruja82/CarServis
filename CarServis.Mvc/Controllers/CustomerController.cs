using CarServis.Services.Repositories.UoW;
using CarServis.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;

namespace CarServis.Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await unitOfWork.Customers.GetAllCustomersAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchText,int pageIndex,int pageSize)
        {
            return View(await unitOfWork.Customers.GetFilteredCustomersAsync(searchText,pageIndex,pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customerModel)
        {
            if (ModelState.IsValid)
            {
                var errors=await unitOfWork.Customers.ValidateCustomerAsync(customerModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(customerModel);
                }

                await unitOfWork.Customers.CreateNewCustomerAsync(customerModel);
                await unitOfWork.ConfirmChangesAsync();
                int lastCustomerId = (await unitOfWork.Customers.GetAllAsync()).LastOrDefault().Id;
                return RedirectToAction(nameof(Edit), new { id = lastCustomerId });
            }
            else
            {
                return View(customerModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await unitOfWork.Customers.GetSingleCustomerAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel customerModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.Customers.ValidateCustomerAsync(customerModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(customerModel);
                }

                await unitOfWork.Customers.UpdateCustomerAsync(customerModel);
                await unitOfWork.ConfirmChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(customerModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await unitOfWork.Customers.DeleteCustomerAsync(id);
            await unitOfWork.ConfirmChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
