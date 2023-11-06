using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using TEST.Models;

namespace TEST.ViewModels
{
    public class VaccineDto
    {
        public Vaccine Vaccine { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> TypeList { get; set; }
    }
}
