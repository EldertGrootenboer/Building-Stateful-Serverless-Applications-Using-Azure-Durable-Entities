using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace EPH.Functions
{
    public static class TicketsDurableFunction
    {
        [FunctionName("TicketsDurableFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            outputs.Add(await context.CallActivityAsync<string>("RegisterTicket", "John Smith"));
            outputs.Add(await context.CallActivityAsync<string>("RegisterTicket", "Jane Doe"));
            outputs.Add(await context.CallActivityAsync<string>("RegisterTicket", "Paul Brown"));

            return outputs;
        }
    }
}