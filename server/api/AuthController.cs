using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTDemo
{
    [Route("v1/api/[controller]")]
    public class AuthController : Controller
    { 
        private IOptions<Audience> audienceOptions;
        private readonly IConfiguration config;


        public AuthController(IOptions<Audience> audienceOptions, IConfiguration config)
        {
            this.audienceOptions = audienceOptions;
            this.config = config;
        }

        [HttpPost]
        public JsonResult AuthAsync([FromBody]Parameters parameters)
        {
            Response response = new Response();

            if (parameters == null)
            {
                return Json(new Response
                {
                    code = "901",
                    message = "Null parameters",
                    data = null,
                    status = false
                });
            }

            if (parameters.grant_type == "password")
            {
                /***** Request Format ****/
                /*
                 * HTTP POST Call to: http://localhost:7000/v1/api/auth

                    Sample JSON Data in POST:
                    {
	                    "grant_type": "password",
	                    "client_id": "100",
	                    "client_secret": "0000",
	                    "username": "appsu",
	                    "password": "123456",
	                    "usertype": "user"
                    }

                    //Response Format:
                    {
                        "status": true,
                        "code": "999",
                        "message": "Ok",
                        "data": "{\n    \"access_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMDAiLCJqdGkiOiI5NmFmMTVlMC1mMmMyLTRkY2ItYjFmYy04MWQ0OWEyZjBmNTYiLCJpYXQiOiIwNy8zMS8yMDE5IDE1OjMzOjQyIiwiTmFtZSI6ImFwcHN1IiwiVXNlclR5cGUiOiJ1c2VyIiwibmJmIjoxNTY0NTg3MjIyLCJleHAiOjE1NjQ1ODcyODIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzAwMCIsImF1ZCI6IlJlc291cmNlU2VydmVyIn0.Rzl4ne2J6_4gpNI_KNJZ54pb5S0-kvB1nM_O9PW1dWk\",\n
                                        \"expires_in\": 60,\n
                                        \"refresh_token\": \"3f7759ffe87f4b7e985072f1b6acb4b8\"
                                 \n}"
                    }

                */
                response = DoPasswordAsync(parameters);
            }
            else if (parameters.grant_type == "refresh_token")
            {

                /***** Request Format ****/
                /*
                 * HTTP POST Call to: http://localhost:7000/v1/api/auth

                    Sample JSON Data in POST:
                    {
	                    "grant_type": "refresh_token",
	                    "client_id": "100",
	                    "client_secret": "0000",
	                    "refresh_token": "3f7759ffe87f4b7e985072f1b6acb4b8"     //use the refresh_token from above response for 1st time, then use refresh_token from it's own response below
                    }

                    //Response Format:
                    {
                        "status": true,
                        "code": "999",
                        "message": "Ok",
                        "data": "{\n    \"access_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMDAiLCJqdGkiOiI5NmFmMTVlMC1mMmMyLTRkY2ItYjFmYy04MWQ0OWEyZjBmNTYiLCJpYXQiOiIwNy8zMS8yMDE5IDE1OjMzOjQyIiwiTmFtZSI6ImFwcHN1IiwiVXNlclR5cGUiOiJ1c2VyIiwibmJmIjoxNTY0NTg3MjIyLCJleHAiOjE1NjQ1ODcyODIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzAwMCIsImF1ZCI6IlJlc291cmNlU2VydmVyIn0.Rzl4ne2J6_4gpNI_KNJZ54pb5S0-kvB1nM_O9PW1dWk\",\n
                                        \"expires_in\": 60,\n
                                        \"refresh_token\": \"e6817614bcab4aafa41259cd48029c20\"
                                 \n}"
                    }

                */


                response = DoRefreshTokenAsync(parameters);
            }
            else
            {
                return Json(new Response
                {
                    code = "904",
                    message = "Bad request",
                    data = null,
                    status = false
                });
            }

            return Json(response);
        }

        //scenario 1 ： get the access-token by username and password
        private Response DoPasswordAsync(Parameters parameters)
        {
            User user = new User();
            //SecurityDataHandler securityDataHandler = new SecurityDataHandler(config);
            TokenDataHandler tokenDataHandler = new TokenDataHandler(config);


            user.UserId = parameters.username;
            user.EmailAddress = parameters.username;
            user.Password = parameters.password;
            user.UserType = parameters.usertype;


            bool result = true; //hardcoded //securityDataHandler.VerifyLogin(user);
            if (result == false)
            {
                user = null;
            }

            if (user == null)
            {
                return new Response
                {
                    code = "902",
                    message = "Invalid user infomation",
                    data = null,
                    status = false
                };
            }

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var token = new RefreshToken
            {
                ClientId = parameters.client_id,
                Token = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0,
                UserName = user.UserId
            };

            //store the refresh_token 
            if (tokenDataHandler.UpdateToken(token))
            {
                return new Response
                {
                    code = "999",
                    message = "Ok",
                    data = GetJwt(parameters.client_id, user.UserId, user.UserType, refresh_token, audienceOptions.Value.ExpireMinutes),
                    status = true
                };
            }
            else
            {
                return new Response
                {
                    code = "909",
                    message = "Cannot add token to database",
                    data = null,
                    status = false
                };
            }
        }

        //scenario 2 ： get the access_token by refresh_token
        private Response DoRefreshTokenAsync(Parameters parameters)
        {
            TokenDataHandler tokenDataHandler = new TokenDataHandler(config);

            var token = tokenDataHandler.VerifyRefreshToken(parameters.username, parameters.refresh_token);

            if (token == false)
            {
                return new Response
                {
                    code = "905",
                    message = "Invalid session",
                    data = null,
                    status = false
                };
            }

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");


            var addFlag = tokenDataHandler.UpdateToken(new RefreshToken
            {
                ClientId = parameters.client_id,
                Token = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0,
                UserName = parameters.username
            });

            if (addFlag)
            {
                return new Response
                {
                    code = "999",
                    message = "Ok",
                    data = GetJwt(parameters.client_id, parameters.username, parameters.usertype, refresh_token, audienceOptions.Value.ExpireMinutes),
                    status = true
                };
            }
            else
            {
                return new Response
                {
                    code = "910",
                    message = "Cannot create a new token",
                    data = null,
                    status = false
                };
            }
        }

        /// <summary>
        /// Gets the jwt.
        /// </summary>
        /// <returns>The jwt.</returns>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="userName">User name.</param>
        /// <param name="refreshToken">Refresh token.</param>
        /// <param name="expireMinutes">Expire minutes.</param>
        private string GetJwt(string clientId, string userName, string userType, string refreshToken, int expireMinutes)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, clientId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                new Claim("Name", userName ?? ""),
                new Claim("UserType", userType ?? "")
            };

            var symmetricKeyAsBase64 = audienceOptions.Value.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: audienceOptions.Value.Iss,
                audience: audienceOptions.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(expireMinutes)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds,
                refresh_token = refreshToken,
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}