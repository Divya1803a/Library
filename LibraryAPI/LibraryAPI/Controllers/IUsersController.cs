using System;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    public interface IUsersController
    {
        public IActionResult Post(LoginRequest request);
        public IActionResult GetAll();

    }
}