public class OrchestrationResult
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public OrchestrationResult()
    {

    }

    public OrchestrationResult(bool success = true, string message = "")
    {
        Success = success;
        Message = message;
    }
}