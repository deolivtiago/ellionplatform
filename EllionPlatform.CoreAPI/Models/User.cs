using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EllionPlatform.CoreAPI.Models;

public class User : IdentityUser
{
    [Required] public string FullName { get; set; } = string.Empty;
}
