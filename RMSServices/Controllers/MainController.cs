using Microsoft.AspNetCore.Mvc;
using RMSModel.Models;

namespace RMSServices.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
    ////[Route("MsBuyer/")]
    //public class MsBuyerController : CrudController<MsBuyer>
    //{
    //    public MsBuyerController()
    //      : base(db => db.MsBuyer)
    //    { }
    //}

    //public class MsUserController : CrudController<MsUser>
    //{
    //    public MsUserController()
    //      : base(db => db.MsUser)
    //    { }
    //}
}
