﻿using Entities.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MvcBox.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcBox.AuthHelpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IAuthService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                Guid userId;
                var stringClaimValue = securityToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
                var result = Guid.TryParse(stringClaimValue, out userId);
                //var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //Guid userId;
                //var result = Guid.TryParse(context.User.Identity.Name, out userId);
                //var user = await userService.GetUser(Guid.Parse("A403FBC2-766A-47CE-B2D5-19E3D1B77B31"));
                //var userId = await userService.GetUserIdByUserName(context.User.Identity.Name);

                //вызов асинхронной задачи, которая выполняется в отдельном потоке
                var user = userService.GetUser(userId);

                // attach user to context on successful jwt validation
                //ожидание результата выполнения задачи
                context.Items["User"] = user.Result;
                context.Items["UserId"] = userId;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
