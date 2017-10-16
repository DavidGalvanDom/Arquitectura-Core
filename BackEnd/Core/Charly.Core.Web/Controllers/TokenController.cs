//Module: User security
//Date: 27 / September / 2017
//Owner: David Galvan

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Net.Http;
using Newtonsoft.Json;


using Charly.Core.Web.Models;
using Charly.Core.Web.Entity;
using Charly.Core.Web.Data;
using Charly.Core.Web.TokenProvider;


namespace Charly.Core.Web.Controllers
{
    [Route("token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Create([FromBody]LoginDTO inputModel)
        {   
            string message = "";
            UserData userInfo = ValidateUser(inputModel.NTUser, inputModel.Password, ref message);

            if (userInfo == null)
                return Unauthorized();

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create())
                                .AddSubject(inputModel.NTUser)
                                .AddIssuer("All")
                                .AddAudience("Charly Core")
                                .AddClaim("title", userInfo.Title == null ? "" : userInfo.Title)
                                .AddClaim("name", userInfo.DisplayName == null ? "" : userInfo.DisplayName)
                                .AddClaim("Department", userInfo.Department == null ? "" : userInfo.Department)                                
                                .AddExpiry(60*24)
                                .Build();

            return Ok(token);
            //return Ok(token.Value);
        }


        /// <summary>
        /// Validate thta the user information is valid
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private UserData ValidateUser(string userLogin, string password, ref string message)
        {
            UserData userData = null;
            string serverDomain = _config.GetSection("urlActiveDirectory")["serverDomain"].ToString();
            try
            {
                //using (var cn = new LdapConnection())
                //{                    
                //    //Connect function will create a socket connection to the server - Port 389 for insecure and 3269 for secure    
                //    cn.Connect(serverDomain, LdapConnection.DEFAULT_PORT);

                //    //Bind function with null user dn and password value will perform anonymous bind to LDAP server 
                //    cn.Bind(userLogin, password);

                //    //Load user information 
                //    userData = SearchForUser(userLogin, password);
                //}
            }
            catch(Exception exp)
            {
                message = exp.Message;
                return null;
            }

            return userData;
        }

       
    }
}
