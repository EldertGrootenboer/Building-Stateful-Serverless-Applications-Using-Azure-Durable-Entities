using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace EPH.Functions
{
    public class ChangeBookedRoomOrchestrator
    {
        [FunctionName("ChangeBookedRoomOrchestrator")]
        public async Task<OrchestrationResult> Orchestrate(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            var input = context.GetInput<ChangeRoomOrchestratorInput>();

            if (!context.IsReplaying)
            {
                log.LogInformation("Starting room booking change orchestrator from room {FromRoomNumber} to room {ToRoomNumber}", input.FromRoomNumber, input.ToRoomNumber);
            }

            var fromRoomEntityId = new EntityId(nameof(RoomEntity), input.FromRoomNumber);
            var toRoomEntityId = new EntityId(nameof(RoomEntity), input.ToRoomNumber);
           
            // Lock entities to prevent race condition
            using (await context.LockAsync(fromRoomEntityId, toRoomEntityId))
            {
                var fromRoomEntityProxy = context.CreateEntityProxy<IRoomEntity>(fromRoomEntityId);
                var toRoomEntityProxy = context.CreateEntityProxy<IRoomEntity>(toRoomEntityId);

                var toRoomIsCurrentlyBooked = await toRoomEntityProxy.IsCurrentlyBookedAsync();
                if (toRoomIsCurrentlyBooked)
                {
                    return new OrchestrationResult(false, "Room already booked!");
                }

                await fromRoomEntityProxy.UnBookRoomAsync();
                await toRoomEntityProxy.BookRoomAsync();
            }

            return new OrchestrationResult(true, "Room booked!");
        }
    }
}