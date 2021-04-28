using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace EPH.Functions
{
    public static class RegisterTicket
    {
        [FunctionName("RegisterTicket")]
        public static string Register([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Registered ticket for {name}.");
            return $"Thank you for your registration {name}!";
        }
    }
}