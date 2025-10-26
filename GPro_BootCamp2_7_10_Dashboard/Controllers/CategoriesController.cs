using GPro_BootCamp2_7_10_Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Dashboard.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService category;
        public CategoriesController(ICategoryService cate)
        { category= cate;}

        public async Task<IActionResult> Index(int page=1 , int pageSize = 20 , string? q= null)
        {
            var (items, total) = await category.GetPageAsync(page, pageSize, q);
            ViewBag.Total = total;
            ViewBag.Page= page;
            ViewBag.PageSize = pageSize;
            ViewBag.Q = q;  

            return View(items);
        }
        //index
        //create 
        //edit 
        //delete 
        //restore 
        //trash


    }
}
