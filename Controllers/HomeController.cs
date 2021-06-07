using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("autheticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado 0 {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            //recupera usuário
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return HttpStatusCode.NotFound;

            //gera o token
            var token = TokenService.GenerateToken(user);

            //oculta a senha
            user.Password = "";

            //retorna os dados
            return new
            {
                User = user,
                Token = token
            };

        }
    }
}
