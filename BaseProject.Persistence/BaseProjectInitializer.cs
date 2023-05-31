using BaseProject.Domain;
using BaseProject.Domain.Constants;
using System;
using System.Linq;
using Whoever.Entities.Common;

namespace BaseProject.Persistence
{
    public class BaseProjectInitializer
    {
        //private readonly Dictionary<int, Employee> Employees = new Dictionary<int, Employee>();
        //private readonly Dictionary<int, Supplier> Suppliers = new Dictionary<int, Supplier>();
        //private readonly Dictionary<int, Category> Categories = new Dictionary<int, Category>();
        //private readonly Dictionary<int, Shipper> Shippers = new Dictionary<int, Shipper>();
        //private readonly Dictionary<int, Product> Products = new Dictionary<int, Product>();

        public static void Initialize(BaseProjectDbContext context)
        {
            var initializer = new BaseProjectInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(BaseProjectDbContext context)
        {
            context.Database.EnsureCreated();

            SeedRoles(context);
            SeedUsers(context);
            //return; // Db has been seeded

            if (context.Roles.Any())
            {
                return; // Db has been seeded
            }

        }


        private void SeedRoles(BaseProjectDbContext context)
        {
            var roles = new[]
            {
                new Role { Id = RolesNames.UserManager.Id, Name = RolesNames.UserManager.Name, NormalizedName = RolesNames.UserManager.Name.ToUpper() },
                new Role { Id = RolesNames.Operator.Id, Name = RolesNames.Operator.Name, NormalizedName = RolesNames.Operator.Name.ToUpper() }
                
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        private void SeedUsers(BaseProjectDbContext context)
        {

            var userSuperAdmin = new User() { FirstName = "User", LastName = "Mananger",
                                              Email = "usermananger@gmail.com", UserName = "usermananger@gmail.com",
                                              NormalizedEmail = "usermananger@gmail.com".ToUpper(),
                                              NormalizedUserName = "usermananger@gmail.com".ToUpper(),
                                              CreationTime = DateTime.Now,
                                              ConcurrencyStamp = "72f1a523-bd84-4ce3-837e-7bb1ae0a858e",
                                              SecurityStamp = "5C2PYGYDGPP7T455XEEIDXXQUSECSQMJ",
                                              PasswordHash = "AQAAAAEAACcQAAAAEDaNjfiSXFkS4J9t4PThBC1uD8LkqiX7sah5wkqXhPz7Gqbvtkqtz+cVc5/k6CpwRg=="
            };
            context.Users.Add(userSuperAdmin);
            var userRole = new UserRole() {
                RoleId = RolesNames.UserManager.Id,
                UserId = userSuperAdmin.Id
            };
            context.UserRoles.Add(userRole);

            context.SaveChanges();
        }

        /*
         context.Database.OpenConnection();
    try
    {
        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Employees ON");
        context.SaveChanges();
        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Employees OFF");
    }
    finally
    {
        context.Database.CloseConnection();
    }*/
    }
}
