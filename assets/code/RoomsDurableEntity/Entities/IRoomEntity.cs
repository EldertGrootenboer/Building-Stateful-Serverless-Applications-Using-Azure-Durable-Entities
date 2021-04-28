using System.Threading.Tasks;

namespace EPH.Functions
{
    public interface IRoomEntity
    { 
        Task BookRoomAsync();
        Task UnBookRoomAsync();
        Task<string> GetAttendeeInfoAsync(string username);
        Task<bool> IsCurrentlyBookedAsync();
    }
}
