using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InAndOut.Models.ViewModels
{
    public class ExpenseVm
    {
        public Expense Expense { get; set; }

        public IEnumerable<SelectListItem> TypeDropDown { get; set;}
    }
}