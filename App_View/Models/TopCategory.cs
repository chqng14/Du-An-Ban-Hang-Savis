using App_Data.Models;
using App_View.IServices;
using Microsoft.AspNetCore.Mvc;

namespace App_View.Models
{
    public class TopCategory : ViewComponent
    {
        private readonly DbContextModel _context;
        public TopCategory()
        {
            _context = new DbContextModel();
        }

        public IViewComponentResult Invoke()
        {
            List<TypeProduct> lstTypeProduct = _context.TypeProducts.Where(x=>x.TrangThai ==0).ToList();
            return View(lstTypeProduct);
        }
    }
}
