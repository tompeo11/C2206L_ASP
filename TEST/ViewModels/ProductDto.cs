using Microsoft.AspNetCore.Mvc.Rendering;
using TEST.Models;

namespace TEST.ViewModels
{
    public class ProductDto
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
