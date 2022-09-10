using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //API appsettings'deki değerleri okumaya yarar
        private TokenOptions _tokenOptions;  //IConfiguration'ın okuduğu değerleri tutan nesne
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration) //.Net Core'dan geleni API içerisine enjekte edilir
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //Configuration'daki alanı bul (appsetting "TokenOptions" bölümünü al ve
            //TokenOptions sınıfı içerisindeki prop'lara aktarır
            //Note: TokenOptions çoğul sebebi içerisindeki herbir prop bir options

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //User bilgileri ve claimlerine göre TOKEN oluşturur
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            //token expiration app settingsten gelen değeri  datetime.now değerine eklenerek token süresi belirlenir
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            //tokenoptiondaki security kullanılarak securitykey oluşturulur
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            //hangi algoritmanın kullanılacağı yazılan SigningCredentialsHelper üzerinden bilgi gelir
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            //tokenoptionları kullanılarak bu kullanıcıya atanacak tokenlar
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        //JWT'de kullanıcıya karşılık gelen bilgiler SetClaims ile verilir
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();

            //Aşağıdaki methodlar extension'lar. Uzun yazmak yerine extension yapılır o method kullanıldı
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}"); //$ ve süslü parantez kod yazmak için
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            //claim'leri (rolleri) array'e basıp rollere ekleniyor

            return claims;
        }
    }
}
