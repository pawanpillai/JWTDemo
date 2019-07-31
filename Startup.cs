using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JWTDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Audience>(Configuration.GetSection("Audience"));
            ConfigureJwtAuthService(services);
            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);   //getting connection string from appsettings.json

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            //app.UseHttpsRedirection();
            //use the authentication  
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();

            ////handle client side routes that fall through
            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/html";
            //    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));  //redirecting to angular's wwwroot/index.html
            //});
        }

        private void ConfigureJwtAuthService(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT issuer (Iss) claim
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],

                // Validate the JWT audience (Aud) claim
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],

                // Validate token expiration
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });

        }
    }
}
