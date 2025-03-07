using System.ComponentModel.DataAnnotations;

namespace SqlAccountRestAPI.Controllers;

public class LoginRequest
{
    /// <example>ADMIN</example>
    [Required]
    public string Username { get; set; } = null!;
    /// <example>ADMIN</example>
    [Required]
    public string Password { get; set; } = null!;
}