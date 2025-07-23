using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.Enums.FilterEnums;
using static ForumApp.GCommon.Enums.SortEnums.Board;
using static ForumApp.GCommon.Enums.SortEnums.Post;

namespace ForumApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
