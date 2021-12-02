using Microsoft.AspNetCore.Authorization;
using ApiSfCuim.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using Newtonsoft.Json;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Options;

namespace ApiSfCuim.Controllers
{
    [SupportedOSPlatform("windows")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response;
            // Se verifica el usuario con Active Directory
            

            dynamic data = AuthenticateUser(login);

            if (data.GetType().GetProperty("User") != null)
            {
                //var tokenString = GenerateJwtToken(data);
                response = Ok(new { token = GenerateJwtToken(data), data.NombreCompleto});
            }
            else
            {
                response = Ok(data);
            }

            return response;
        }

        private string GenerateJwtToken(UserModel usuario)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("tokenSettings:SecurityKey")));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.User),
                    new Claim(ClaimTypes.GivenName, usuario.NombreCompleto),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<double>("tokenSettings:TimeLife")),
                SigningCredentials = signinCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        //Verificar usuario contraseña y grupo contra active directory
        private dynamic AuthenticateUser(UserModel login)
        {
            string userName = login.User;
            string password = login.Password;
            var domainAndUsername = _config.GetValue<string>("serverSettings:Domain") + @"\" + userName;
            var _path = _config.GetValue<string>("serverSettings:Server");


            // Validar si el usuario existe 
            PrincipalContext pc = new(ContextType.Domain, _path);
            if (!pc.ValidateCredentials(userName, password))
            {
                dynamic data = new { Mensaje = "Usuario o contraseña incorrectos." };
                return data;

            }


            DirectoryEntry entry = new("LDAP://" + _path, domainAndUsername, password);

            DirectorySearcher search = new(entry);

            try
            {
                //Busqueda del nombre.
                search.Filter = "sAMAccountName=" + userName + "";
                SearchResult results = search.FindOne();
                string NombreCompleto = results.GetDirectoryEntry().Properties["DisplayName"].Value.ToString();

                // Validar el grupo de Active Directory
                bool memberOf = results.GetDirectoryEntry().Properties["memberOf"].Contains(_config.GetValue<string>("serverSettings:PathGroup"));

                //usuario password y grupo correcto.
                if (memberOf)
                {
                    UserModel user = new() { User = userName, NombreCompleto = NombreCompleto };
                    return user; 
                }
                //Sin el grupo.
                else
                {
                    dynamic data = new { Mensaje = $"El usuario <span style='color:red'>{userName}</span> no tiene acceso a la aplicación" };
                    return data;
                }
            }
            catch (DirectoryServicesCOMException ex)  //Exception
            {
                dynamic data = new { Mensaje = $"{ex.Message}" };
                return data;
            }
            finally
            {
                entry.Close();
                entry.Dispose();
                search.Dispose();
            }
        }
    }
}
