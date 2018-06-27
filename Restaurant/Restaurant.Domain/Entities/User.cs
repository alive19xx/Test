using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public UserPosition Position { get; set; }
        
        public string FullName
        {
            get
            {
                var fullName = new StringBuilder();
                fullName.Append(FirstName);
                if (MiddleName != null)
                    fullName.Append(" ").Append(MiddleName);
                fullName.Append(" ").Append(SecondName);

                return fullName.ToString();
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.Role,this.Position.ToString()));
            return userIdentity;
        }
    }
}
