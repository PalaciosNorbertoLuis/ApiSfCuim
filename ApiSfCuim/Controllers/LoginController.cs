using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.DirectoryServices.Protocols;
using System.Net;

namespace ApiSfCuim.Controllers
{
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

        private  string GenerateJwtToken(UserModel usuario)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("tokenSettings:SecurityKey")));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new  ClaimsIdentity(new Claim[]
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


        //Verificar usuario, contraseña y grupo contra active directory
        private dynamic AuthenticateUser(UserModel login)
        {
            //Path del servidor Active Directory
            var _path = _config.GetValue<string>("serverSettings:Server");
            LdapConnection connection = new(_path);


            try
            {
                //Verificar usuario y contraseña
                connection.Credential = new NetworkCredential(login.User, login.Password);
                connection.Bind();
                   
                // Parametros para la busqueda en Active Directory
                string[] attributesToReturn = new string[] { "displayName", "mail", "cn", "memberOf" };

                string targetOu = _config.GetValue<string>("serverSettings:PathSearch");

                string ldapSearchFilter = $"(sAMAccountName={login.User})";

                SearchRequest searchRequest = new(targetOu, ldapSearchFilter, 
                                                  SearchScope.Subtree,
                                                  attributesToReturn);

                SortRequestControl sortRequest = new("sn", false);
                searchRequest.Controls.Add(sortRequest);

                // Realizar busqueda
                SearchResponse searchResponse =
                            (SearchResponse)connection.SendRequest(searchRequest);

                // Enumerar resultados 
                foreach (SearchResultEntry entry in searchResponse.Entries)
                {
                    // Si tiene el grupo
                    if (entry.Attributes["memberOf"][0].Equals(_config.GetValue<string>("serverSettings:PathGroup")))
                    {
                        login.NombreCompleto = entry.Attributes["displayName"][0].ToString();
                        return login;
                    }
 
                }
                //Sin el grupo.
                dynamic data = new { Mensaje = $"El usuario <span style='color:red'>{login.User}</span> no tiene acceso a la aplicación" };
                return data;
            }

            catch (LdapException lexc)
            {
                if(lexc.Message == "The supplied credential is invalid.")
                {
                    //return ("El usuario o la contraseña son incorrectos");
                    return new { Mensaje = $"El usuario o la contraseña son incorrectos" };
                }

                return lexc.Message;    
                
            }

            catch (Exception e)
            {
                
                return e.Message;
            }
        }
    }
}
