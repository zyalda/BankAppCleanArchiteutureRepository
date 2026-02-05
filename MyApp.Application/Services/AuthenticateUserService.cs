using Microsoft.IdentityModel.Tokens;
using MyApp.Application.ServiceInterfaces;
using MyApp.Application.ServicesInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.Application.Services
{
    public class AuthenticateUserService : IAuthenticateUserService
    {
        public UserManagerResponse GenerateToken()
        {
            //Lista med vilka behörigheter som en användare har får man i
            //vanliga fall med sig från inloggningen. Här hårdkodar
            //vi att användaren har rollen Admin
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, Roll));


            //Sätta upp kryptering. Samma säkerhetsnyckel som när vi satte upp tjänsten
            //Denna förvaras på ett säkert ställe tex Azure Keyvault eller liknande och hårdkodas
            //inte in på detta sätt
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mykey1234567&%%485734579453%&//1255362"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //Skapa options för att sätta upp en token
            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:51597",
                    audience: "http://localhost:51597",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials);

            //Generar en ny token som skall skickas tillbaka 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            //return tokenString;
            return new UserManagerResponse
            {
                Message = tokenString,
                isSuccess = true,
            };
        }

        private string Roll { get; set; }

        public bool Login(int id, ICustomerService customerService, IUserTypeService userTypeService)
        {
            var user = customerService.GetCustomerById(id);

            //Här gör man i vanliga fall en kontroll av användaren. Det kan ske mot
            //en sk identity provider tjänst tex Azure Entra(cloud) eller Windows Server AD (ej cloud) 
            //Det går också att hantera användare via en egen databas och en user tabell

            if (user != null)
            {
                var userType = userTypeService.GetUserTypeById((int)user.UserTypeId);
                Roll = userType.UserTypeName;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
