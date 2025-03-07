namespace SqlAccountRestAPI.ViewModels.Responses;
/// <summary>
/// Error response model.
/// </summary>
public class ErrorResponse
{
    /// <example>Login failed: Invalid user name or password</example>
    public string ErrorMessage { get; set; } = null!;
}
