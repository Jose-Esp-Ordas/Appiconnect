using Appiconnect.Shared.Entities;
using Appiconnect.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Appiconnect.Web.Data
{
    public class Seeder
    {
        private readonly DataContext dataContext;

        private readonly IUserHelper userHelper;

        public Seeder( DataContext dataContext, IUserHelper userHelper) { 
            this.dataContext = dataContext;
            this.userHelper  = userHelper;
        }
        public async Task SeedAsync()
        {
            await
                dataContext.Database.EnsureCreatedAsync();
            await
                CheckCitiesAsync();
            await
                CheckRolesAsync();
            await CheckUserAsync("Brad", "Pitt", "brad.pitt@gmail.com", "2221111111","brad.png", UserType.Provider);
            await CheckUserAsync("Angelina", "Jolie", "angelina.jolie@gmail.com", "2222222222", "ange.png", UserType.Client);
            await CheckUserAsync("Jenifer", "Lopez", "jlo@gmail.com", "2223333333", "jlo.png", UserType.Provider);
            await CheckUserAsync("Dwayne", "Johnson", "laroca@gmail.com", "2221114111", "laroca.png", UserType.Admin);


        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phoneNumber, string photo, UserType role)
        {
            var user = await userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await dataContext.Cities.FirstOrDefaultAsync(x => x.Name == "Veracruz");
                if (city == null) {
                    city = await dataContext.Cities.FirstOrDefaultAsync();
                }
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    UserName = email,
                    City = city,
                    PhotoUrl = photo
                };
                IdentityResult x = await userHelper.AddUserAsync(user, "123456");
                await userHelper.AddUserToRoleAsync(user, role.ToString());
            }
            return user;
        }

        private async Task CheckRolesAsync()
        {
            await userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await userHelper.CheckRoleAsync(UserType.Client.ToString());
            await userHelper.CheckRoleAsync(UserType.Provider.ToString());
        }

        private async Task CheckCitiesAsync()
        {
            if (!dataContext.Cities.Any()) 
            {
                dataContext.Cities.Add(new City { Name = "San Andres Cholula" });
                dataContext.Cities.Add(new City { Name = "Puebla" });
                dataContext.Cities.Add(new City { Name = "Oaxaca" });
                dataContext.Cities.Add(new City { Name = "Chipilo" });
                dataContext.Cities.Add(new City { Name = "Atlixco" });
                dataContext.Cities.Add(new City { Name = "Las Vegas" });
                await dataContext.SaveChangesAsync();
            }
        }
    }
}
