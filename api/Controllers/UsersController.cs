using System.Linq;
using api.DAL;
using api.DTO;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        const string missing_parameter = "Missing parameter";
        const string invalid_user_password = "Invalid user or password";
        const string admin_role = "admin";
        const string Unauthorized_operation_user = "Unauthorized operation for user";

        [HttpPost()]
        public IActionResult Post([FromBody] LoginRequestDTO request)
        {
            if (request.RequestValidation() == false)
                return BadRequest(missing_parameter);

            var login = ValidateLogin(request);
            if (login.isAuthorized)
            {
                LoginResponseDTO loginResponse = new LoginResponseDTO();
                loginResponse.Login = login.user.Login;
                loginResponse.Role = login.user.Role;
                loginResponse.Balance = login.user.USD_Balance.ToString();
                return Ok(loginResponse);
            }
            return Unauthorized(invalid_user_password);
        }

        [HttpPut()]
        public IActionResult Put([FromBody] UpdateRequestDTO request)
        {
            if (request.RequestValidation() == false)
                return BadRequest(missing_parameter);

            var login = ValidateLogin(request);
            if (login.isAuthorized)
            {
                using (var context = new ApplicationDBContext())
                {
                    var user = (from v in context.User
                                where v.Login == request.Login
                                select v).FirstOrDefault();

                    user.USD_Balance = float.Parse(request.Balance,
                        System.Globalization.CultureInfo.InvariantCulture);

                    context.SaveChanges();

                    return Ok($"Balance updated: USDss $[{user.USD_Balance}]");
                }
            }
            return Unauthorized(invalid_user_password);
        }

        [HttpDelete()]
        public IActionResult Delete([FromBody] DeleteRequestDTO request)
        {
            if (request.RequestValidation() == false)
                return BadRequest(missing_parameter);

            var login = ValidateLogin(request);
            if (login.isAuthorized)
            {
                if (login.user.Role != admin_role)
                    return Unauthorized(Unauthorized_operation_user);

                using (var context = new ApplicationDBContext())
                {
                    var userToDelete = (from v in context.User
                                        where v.Login == request.User
                                        select v).FirstOrDefault();

                    if (userToDelete != null)
                    {
                        context.User.RemoveRange(userToDelete);
                        context.SaveChanges();
                        return Ok($"User deleted: [{request.User}]");
                    }
                    else
                        return NotFound($"User to delete does not exist: [{request.User}]");
                }
            }
            return Unauthorized(invalid_user_password);
        }

        private (bool isAuthorized, User user) ValidateLogin(LoginRequestDTO request)
        {
            using (var context = new ApplicationDBContext())
            {
                var user = (from v in context.User
                            where v.Login == request.Login
                            select v).FirstOrDefault();

                if (user != null && user.Password == request.Password)
                    return (true, user);
                return (false, user);
            }
        }
    }
}
