using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace WebShop.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult Error(this ControllerBase controller, Error error)
        {
            return controller.StatusCode(error.StatusCode, error.Message);
        }
    }
}
