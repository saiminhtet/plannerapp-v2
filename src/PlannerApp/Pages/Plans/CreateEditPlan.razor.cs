using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Pages.Plans
{
    public partial class CreateEditPlan
    {
        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/index"),
            new BreadcrumbItem("Plans", "/plans"),
            new BreadcrumbItem("Create", "/plans/form", true)
        };
    }
}
 