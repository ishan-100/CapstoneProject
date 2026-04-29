using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser
{
    public string UserPreference { get; set; }
}
