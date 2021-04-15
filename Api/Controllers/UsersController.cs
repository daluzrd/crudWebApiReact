using DataAccess;
using DataServices;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersServices _usersServices;

        public UsersController([FromServices] DataContext dbContext)
        {
            _usersServices=new UsersServices(dbContext);
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            return Ok(_usersServices.GetAll());
        }
    }
}
