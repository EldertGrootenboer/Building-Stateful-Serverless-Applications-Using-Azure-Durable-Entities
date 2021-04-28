using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace EPH.Functions
{
    public class HttpStartClient
    { 
        [FunctionName("HttpStartClient")]
        public async Task<IActionResult> ChangeBookedRoom(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient durableOrchestrationClient,
            ILogger log)
        {
            var fromRoomNumber = req.Query["FromRoomNumber"];
            var toRoomNumber = req.Query["ToRoomNumber"]; 

            log.LogInformation("Got room booking change request for room {fromRoomNumber} to room {toRoomNumber}", fromRoomNumber, toRoomNumber);

            var orchestrationId = await durableOrchestrationClient.StartNewAsync("ChangeBookedRoomOrchestrator", new ChangeRoomOrchestratorInput(fromRoomNumber, toRoomNumber));

            return durableOrchestrationClient.CreateCheckStatusResponse(req, orchestrationId);
        }
    }
}