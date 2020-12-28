using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Eventbus.Api.Init
{
    public class Oauth2Service
    {
        public string Iss { get; init; }

        public string Audience { get; init; }

        public Oauth2Service()
        {

        }

        public void Register(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().
                    AddAuthenticationSchemes("Bearer").
                    RequireAuthenticatedUser().
//                    RequireClaim("scope", "read").
                    Build();
            })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                    {
                        var json = new WebClient().DownloadString(parameters.ValidIssuer + "/.well-known/jwks.json");
                        var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                        return keys;
                    },

                    ValidIssuer = Iss,
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = Audience,
                    ValidateAudience = true
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseAuthentication();
            app.UseAuthorization();


        }
    }
}

