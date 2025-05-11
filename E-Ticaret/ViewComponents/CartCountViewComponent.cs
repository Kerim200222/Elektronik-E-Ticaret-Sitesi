using System.Linq;
using DataAccessLayer2.Context;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public CartCountViewComponent(DataContext context)
            => _context = context;

        public IViewComponentResult Invoke()
        {
            if (!User.Identity.IsAuthenticated)
                return Content("0");

            var email = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return Content("0");

            var count = _context.Carts
                .Where(c => c.UserId == user.Id)
                .Sum(c => c.Quantity);

            return Content(count.ToString());
        }
    }
}
