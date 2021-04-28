public class ChangeRoomOrchestratorInput
{
    public string FromRoomNumber { get; set; }
    public string ToRoomNumber { get; set; }

    public ChangeRoomOrchestratorInput()
    {

    }

    public ChangeRoomOrchestratorInput(string fromRoomNumber, string toRoomNumber)
    {
        FromRoomNumber = fromRoomNumber;
        ToRoomNumber = toRoomNumber;
    }
}