using System.Security.Claims;
using School.Auth.Extensions;
using School.Domain.Entities.StudentAggregate;
using School.Domain.Interfaces;

namespace School.Auth.Services
{
    public interface IAuth
    {
        Task<string> Authenticate(string email, string password, bool hashPassword = true);
        int? getUserFromToken(string token);

        string? getRoleFromToken(string token);


    }
    public class Auth : IAuth
    {
        private readonly IAuthenticate auth;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<Student> _studentRepository;
        public Auth(IAuthenticate auth, IConfiguration configuration, IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            this.auth = auth;
            _configuration = configuration;
            this.auth.SecretKey = _configuration["jwt:secret"];
        }
        public async Task<string> Authenticate(string username, string password, bool hashPassword = true)
        {
            var hashPwd = hashPassword == true ? password.Hash() : password;
            var student = new Student(username, hashPwd);
            var user = await _studentRepository.Get(u => u.Email.ToLower() == student.Email.ToLower() && u.Password == hashPwd);
            if (user is null) return null;
            var perform_auth = Authenticate(user.Id);
            return perform_auth;
        }
        public int? getUserFromToken(string token)
        {

            var claims = auth.GetClaims(token.Replace("Bearer", string.Empty).Trim(), _configuration["jwt:issuer"], _configuration["jwt:audience"]);
            if (claims is not null)
            {
                string[] clms = claims.Select(x => x.Value).ToArray();
                var userId = clms[0];
                return int.Parse(userId);
            }
            return null;
        }
        public string? getRoleFromToken(string token)
        {

            var claims = auth.GetClaims(token.Replace("Bearer", string.Empty).Trim(), _configuration["jwt:issuer"], _configuration["jwt:audience"]);
            if (claims is not null)
            {
                string[] clms = claims.Select(x => x.Value).ToArray();
                string role = clms[1];
                return role;
            }
            return null;
        }
        string Authenticate(int userId)
        {
            var claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, userId.ToString()),
            });
            var result = auth.CreateToken(claims, _configuration["jwt:issuer"], _configuration["jwt:audience"], 45);
            return result;
        }


    }
}
