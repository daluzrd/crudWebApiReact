using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataAccess;
using DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersServices _usersServices;

        public UsersController([FromServices] DataContext dbContext)
        {
            _usersServices=new UsersServices(dbContext);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Users>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                List<Users> listResult = _usersServices.GetAll().ToList();

                if(listResult.Count() > 0)
                    return Ok(listResult);
                return NotFound();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(string name, DateTime birthDate, string cpf, string user = null, string pwd = null)
        {
            try
            {
                Users users = new Users();
                users.Name = name;
                users.BirthDate = birthDate;
                users.CPF = cpf;
                users.User = user;
                users.Pwd = pwd;

                _usersServices.Post(users);
                return NoContent();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                Users user = _usersServices.GetById(id);

                if(user != null)
                    return Ok(user);
                    return NotFound();
                }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpGet]
        [Route("Name/{name}")]
        [ProducesResponseType(typeof(List<Users>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByName(string name)
        {
            try
            {
                List<Users> listResult = _usersServices.GetByName(name).ToList();

                if(listResult.Count() > 0)
                    return Ok(listResult);
                return NotFound();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("BetweenDates/{FirstDate}/{LastDate}")]
        [ProducesResponseType(typeof(List<Users>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBetweenBirthDates(string startDate, string endDate)
        {
            if(DateTime.TryParse(startDate, out DateTime newStartDate) && DateTime.TryParse(endDate, out DateTime newEndDate))
            {
                try
                {
                    List<Users> listResult = _usersServices.GetBetweenBirthDates(newStartDate, newEndDate).ToList();

                    if(listResult.Count() > 0)
                        return Ok(listResult);
                    return NotFound();
                }
                catch(Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("CPF/{cpf}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBetweenBirthDates(string cpf)
        {
            Regex regex = new Regex(@"[\D]");
            cpf = regex.Replace(cpf, "");
            if(cpf.Length == 11)
            {
                try
                {
                    Users result = _usersServices.GetByCPF(cpf);

                    if(result != null)
                        return Ok(result);
                    return NotFound();
                }
                catch(Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
                