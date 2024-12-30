using Microsoft.AspNetCore.Identity;

namespace EllionPlatform.CoreAPI.Models;

public class Role : IdentityRole
{
    public string Description { get; set; } = String.Empty;
}
