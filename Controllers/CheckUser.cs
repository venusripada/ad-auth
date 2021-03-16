using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using ad_auth.Models;
using Microsoft.Extensions.Options;

namespace ad_auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckUser : ControllerBase
    {
      

        private readonly ILogger<CheckUser> _logger;
        private readonly ConfigSettings _settings;
        public CheckUser(IOptions<ConfigSettings> settings, ILogger<CheckUser> logger)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        [HttpPost]
        public async Task<RootResponse> Post(RootRequest request)
        {
            string validationResponse = "UNVERIFIED" ;
           
             using (var context = new PrincipalContext(ContextType.Domain, _settings.LdapDomain, _settings.ADServiceAccount, _settings.ADServiceAccountPwd))
                {
                    if(request == null || request.data == null || request.data.context == null || 
                            request.data.context.credential == null || request.data.context.credential.username == null ||
                            request.data.context.credential.password == null ) 
                        return new RootResponse();

                     _logger.LogInformation("Verifying password for:" + request.data.context.credential.username);
                    if (context.ValidateCredentials(request.data.context.credential.username, request.data.context.credential.password))
                    {
                      _logger.LogInformation("Verified password successfully for:" + request.data.context.credential.username);
                      validationResponse  = "VERIFIED";
                    }
                }
            Command command = new Command(){
                type = "com.okta.action.update",
                value = new Value(){  credential = validationResponse }
            };
            RootResponse response = new RootResponse();
            response.commands = new List<Command>();
            response.commands.Add(command);
          return response;
        }
    }
}
