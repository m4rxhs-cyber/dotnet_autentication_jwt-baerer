using Microsoft.AspNetCore.Mvc;
using ProjetoMySQL.Repositories;
using ProjetoMySQL.Services;
using ProjetoMySQL.Models;

namespace ProjetoMySQL.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController:ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody]  User model)
        {
            var user =  UserRepository.Get(model.Username, model.Password);  

            if (user == null)
                return NotFound(new {message = "Usuario ou senha invalidos"});

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
        
    }

}