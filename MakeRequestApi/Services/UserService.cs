using MakeRequestApi.Entities;
using MakeRequestApi.Helpers;
using MakeRequestApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MakeRequestApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequestModel model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService: IUserService
    {
        private readonly UsersContext _context;
        private readonly AppSettings _appSettings;

        public UserService(UsersContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        private List<User> _users = new List<User>
    {
        new User { UserId = 1, FirstName = "Ayo", LastName = "Oyeneye", Email = "ayo44pretty@gmail.com", Password = "test1234" }
    };

        public AuthenticateResponse Authenticate(AuthenticateRequestModel model)
        {
            var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        //public User GetByUserId(int UserId)
        //{
        //    return _context.Users.Find(x => x.UserId == UserId);
        //} TO BE WORKED ON LATER

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ApaticularSecretKey8172022");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
