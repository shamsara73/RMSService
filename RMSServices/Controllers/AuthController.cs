using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RMSModel.Models;
using RMSModel.Tools;
using RMSServices.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace RMSServices.Controllers
{
    public class AuthController : Controller
    {
        public int jwt_token_expire_in_minutes { get; set; }

        IConfiguration configuration;
        public AuthController(IConfiguration config)
        {
            this.configuration = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/[controller]/")]
        public IActionResult Auth([FromBody] User user)
        {
            AuthResponseWrapper authResponseWrapper = new AuthResponseWrapper();

            //IActionResult response = Unauthorized();

            if(user != null)
            {
                RMSModel.Models.RMSContext db = new RMSModel.Models.RMSContext();
                string encryptedPassword = Cryptography.EncryptText(user.Password);
                if(db.MsUser.Any(x=>x.USERNAME == user.Username && x.PASSWORD == encryptedPassword))
                {
                    var _user = db.MsUser.First(x => x.USERNAME == user.Username && x.PASSWORD == encryptedPassword);
                    string _loginInvalidMessage = "";
                    
                    if(ValidateLogin(_user,out _loginInvalidMessage))
                    {
                        long jwt_refresh_expire_in_days = db.MsParameter.First(x => x.PRM_TEXT == "jwt_refresh_expire_in_days").PRM_VALUE.Value;


                        var tokenResult = CreateToken(_user);
                        string _accessToken = new JwtSecurityTokenHandler().WriteToken(tokenResult);
                        DateTime _expiryToken = tokenResult.ValidTo;
                        string _refreshToken = GenerateRefreshToken();
                        DateTime _expiryRefreshToken = _expiryToken.AddDays(jwt_refresh_expire_in_days);

                        if (db.MsUserToken.Any(x => x.USERID == _user.ID))
                        {
                            MsUserToken userToken = db.MsUserToken.First(x => x.USERID == _user.ID);
                            userToken.ACCESS_TOKEN = _accessToken;
                            userToken.ACCESS_TOKEN_EXPIRY = _expiryToken;
                            userToken.REFRESH_TOKEN = _refreshToken;
                            userToken.REFRESH_TOKEN_EXPIRY = _expiryRefreshToken;
                        }
                        else
                        {
                            MsUserToken userToken = new MsUserToken();
                            userToken.USERID = _user.ID;
                            userToken.ACCESS_TOKEN = _accessToken;
                            userToken.ACCESS_TOKEN_EXPIRY = _expiryToken;
                            userToken.REFRESH_TOKEN = _refreshToken;
                            userToken.REFRESH_TOKEN_EXPIRY = _expiryRefreshToken;
                            db.MsUserToken.Add(userToken);

                        }

                        db.SaveChanges();

                        authResponseWrapper.Message = "Success Login";
                        authResponseWrapper.Status = "Success";
                        authResponseWrapper.AccessToken = _accessToken;
                        authResponseWrapper.ValidUntil = _expiryToken;
                        authResponseWrapper.RefreshToken = _refreshToken;
                    }
                    else
                    {
                        authResponseWrapper.Status = "Failed";
                        authResponseWrapper.Message = _loginInvalidMessage;
                    }
                    

                    
                    //return Ok(Json(authResponseWrapper, new JsonSerializerOptions { PropertyNamingPolicy = null }));

                }
                else
                {
                    authResponseWrapper.Status = "Failed";
                    authResponseWrapper.Message = "Invalid Credential";
                    //return Unauthorized(Json(authResponseWrapper, new JsonSerializerOptions { PropertyNamingPolicy = null }));

                }

            }
            else
            {
                authResponseWrapper.Status = "Failed";
                authResponseWrapper.Message = "Bad Request";
                //return BadRequest(Json(authResponseWrapper, new JsonSerializerOptions { PropertyNamingPolicy = null }));


            }

            return Json(authResponseWrapper, new JsonSerializerOptions { PropertyNamingPolicy = null });

        }

        private bool ValidateLogin(MsUser user,out string Message)
        {
            string _msg = "";
            bool _isValid = true;
            if (user.IsDeleted.HasValue && user.IsDeleted.GetValueOrDefault())
                _msg = "User is no longer active";
            else if (!user.IsApproved.HasValue || !user.IsApproved.GetValueOrDefault())
                _msg = "User has not been approved";
            else if (user.IsActive.HasValue && user.IsActive.GetValueOrDefault())
                _msg = "User still active on another session";


            Message = _msg;

            return _isValid;
        }

        [HttpPost]
        [Route("/[controller]/refresh")]
        public IActionResult RefreshToken(RefreshTokenModel tokenModel)
        {
            AuthResponseWrapper authResponseWrapper = new AuthResponseWrapper();

            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            RMSContext db = new RMSContext();
            var user = db.MsUser.First(x => x.ID == tokenModel.UserID);
            var userToken = db.MsUserToken.FirstOrDefault(x => x.USERID == tokenModel.UserID);
            long jwt_refresh_expire_in_days = db.MsParameter.First(x => x.PRM_TEXT == "jwt_refresh_expire_in_days").PRM_VALUE.Value;

            if (userToken == null || userToken.REFRESH_TOKEN != refreshToken || userToken.REFRESH_TOKEN_EXPIRY <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessTokenResult = CreateToken(user);
            var _newRefreshToken = GenerateRefreshToken();

            string _newAccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessTokenResult);
            DateTime _expiryToken = newAccessTokenResult.ValidTo;
            DateTime _expiryRefreshToken = _expiryToken.AddDays(jwt_refresh_expire_in_days);

            userToken.ACCESS_TOKEN = _newAccessToken;
            userToken.ACCESS_TOKEN_EXPIRY = _expiryToken;
            userToken.REFRESH_TOKEN = _newRefreshToken;
            userToken.REFRESH_TOKEN_EXPIRY = _expiryRefreshToken;
            //await _userManager.UpdateAsync(userToken);

            db.SaveChanges();

            authResponseWrapper.Message = "Success Refresh Token";
            authResponseWrapper.Status = "Success";
            authResponseWrapper.AccessToken = _newAccessToken;
            authResponseWrapper.ValidUntil = _expiryToken;
            authResponseWrapper.RefreshToken = _newRefreshToken;
            //return Json(new
            //{
            //    accessToken = _accessToken,
            //    refreshToken = newRefreshToken
            //}, new JsonSerializerOptions { PropertyNamingPolicy = null });

            return Json(authResponseWrapper, new JsonSerializerOptions { PropertyNamingPolicy = null });

        }
        public SecurityToken CreateToken(MsUser user)
        {
            RMSContext db = new RMSContext();
            long jwt_token_expire_in_minutes = db.MsParameter.First(x => x.PRM_TEXT == "jwt_token_expire_in_minutes").PRM_VALUE.Value;

            var issuer = "Maybank";
            var audience = "SurroundingApps";
            var key = Encoding.UTF8.GetBytes("bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca");

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
                );

            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub,user.USERNAME),
                        new Claim(JwtRegisteredClaimNames.Email,user.Email),
                        new Claim(JwtRegisteredClaimNames.Name,user.FullName),

                    });

            var expires = DateTime.UtcNow.AddMinutes(jwt_token_expire_in_minutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler { };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //var jwtToken = tokenHandler.WriteToken(token);
            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        [Authorize]
        [HttpPost]
        [Route("/[controller]/revoke/{username}")]
        public IActionResult Revoke(string username)
        {
            RMSContext db = new RMSContext();
            
            var user = db.MsUser.FirstOrDefault(x=>x.USERNAME == username);
            if (user == null) return BadRequest("Invalid username");

            var userToken = db.MsUserToken.FirstOrDefault(x => x.USERID == user.ID);
            userToken.REFRESH_TOKEN = null;
            userToken.REFRESH_TOKEN_EXPIRY = (DateTime?)null;
            db.SaveChanges();

            return Ok();
        }
    }
}
