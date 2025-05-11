using DataAccessLayer2.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    [Authorize (Roles ="Admin")]

    public class AdminController : Controller
    {
        private readonly DataContext _db;

        public AdminController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var user = _db.Users.Where(x => x.Role == "User").ToList();
            return View(user);
        }

        public ActionResult UserDelete(int id)
        {
            var userid = _db.Users.Where(x => x.Id == id).FirstOrDefault();
            _db.Users.Remove(userid);
            _db.SaveChanges();
            return RedirectToAction("UserList");
        }

    }
}
