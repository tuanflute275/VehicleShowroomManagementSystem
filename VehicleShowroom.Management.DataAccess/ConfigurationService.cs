using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Entities;
namespace VehicleShowroom.Management.DataAccess
{
    public static class ConfigurationService
    {
        public static async Task AutoMigration(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

               await appContext.Database.MigrateAsync();
                await webApplication.SeedData(webApplication.Configuration);
            }
        }

        public static async Task SeedData(this WebApplication webApplication, IConfiguration configuration)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Data User
                if (!appContext.Users.Any())
                {
                    // Insert the seed data for Users table
                    appContext.Users.AddRange(
                        new User
                        {
                            Username = "admin",
                            FullName = "admin",
                            Email = "admin@gmail.com",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye", // Example hashed password
                            Role = 1,
                            PhoneNumber = "0123456789",
                            Address = "HN",
                            Gender = 1,
                            Status = 0
                        },
                        new User
                        {
                            Username = "employee",
                            FullName = "employee",
                            Email = "employee@gmail.com",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye", // Example hashed password
                            Role = 2,
                            PhoneNumber = "9876543210",
                            Address = "HN",
                            Gender = 2,
                            Status = 0
                        },
                        new User
                        {
                            Username = "invoice",
                            FullName = "invoice",
                            Email = "invoice@gmail.com",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye", // Example hashed password
                            Role = 3,
                            PhoneNumber = "1234567890",
                            Address = "HN",
                            Gender = 2,
                            Status = 0
                        },
                        new User
                        {
                            Username = "user",
                            FullName = "user",
                            Email = "user@gmail.com",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye", // Example hashed password
                            Role = 0,
                            PhoneNumber = "345678123",
                            Address = "HN",
                            Gender = 1,
                            Status = 0
                        }
                    );
                    appContext.Users.AddRange(
                        new User
                        {
                            Username = "johndoe",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye",
                            FullName = "John Doe",
                            Avatar = "/images/johndoe.jpg",
                            Email = "john.doe@example.com",
                            PhoneNumber = "1234567890",
                            Address = "123 Main St",
                            Gender = 1,
                            Department = "HR",
                            JobTitle = "Manager",
                            HireDate = DateTime.Parse("2020-01-15"),
                            Salary = 80000,
                            Status = 1,
                            DateOfBirth = DateTime.Parse("1985-05-20"),
                            Nationality = "USA",
                            EmergencyContact = "Jane Doe, 987654321",
                            Role = 0
                        },
                        new User
                        {
                            Username = "janesmith",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye",
                            FullName = "Jane Smith",
                            Avatar = "/images/janesmith.jpg",
                            Email = "jane.smith@example.com",
                            PhoneNumber = "9876543210",
                            Address = "456 Elm St",
                            Gender = 2,
                            Department = "Finance",
                            JobTitle = "Analyst",
                            HireDate = DateTime.Parse("2021-03-20"),
                            Salary = 60000,
                            Status = 1,
                            DateOfBirth = DateTime.Parse("1990-07-15"),
                            Nationality = "Canada",
                            EmergencyContact = "John Smith, 123456789",
                            Role = 0
                        },
                         new User
                         {
                             Username = "michaelbrown",
                             Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye",
                             FullName = "Michael Brown",
                             Avatar = "/images/michaelbrown.jpg",
                             Email = "michael.brown@example.com",
                             PhoneNumber = "1122334455",
                             Address = "789 Oak St",
                             Gender = 1,
                             Department = "IT",
                             JobTitle = "Developer",
                             HireDate = DateTime.Parse("2019-08-10"),
                             Salary = 75000.00M,
                             Status = 1,
                             DateOfBirth = DateTime.Parse("1992-09-12"),
                             Nationality = "UK",
                             EmergencyContact = "Sarah Brown, 2233445566",
                             Role = 0
                         },
                        new User
                        {
                            Username = "emilydavis",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye",
                            FullName = "Emily Davis",
                            Avatar = "/images/emilydavis.jpg",
                            Email = "emily.davis@example.com",
                            PhoneNumber = "3344556677",
                            Address = "321 Pine St",
                            Gender = 2,
                            Department = "Marketing",
                            JobTitle = "Coordinator",
                            HireDate = DateTime.Parse("2022-06-01"),
                            Salary = 50000.00M,
                            Status = 1,
                            DateOfBirth = DateTime.Parse("1995-12-25"),
                            Nationality = "Australia",
                            EmergencyContact = "David Davis, 9988776655",
                            Role = 0
                        },
                        new User
                        {
                            Username = "chrisjohnson",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye",
                            FullName = "Chris Johnson",
                            Avatar = "/images/chrisjohnson.jpg",
                            Email = "chris.johnson@example.com",
                            PhoneNumber = "5566778899",
                            Address = "654 Birch St",
                            Gender = 1,
                            Department = "Sales",
                            JobTitle = "Executive",
                            HireDate = DateTime.Parse("2018-11-15"),
                            Salary = 85000.00M,
                            Status = 1,
                            DateOfBirth = DateTime.Parse("1988-03-10"),
                            Nationality = "USA",
                            EmergencyContact = "Anna Johnson, 7766554433",
                            Role = 0
                        },
                        new User
                        {
                            Username = "laurawilson",
                            Password = "$2a$12$eefyE/f6G0AKFLfVl3B66.T6QfgGNHPIZLlDp.v527EuwVruYlTye",
                            FullName = "Laura Wilson",
                            Avatar = "/images/laurawilson.jpg",
                            Email = "laura.wilson@example.com",
                            PhoneNumber = "7788990011",
                            Address = "987 Maple St",
                            Gender = 2,
                            Department = "Admin",
                            JobTitle = "Assistant",
                            HireDate = DateTime.Parse("2023-01-01"),
                            Salary = 40000.00M,
                            Status = 1,
                            DateOfBirth = DateTime.Parse("1998-01-18"),
                            Nationality = "UK",
                            EmergencyContact = "Paul Wilson, 8899001122",
                            Role = 0
                        }
                    // Add other records as needed
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data Supplier
                if (!appContext.Suppliers.Any())
                {
                    // Insert the seed data for Suppliers table
                    appContext.Suppliers.AddRange(
                        new Supplier
                        {
                            SupplierName = "Tech Solutions Ltd.",
                            ContactPerson = "John Doe",
                            PhoneNumber = "0912345678",
                            Email = "john.doe@techsolutions.com",
                            Website = "https://www.techsolutions.com",
                            TaxCode = "1234567890",
                            Address = "123 Tech Street, HCM City",
                            BankAccount = "123456789012345",
                            BankName = "Tech Bank",
                            ContractNumber = "CONTRACT12345",
                            ContractDate = DateTime.Parse("2024-01-15"),
                            Status = "Active",
                            Notes = "Important Supplier"
                        },
                        new Supplier
                        {
                            SupplierName = "Global Electronics",
                            ContactPerson = "Jane Smith",
                            PhoneNumber = "0923456789",
                            Email = "jane.smith@globelec.com",
                            Website = "https://www.globelec.com",
                            TaxCode = "9876543210",
                            Address = "456 Global Ave, HN City",
                            BankAccount = "987654321012345",
                            BankName = "Global Bank",
                            ContractNumber = "CONTRACT54321",
                            ContractDate = DateTime.Parse("2024-02-20"),
                            Status = "Active",
                            Notes = "Regular Vendor"
                        },
                         new Supplier
                         {
                             SupplierName = "Home Appliances Corp.",
                             ContactPerson = "Alice Brown",
                             PhoneNumber = "0934567890",
                             Email = "alice.brown@homeappliances.com",
                             Website = "https://www.homeappliances.com",
                             TaxCode = "112233445512345",
                             Address = "789 Appliance Rd, HCM City",
                             BankAccount = "112233445512345",
                             BankName = "Home Bank",
                             ContractNumber = "CONTRACT98765",
                             ContractDate = DateTime.Parse("2024-03-01"),
                             Status = "Active",
                             Notes = "Supplier for electronics"
                         },
                          new Supplier
                          {
                              SupplierName = "Eco Products Ltd.",
                              ContactPerson = "Robert White",
                              PhoneNumber = "0945678901",
                              Email = "robert.white@ecoproducts.com",
                              Website = "https://www.ecoproducts.com",
                              TaxCode = "2233445566",
                              Address = "101 Eco Lane, HN City",
                              BankAccount = "223344556612345",
                              BankName = "Eco Bank",
                              ContractNumber = "CONTRACT24680",
                              ContractDate = DateTime.Parse("2024-04-12"),
                              Status = "Active",
                              Notes = "Sustainable supplier"
                          },
                            new Supplier
                            {
                                SupplierName = "Luxury Goods Ltd.",
                                ContactPerson = "Emily Green",
                                PhoneNumber = "0956789012",
                                Email = "emily.green@luxurygoods.com",
                                Website = "https://www.luxurygoods.com",
                                TaxCode = "3344556677",
                                Address = "202 Luxury Blvd, HCM City",
                                BankAccount = "334455667712345",
                                BankName = "Luxury Bank",
                                ContractNumber = "CONTRACT13579",
                                ContractDate = DateTime.Parse("2024-05-25"),
                                Status = "Active",
                                Notes = "High-end supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Food & Beverage Co.",
                                ContactPerson = "Chris Black",
                                PhoneNumber = "0967890123",
                                Email = "chris.black@foodbeverage.com",
                                Website = "https://www.foodbeverage.com",
                                TaxCode = "4455667788",
                                Address = "303 Food Street, HN City",
                                BankAccount = "445566778812345",
                                BankName = "Food Bank",
                                ContractNumber = "CONTRACT86420",
                                ContractDate = DateTime.Parse("2024-06-10"),
                                Status = "Active",
                                Notes = "Major food supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Office Supplies Ltd.",
                                ContactPerson = "Patricia Clark",
                                PhoneNumber = "0978901234",
                                Email = "patricia.clark@officesupplies.com",
                                Website = "https://www.officesupplies.com",
                                TaxCode = "5566778899",
                                Address = "404 Office Lane, HCM City",
                                BankAccount = "556677889912345",
                                BankName = "Office Bank",
                                ContractNumber = "CONTRACT75319",
                                ContractDate = DateTime.Parse("2024-07-01"),
                                Status = "Active",
                                Notes = "Office equipment supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Industrial Tools Inc.",
                                ContactPerson = "Michael Davis",
                                PhoneNumber = "0989012345",
                                Email = "michael.davis@industrialtools.com",
                                Website = "https://www.industrialtools.com",
                                TaxCode = "6677889900",
                                Address = "505 Industry Rd, HN City",
                                BankAccount = "667788990012345",
                                BankName = "Industrial Bank",
                                ContractNumber = "CONTRACT95124",
                                ContractDate = DateTime.Parse("2024-08-15"),
                                Status = "Active",
                                Notes = "Tools for manufacturing"
                            },
                            new Supplier
                            {
                                SupplierName = "Automotive Parts Ltd.",
                                ContactPerson = "Sarah Wilson",
                                PhoneNumber = "0990123456",
                                Email = "sarah.wilson@auto-parts.com",
                                Website = "https://www.auto-parts.com",
                                TaxCode = "7788990011",
                                Address = "606 Car Blvd, HCM City",
                                BankAccount = "778899001112345",
                                BankName = "Auto Bank",
                                ContractNumber = "CONTRACT85246",
                                ContractDate = DateTime.Parse("2024-09-05"),
                                Status = "Active",
                                Notes = "Car parts supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Furniture Co.",
                                ContactPerson = "David Lee",
                                PhoneNumber = "0911122334",
                                Email = "david.lee@furnitureco.com",
                                Website = "https://www.furnitureco.com",
                                TaxCode = "8899001122",
                                Address = "707 Furniture Street, HN City",
                                BankAccount = "889900112212345",
                                BankName = "Furniture Bank",
                                ContractNumber = "CONTRACT74185",
                                ContractDate = DateTime.Parse("2024-10-30"),
                                Status = "Active",
                                Notes = "Furniture vendor"
                            },
                            new Supplier
                            {
                                SupplierName = "Health Products Inc.",
                                ContactPerson = "Karen Martinez",
                                PhoneNumber = "0922233445",
                                Email = "karen.martinez@healthproducts.com",
                                Website = "https://www.healthproducts.com",
                                TaxCode = "9900112233",
                                Address = "808 Health Rd, HCM City",
                                BankAccount = "990011223312345",
                                BankName = "Health Bank",
                                ContractNumber = "CONTRACT85247",
                                ContractDate = DateTime.Parse("2024-11-18"),
                                Status = "Active",
                                Notes = "Medical supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Technology Group Ltd.",
                                ContactPerson = "James Taylor",
                                PhoneNumber = "0933344556",
                                Email = "james.taylor@techgroup.com",
                                Website = "https://www.techgroup.com",
                                TaxCode = "1001222333",
                                Address = "909 Tech Lane, HN City",
                                BankAccount = "100122233312345",
                                BankName = "Tech Group Bank",
                                ContractNumber = "CONTRACT96374",
                                ContractDate = DateTime.Parse("2024-12-01"),
                                Status = "Active",
                                Notes = "Software and hardware supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Textile Co.",
                                ContactPerson = "Linda Johnson",
                                PhoneNumber = "0944455667",
                                Email = "linda.johnson@textileco.com",
                                Website = "https://www.textileco.com",
                                TaxCode = "1102333444",
                                Address = "1010 Fabric St, HCM City",
                                BankAccount = "110233344412345",
                                BankName = "Textile Bank",
                                ContractNumber = "CONTRACT12346",
                                ContractDate = DateTime.Parse("2024-01-10"),
                                Status = "Active",
                                Notes = "Supplier of fabrics"
                            },
                            new Supplier
                            {
                                SupplierName = "Smart Devices Inc.",
                                ContactPerson = "Oliver Thomas",
                                PhoneNumber = "0955566778",
                                Email = "oliver.thomas@smartdevices.com",
                                Website = "https://www.smartdevices.com",
                                TaxCode = "2203444555",
                                Address = "121 Smart St, HN City",
                                BankAccount = "220344455512345",
                                BankName = "Smart Bank",
                                ContractNumber = "CONTRACT75320",
                                ContractDate = DateTime.Parse("2024-02-12"),
                                Status = "Active",
                                Notes = "Supplier for electronics"
                            },
                            new Supplier
                            {
                                SupplierName = "Construction Materials Ltd.",
                                ContactPerson = "Sophia Moore",
                                PhoneNumber = "0966677889",
                                Email = "sophia.moore@constructionmat.com",
                                Website = "https://www.constructionmat.com",
                                TaxCode = "3304555666",
                                Address = "131 Build Rd, HCM City",
                                BankAccount = "330455566612345",
                                BankName = "Build Bank",
                                ContractNumber = "CONTRACT65432",
                                ContractDate = DateTime.Parse("2024-03-15"),
                                Status = "Active",
                                Notes = "Building materials supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Packaging Solutions",
                                ContactPerson = "Benjamin Harris",
                                PhoneNumber = "0977788990",
                                Email = "benjamin.harris@packaging.com",
                                Website = "https://www.packaging.com",
                                TaxCode = "4405666777",
                                Address = "141 Package Ave, HN City",
                                BankAccount = "440566677712345",
                                BankName = "Pack Bank",
                                ContractNumber = "CONTRACT25863",
                                ContractDate = DateTime.Parse("2024-04-05"),
                                Status = "Active",
                                Notes = "Packaging products vendor"
                            },
                            new Supplier
                            {
                                SupplierName = "Clean Energy Co.",
                                ContactPerson = "Charlotte Lee",
                                PhoneNumber = "0988899001",
                                Email = "charlotte.lee@cleanenergy.com",
                                Website = "https://www.cleanenergy.com",
                                TaxCode = "5506777888",
                                Address = "151 Green St, HCM City",
                                BankAccount = "550677788812345",
                                BankName = "Green Bank",
                                ContractNumber = "CONTRACT74162",
                                ContractDate = DateTime.Parse("2024-05-20"),
                                Status = "Active",
                                Notes = "Renewable energy supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Shipping & Logistics Inc.",
                                ContactPerson = "Daniel Walker",
                                PhoneNumber = "0919900112",
                                Email = "daniel.walker@shippinglogistics.com",
                                Website = "https://www.shippinglogistics.com",
                                TaxCode = "6607888999",
                                Address = "161 Port Rd, HN City",
                                BankAccount = "660788899912345",
                                BankName = "Ship Bank",
                                ContractNumber = "CONTRACT17453",
                                ContractDate = DateTime.Parse("2024-06-15"),
                                Status = "Active",
                                Notes = "Logistics and shipping company"
                            },
                            new Supplier
                            {
                                SupplierName = "Printing Press Ltd.",
                                ContactPerson = "Grace King",
                                PhoneNumber = "0921001223",
                                Email = "grace.king@printingpress.com",
                                Website = "https://www.printingpress.com",
                                TaxCode = "7708990011",
                                Address = "171 Print St, HCM City",
                                BankAccount = "770899001112345",
                                BankName = "Print Bank",
                                ContractNumber = "CONTRACT96385",
                                ContractDate = DateTime.Parse("2024-07-12"),
                                Status = "Active",
                                Notes = "Printing and press supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Software Development Corp.",
                                ContactPerson = "William Scott",
                                PhoneNumber = "0932112334",
                                Email = "william.scott@softwaredev.com",
                                Website = "https://www.softwaredev.com",
                                TaxCode = "8809991122",
                                Address = "181 Code Rd, HN City",
                                BankAccount = "880999112212345",
                                BankName = "Code Bank",
                                ContractNumber = "CONTRACT75392",
                                ContractDate = DateTime.Parse("2024-08-20"),
                                Status = "Active",
                                Notes = "Software development supplier"
                            },
                            new Supplier
                            {
                                SupplierName = "Packaging Materials Ltd.",
                                ContactPerson = "Isabella Perez",
                                PhoneNumber = "0943223445",
                                Email = "isabella.perez@packagingmaterials.com",
                                Website = "https://www.packagingmaterials.com",
                                TaxCode = "9900112233",
                                Address = "191 Package Blvd, HCM City",
                                BankAccount = "990011223312345",
                                BankName = "Package Bank",
                                ContractNumber = "CONTRACT84125",
                                ContractDate = DateTime.Parse("2024-09-30"),
                                Status = "Active",
                                Notes = "Material packaging supplier"
                            }
                    // Add the remaining suppliers as needed
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data company
                if (!appContext.Companies.Any())
                {
                    // Insert the seed data for Companies table
                    appContext.Companies.AddRange(
                        new Company
                        {
                            CompanyName = "Honda Motors",
                            PhoneNumber = "0123456789",
                            Email = "contact@honda.com",
                            Status = "Active",
                        },
                        new Company
                        {
                            CompanyName = "Toyota Motors",
                            PhoneNumber = "0987654321",
                            Email = "contact@toyota.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Ford Motor Company",
                            PhoneNumber = "0234567891",
                            Email = "contact@ford.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "General Motors",
                            PhoneNumber = "0345678912",
                            Email = "contact@gm.com",
                            Status = "Inactive"
                        },
                        new Company
                        {
                            CompanyName = "BMW Group",
                            PhoneNumber = "0456789123",
                            Email = "contact@bmw.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Mercedes-Benz",
                            PhoneNumber = "0567891234",
                            Email = "contact@mercedes.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Volkswagen Group",
                            PhoneNumber = "0678912345",
                            Email = "contact@volkswagen.com",
                            Status = "Inactive"
                        },
                        new Company
                        {
                            CompanyName = "Hyundai Motor",
                            PhoneNumber = "0789123456",
                            Email = "contact@hyundai.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Nissan Motor Corporation",
                            PhoneNumber = "0891234567",
                            Email = "contact@nissan.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Fiat Chrysler Automobiles",
                            PhoneNumber = "0901234568",
                            Email = "contact@fiat.com",
                            Status = "Inactive"
                        },
                        new Company
                        {
                            CompanyName = "Tesla Inc.",
                            PhoneNumber = "0912345678",
                            Email = "contact@tesla.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Renault",
                            PhoneNumber = "0123345678",
                            Email = "contact@renault.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Peugeot",
                            PhoneNumber = "0234456789",
                            Email = "contact@peugeot.com",
                            Status = "Inactive"
                        },
                        new Company
                        {
                            CompanyName = "Mazda",
                            PhoneNumber = "0345567891",
                            Email = "contact@mazda.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Suzuki",
                            PhoneNumber = "0456678912",
                            Email = "contact@suzuki.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Mitsubishi Motors",
                            PhoneNumber = "0567789123",
                            Email = "contact@mitsubishi.com",
                            Status = "Inactive"
                        },
                        new Company
                        {
                            CompanyName = "Volvo Cars",
                            PhoneNumber = "0678891234",
                            Email = "contact@volvo.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Kia Motors",
                            PhoneNumber = "0789901234",
                            Email = "contact@kia.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Subaru Corporation",
                            PhoneNumber = "0891012345",
                            Email = "contact@subaru.com",
                            Status = "Active"
                        },
                        new Company
                        {
                            CompanyName = "Daimler AG",
                            PhoneNumber = "0912123456",
                            Email = "contact@daimler.com",
                            Status = "Active"
                        }
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data Vehicle
                if (!appContext.Vehicles.Any())
                {
                    // Insert the seed data for Vehicles table
                    appContext.Vehicles.AddRange(
                        new Vehicle
                        {
                            ModelNumber = "MDL2024",
                            Name = "Toyota Corolla",
                            Slug = "toyota-corolla",
                            Image = "toyota-corolla.jpg",
                            Status = "Available",
                            Description = "Compact sedan with advanced safety features.",
                            SupplierId = 1,
                            CompanyId = 1,
                            EngineNumber = "ENG12345",
                            ChassisNumber = "CHS67890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "White",
                            Price = 20000,
                            Mileage = 15000,
                            ManufactureYear = 2022
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2023",
                            Name = "Honda Civic",
                            Slug = "honda-civic",
                            Image = "honda-civic.jpg",
                            Status = "Available",
                            Description = "Stylish and fuel-efficient compact car.",
                            SupplierId = 1,
                            CompanyId = 1,
                            EngineNumber = "ENG22345",
                            ChassisNumber = "CHS77890",
                            FuelType = "Petrol",
                            TransmissionType = "Manual",
                            Color = "Black",
                            Price = 22000,
                            Mileage = 18000,
                            ManufactureYear = 2021
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2022",
                            Name = "Ford Mustang",
                            Slug = "ford-mustang",
                            Image = "ford-mustang.jpg",
                            Status = "Available",
                            Description = "Classic muscle car with V8 engine.",
                            SupplierId = 2,
                            CompanyId = 1,
                            EngineNumber = "ENG32345",
                            ChassisNumber = "CHS87890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "Red",
                            Price = 35000,
                            Mileage = 10000,
                            ManufactureYear = 2020
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2025",
                            Name = "Chevrolet Bolt",
                            Slug = "chevrolet-bolt",
                            Image = "chevrolet-bolt.jpg",
                            Status = "Available",
                            Description = "Affordable electric car with great range.",
                            SupplierId = 3,
                            CompanyId = 2,
                            EngineNumber = "ENG42345",
                            ChassisNumber = "CHS97890",
                            FuelType = "Electric",
                            TransmissionType = "Automatic",
                            Color = "Blue",
                            Price = 31000,
                            Mileage = 5000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2026",
                            Name = "Tesla Model 3",
                            Slug = "tesla-model-3",
                            Image = "tesla-model-3.jpg",
                            Status = "Available",
                            Description = "Premium electric sedan with autopilot.",
                            SupplierId = 3,
                            CompanyId = 2,
                            EngineNumber = "ENG52345",
                            ChassisNumber = "CHS07890",
                            FuelType = "Electric",
                            TransmissionType = "Automatic",
                            Color = "Silver",
                            Price = 45000,
                            Mileage = 12000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2027",
                            Name = "BMW X5",
                            Slug = "bmw-x5",
                            Image = "bmw-x5.jpg",
                            Status = "Available",
                            Description = "Luxury SUV with advanced features.",
                            SupplierId = 4,
                            CompanyId = 3,
                            EngineNumber = "ENG62345",
                            ChassisNumber = "CHS17890",
                            FuelType = "Diesel",
                            TransmissionType = "Automatic",
                            Color = "Black",
                            Price = 60000,
                            Mileage = 8000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2028",
                            Name = "Mercedes-Benz C-Class",
                            Slug = "mercedes-c-class",
                            Image = "mercedes-c-class.jpg",
                            Status = "Available",
                            Description = "Executive sedan with luxurious interiors.",
                            SupplierId = 4,
                            CompanyId = 3,
                            EngineNumber = "ENG72345",
                            ChassisNumber = "CHS27890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "Gray",
                            Price = 55000,
                            Mileage = 9500,
                            ManufactureYear = 2022
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2029",
                            Name = "Audi A4",
                            Slug = "audi-a4",
                            Image = "audi-a4.jpg",
                            Status = "Available",
                            Description = "Premium sedan with Quattro AWD.",
                            SupplierId = 4,
                            CompanyId = 3,
                            EngineNumber = "ENG82345",
                            ChassisNumber = "CHS37890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "Blue",
                            Price = 53000,
                            Mileage = 8700,
                            ManufactureYear = 2022
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2030",
                            Name = "Hyundai Sonata",
                            Slug = "hyundai-sonata",
                            Image = "hyundai-sonata.jpg",
                            Status = "Available",
                            Description = "Midsize sedan with hybrid options.",
                            SupplierId = 5,
                            CompanyId = 4,
                            EngineNumber = "ENG92345",
                            ChassisNumber = "CHS47890",
                            FuelType = "Hybrid",
                            TransmissionType = "Automatic",
                            Color = "White",
                            Price = 27000,
                            Mileage = 14000,
                            ManufactureYear = 2021
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2031",
                            Name = "Kia Seltos",
                            Slug = "kia-seltos",
                            Image = "kia-seltos.jpg",
                            Status = "Available",
                            Description = "Compact SUV with modern features.",
                            SupplierId = 5,
                            CompanyId = 4,
                            EngineNumber = "ENG03345",
                            ChassisNumber = "CHS57890",
                            FuelType = "Petrol",
                            TransmissionType = "Manual",
                            Color = "Yellow",
                            Price = 20000,
                            Mileage = 2000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2032",
                            Name = "Nissan Rogue",
                            Slug = "nissan-rogue",
                            Image = "nissan-rogue.jpg",
                            Status = "Available",
                            Description = "Versatile SUV with all-wheel drive.",
                            SupplierId = 6,
                            CompanyId = 4,
                            EngineNumber = "ENG13345",
                            ChassisNumber = "CHS67890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "Green",
                            Price = 28000,
                            Mileage = 8000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2033",
                            Name = "Jeep Wrangler",
                            Slug = "jeep-wrangler",
                            Image = "jeep-wrangler.jpg",
                            Status = "Available",
                            Description = "Off-road SUV with removable roof.",
                            SupplierId = 6,
                            CompanyId = 5,
                            EngineNumber = "ENG23345",
                            ChassisNumber = "CHS77890",
                            FuelType = "Diesel",
                            TransmissionType = "Manual",
                            Color = "Red",
                            Price = 35000,
                            Mileage = 7000,
                            ManufactureYear = 2022
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2034",
                            Name = "Mazda CX-5",
                            Slug = "mazda-cx-5",
                            Image = "mazda-cx-5.jpg",
                            Status = "Available",
                            Description = "Compact crossover SUV.",
                            SupplierId = 7,
                            CompanyId = 5,
                            EngineNumber = "ENG33345",
                            ChassisNumber = "CHS87890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "White",
                            Price = 30000,
                            Mileage = 12000,
                            ManufactureYear = 2022
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2035",
                            Name = "Subaru Outback",
                            Slug = "subaru-outback",
                            Image = "subaru-outback.jpg",
                            Status = "Available",
                            Description = "SUV with symmetrical AWD.",
                            SupplierId = 7,
                            CompanyId = 5,
                            EngineNumber = "ENG43345",
                            ChassisNumber = "CHS97890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "Silver",
                            Price = 33000,
                            Mileage = 6000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2036",
                            Name = "Volkswagen Passat",
                            Slug = "volkswagen-passat",
                            Image = "volkswagen-passat.jpg",
                            Status = "Available",
                            Description = "Spacious sedan with premium features.",
                            SupplierId = 8,
                            CompanyId = 6,
                            EngineNumber = "ENG53345",
                            ChassisNumber = "CHS07890",
                            FuelType = "Diesel",
                            TransmissionType = "Automatic",
                            Color = "Black",
                            Price = 32000,
                            Mileage = 11000,
                            ManufactureYear = 2021
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2037",
                            Name = "Volvo XC90",
                            Slug = "volvo-xc90",
                            Image = "volvo-xc90.jpg",
                            Status = "Available",
                            Description = "Luxury SUV with hybrid options.",
                            SupplierId = 8,
                            CompanyId = 6,
                            EngineNumber = "ENG63345",
                            ChassisNumber = "CHS17890",
                            FuelType = "Hybrid",
                            TransmissionType = "Automatic",
                            Color = "Blue",
                            Price = 70000,
                            Mileage = 9000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2038",
                            Name = "Lexus RX",
                            Slug = "lexus-rx",
                            Image = "lexus-rx.jpg",
                            Status = "Available",
                            Description = "Luxury crossover with quiet ride.",
                            SupplierId = 9,
                            CompanyId = 6,
                            EngineNumber = "ENG73345",
                            ChassisNumber = "CHS27890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "White",
                            Price = 65000,
                            Mileage = 7500,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2039",
                            Name = "Porsche Cayenne",
                            Slug = "porsche-cayenne",
                            Image = "porsche-cayenne.jpg",
                            Status = "Available",
                            Description = "Performance luxury SUV.",
                            SupplierId = 9,
                            CompanyId = 7,
                            EngineNumber = "ENG83345",
                            ChassisNumber = "CHS37890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "Red",
                            Price = 85000,
                            Mileage = 5000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2040",
                            Name = "Range Rover Evoque",
                            Slug = "range-rover-evoque",
                            Image = "range-rover-evoque.jpg",
                            Status = "Available",
                            Description = "Compact luxury SUV.",
                            SupplierId = 9,
                            CompanyId = 7,
                            EngineNumber = "ENG93345",
                            ChassisNumber = "CHS47890",
                            FuelType = "Diesel",
                            TransmissionType = "Automatic",
                            Color = "Gray",
                            Price = 75000,
                            Mileage = 10000,
                            ManufactureYear = 2023
                        },
                        new Vehicle
                        {
                            ModelNumber = "MDL2041",
                            Name = "Chevrolet Tahoe",
                            Slug = "chevrolet-tahoe",
                            Image = "chevrolet-tahoe.jpg",
                            Status = "Available",
                            Description = "Full-size SUV with V8 engine.",
                            SupplierId = 10,
                            CompanyId = 8,
                            EngineNumber = "ENG04345",
                            ChassisNumber = "CHS57890",
                            FuelType = "Petrol",
                            TransmissionType = "Automatic",
                            Color = "Black",
                            Price = 55000,
                            Mileage = 8000,
                            ManufactureYear = 2022
                        }
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data PurchaseOrders
                if (!appContext.PurchaseOrders.Any())
                {
                    // Insert the seed data for PurchaseOrders table
                    appContext.PurchaseOrders.AddRange(
                        new PurchaseOrder { SupplierId = 1, OrderDate = new DateTime(2024, 12, 1), TotalAmount = 50000000 },
                        new PurchaseOrder { SupplierId = 2, OrderDate = new DateTime(2024, 12, 2), TotalAmount = 60000000 },
                        new PurchaseOrder { SupplierId = 3, OrderDate = new DateTime(2024, 12, 3), TotalAmount = 45000000 },
                        new PurchaseOrder { SupplierId = 4, OrderDate = new DateTime(2024, 12, 4), TotalAmount = 70000000 },
                        new PurchaseOrder { SupplierId = 5, OrderDate = new DateTime(2024, 12, 5), TotalAmount = 80000000 },
                        new PurchaseOrder { SupplierId = 6, OrderDate = new DateTime(2024, 12, 6), TotalAmount = 90000000 },
                        new PurchaseOrder { SupplierId = 7, OrderDate = new DateTime(2024, 12, 7), TotalAmount = 100000000 },
                        new PurchaseOrder { SupplierId = 8, OrderDate = new DateTime(2024, 12, 8), TotalAmount = 110000000 },
                        new PurchaseOrder { SupplierId = 9, OrderDate = new DateTime(2024, 12, 9), TotalAmount = 120000000 },
                        new PurchaseOrder { SupplierId = 10, OrderDate = new DateTime(2024, 12, 10), TotalAmount = 130000000 },
                        new PurchaseOrder { SupplierId = 11, OrderDate = new DateTime(2024, 12, 11), TotalAmount = 140000000 },
                        new PurchaseOrder { SupplierId = 12, OrderDate = new DateTime(2024, 12, 12), TotalAmount = 150000000 },
                        new PurchaseOrder { SupplierId = 13, OrderDate = new DateTime(2024, 12, 13), TotalAmount = 160000000 },
                        new PurchaseOrder { SupplierId = 14, OrderDate = new DateTime(2024, 12, 14), TotalAmount = 170000000 },
                        new PurchaseOrder { SupplierId = 15, OrderDate = new DateTime(2024, 12, 15), TotalAmount = 180000000 },
                        new PurchaseOrder { SupplierId = 16, OrderDate = new DateTime(2024, 12, 16), TotalAmount = 190000000 },
                        new PurchaseOrder { SupplierId = 17, OrderDate = new DateTime(2024, 12, 17), TotalAmount = 200000000 },
                        new PurchaseOrder { SupplierId = 18, OrderDate = new DateTime(2024, 12, 18), TotalAmount = 210000000 },
                        new PurchaseOrder { SupplierId = 19, OrderDate = new DateTime(2024, 12, 19), TotalAmount = 220000000 },
                        new PurchaseOrder { SupplierId = 20, OrderDate = new DateTime(2024, 12, 20), TotalAmount = 230000000 }
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data PurchaseOrderDetails
                if (!appContext.PurchaseOrderDetails.Any())
                {
                    // Insert the seed data for PurchaseOrderDetails table
                    appContext.PurchaseOrderDetails.AddRange(
                        new PurchaseOrderDetail { PurchaseOrderId = 1, VehicleId = 1, Quantity = 10, UnitPrice = 5000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 2, VehicleId = 2, Quantity = 5, UnitPrice = 6000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 3, VehicleId = 3, Quantity = 7, UnitPrice = 4500000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 4, VehicleId = 4, Quantity = 8, UnitPrice = 7000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 5, VehicleId = 5, Quantity = 3, UnitPrice = 8000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 6, VehicleId = 6, Quantity = 6, UnitPrice = 9000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 7, VehicleId = 7, Quantity = 4, UnitPrice = 10000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 8, VehicleId = 8, Quantity = 2, UnitPrice = 11000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 9, VehicleId = 9, Quantity = 5, UnitPrice = 12000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 10, VehicleId = 10, Quantity = 9, UnitPrice = 13000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 11, VehicleId = 11, Quantity = 1, UnitPrice = 14000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 12, VehicleId = 12, Quantity = 4, UnitPrice = 15000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 13, VehicleId = 13, Quantity = 8, UnitPrice = 16000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 14, VehicleId = 14, Quantity = 3, UnitPrice = 17000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 15, VehicleId = 15, Quantity = 2, UnitPrice = 18000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 16, VehicleId = 16, Quantity = 7, UnitPrice = 19000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 17, VehicleId = 17, Quantity = 6, UnitPrice = 20000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 18, VehicleId = 18, Quantity = 5, UnitPrice = 21000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 19, VehicleId = 19, Quantity = 3, UnitPrice = 22000000 },
                        new PurchaseOrderDetail { PurchaseOrderId = 20, VehicleId = 20, Quantity = 4, UnitPrice = 23000000 }
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data SalesOrders
                if (!appContext.SalesOrders.Any())
                {
                    // Insert the seed data for SalesOrders table
                    appContext.SalesOrders.AddRange(
                        new SalesOrder { UserId = 1, OrderDate = new DateTime(2024, 12, 1), TotalAmount = 50000000, Status = "pending" },
                        new SalesOrder { UserId = 2, OrderDate = new DateTime(2024, 12, 2), TotalAmount = 60000000, Status = "completed" },
                        new SalesOrder { UserId = 3, OrderDate = new DateTime(2024, 12, 3), TotalAmount = 45000000, Status = "pending" },
                        new SalesOrder { UserId = 4, OrderDate = new DateTime(2024, 12, 4), TotalAmount = 70000000, Status = "completed" },
                        new SalesOrder { UserId = 5, OrderDate = new DateTime(2024, 12, 5), TotalAmount = 80000000, Status = "pending" },
                        new SalesOrder { UserId = 6, OrderDate = new DateTime(2024, 12, 6), TotalAmount = 90000000, Status = "completed" },
                        new SalesOrder { UserId = 7, OrderDate = new DateTime(2024, 12, 7), TotalAmount = 100000000, Status = "pending" },
                        new SalesOrder { UserId = 8, OrderDate = new DateTime(2024, 12, 8), TotalAmount = 110000000, Status = "completed" },
                        new SalesOrder { UserId = 9, OrderDate = new DateTime(2024, 12, 9), TotalAmount = 120000000, Status = "pending" },
                        new SalesOrder { UserId = 10, OrderDate = new DateTime(2024, 12, 10), TotalAmount = 130000000, Status = "completed" },
                        new SalesOrder { UserId = 1, OrderDate = new DateTime(2024, 12, 11), TotalAmount = 140000000, Status = "pending" },
                        new SalesOrder { UserId = 2, OrderDate = new DateTime(2024, 12, 12), TotalAmount = 150000000, Status = "completed" },
                        new SalesOrder { UserId = 3, OrderDate = new DateTime(2024, 12, 13), TotalAmount = 160000000, Status = "pending" },
                        new SalesOrder { UserId = 4, OrderDate = new DateTime(2024, 12, 14), TotalAmount = 170000000, Status = "completed" },
                        new SalesOrder { UserId = 5, OrderDate = new DateTime(2024, 12, 15), TotalAmount = 180000000, Status = "pending" },
                        new SalesOrder { UserId = 6, OrderDate = new DateTime(2024, 12, 16), TotalAmount = 190000000, Status = "completed" },
                        new SalesOrder { UserId = 7, OrderDate = new DateTime(2024, 12, 17), TotalAmount = 200000000, Status = "pending" },
                        new SalesOrder { UserId = 8, OrderDate = new DateTime(2024, 12, 18), TotalAmount = 210000000, Status = "completed" },
                        new SalesOrder { UserId = 9, OrderDate = new DateTime(2024, 12, 19), TotalAmount = 220000000, Status = "pending" },
                        new SalesOrder { UserId = 10, OrderDate = new DateTime(2024, 12, 20), TotalAmount = 230000000, Status = "completed" }
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data SalesOrderDetails
                if (!appContext.SalesOrderDetails.Any())
                {
                    // Insert the seed data for SalesOrderDetails table
                    appContext.SalesOrderDetails.AddRange(
                        new SalesOrderDetail { SalesOrderId = 1, VehicleId = 1, Quantity = 2, UnitPrice = 5000000 },
                        new SalesOrderDetail { SalesOrderId = 2, VehicleId = 2, Quantity = 1, UnitPrice = 6000000 },
                        new SalesOrderDetail { SalesOrderId = 3, VehicleId = 3, Quantity = 3, UnitPrice = 4500000 },
                        new SalesOrderDetail { SalesOrderId = 4, VehicleId = 4, Quantity = 2, UnitPrice = 7000000 },
                        new SalesOrderDetail { SalesOrderId = 5, VehicleId = 5, Quantity = 4, UnitPrice = 8000000 },
                        new SalesOrderDetail { SalesOrderId = 6, VehicleId = 6, Quantity = 1, UnitPrice = 9000000 },
                        new SalesOrderDetail { SalesOrderId = 7, VehicleId = 7, Quantity = 5, UnitPrice = 10000000 },
                        new SalesOrderDetail { SalesOrderId = 8, VehicleId = 8, Quantity = 2, UnitPrice = 11000000 },
                        new SalesOrderDetail { SalesOrderId = 9, VehicleId = 9, Quantity = 1, UnitPrice = 12000000 },
                        new SalesOrderDetail { SalesOrderId = 10, VehicleId = 10, Quantity = 3, UnitPrice = 13000000 },
                        new SalesOrderDetail { SalesOrderId = 11, VehicleId = 11, Quantity = 2, UnitPrice = 14000000 },
                        new SalesOrderDetail { SalesOrderId = 12, VehicleId = 12, Quantity = 4, UnitPrice = 15000000 },
                        new SalesOrderDetail { SalesOrderId = 13, VehicleId = 13, Quantity = 1, UnitPrice = 16000000 },
                        new SalesOrderDetail { SalesOrderId = 14, VehicleId = 14, Quantity = 3, UnitPrice = 17000000 },
                        new SalesOrderDetail { SalesOrderId = 15, VehicleId = 15, Quantity = 2, UnitPrice = 18000000 },
                        new SalesOrderDetail { SalesOrderId = 16, VehicleId = 16, Quantity = 4, UnitPrice = 19000000 },
                        new SalesOrderDetail { SalesOrderId = 17, VehicleId = 17, Quantity = 1, UnitPrice = 20000000 },
                        new SalesOrderDetail { SalesOrderId = 18, VehicleId = 18, Quantity = 2, UnitPrice = 21000000 },
                        new SalesOrderDetail { SalesOrderId = 19, VehicleId = 19, Quantity = 3, UnitPrice = 22000000 },
                        new SalesOrderDetail { SalesOrderId = 20, VehicleId = 20, Quantity = 4, UnitPrice = 23000000 }
                    );
                    await appContext.SaveChangesAsync();
                }

                // Data StockHistory
                if (!appContext.StockHistories.Any())
                {
                    // Insert the seed data for StockHistory table
                    appContext.StockHistories.AddRange(
                        new StockHistory { VehicleId = 1, UserId = 1, ChangeDate = new DateTime(2024, 12, 1), ChangeType = "Stock_In", Quantity = 10 },
                        new StockHistory { VehicleId = 2, UserId = 1, ChangeDate = new DateTime(2024, 12, 2), ChangeType = "Stock_Out", Quantity = 5 },
                        new StockHistory { VehicleId = 3, UserId = 1, ChangeDate = new DateTime(2024, 12, 3), ChangeType = "Stock_In", Quantity = 7 },
                        new StockHistory { VehicleId = 4, UserId = 2, ChangeDate = new DateTime(2024, 12, 4), ChangeType = "Stock_Out", Quantity = 8 },
                        new StockHistory { VehicleId = 5, UserId = 2, ChangeDate = new DateTime(2024, 12, 5), ChangeType = "Stock_In", Quantity = 5 },
                        new StockHistory { VehicleId = 6, UserId = 3, ChangeDate = new DateTime(2024, 12, 6), ChangeType = "Stock_Out", Quantity = 6 },
                        new StockHistory { VehicleId = 7, UserId = 3, ChangeDate = new DateTime(2024, 12, 7), ChangeType = "Stock_In", Quantity = 12 },
                        new StockHistory { VehicleId = 8, UserId = 3, ChangeDate = new DateTime(2024, 12, 8), ChangeType = "Stock_Out", Quantity = 14 },
                        new StockHistory { VehicleId = 9, UserId = 4, ChangeDate = new DateTime(2024, 12, 9), ChangeType = "Stock_In", Quantity = 13 },
                        new StockHistory { VehicleId = 10, UserId = 5, ChangeDate = new DateTime(2024, 12, 10), ChangeType = "Stock_Out", Quantity = 11 },
                        new StockHistory { VehicleId = 11, UserId = 6, ChangeDate = new DateTime(2024, 12, 11), ChangeType = "Stock_In", Quantity = 9 },
                        new StockHistory { VehicleId = 12, UserId = 6, ChangeDate = new DateTime(2024, 12, 12), ChangeType = "Stock_Out", Quantity = 10 },
                        new StockHistory { VehicleId = 13, UserId = 7, ChangeDate = new DateTime(2024, 12, 13), ChangeType = "Stock_In", Quantity = 8 },
                        new StockHistory { VehicleId = 14, UserId = 8, ChangeDate = new DateTime(2024, 12, 14), ChangeType = "Stock_Out", Quantity = 6 },
                        new StockHistory { VehicleId = 15, UserId = 8, ChangeDate = new DateTime(2024, 12, 15), ChangeType = "Stock_In", Quantity = 5 },
                        new StockHistory { VehicleId = 16, UserId = 9, ChangeDate = new DateTime(2024, 12, 16), ChangeType = "Stock_Out", Quantity = 7 },
                        new StockHistory { VehicleId = 17, UserId = 9, ChangeDate = new DateTime(2024, 12, 17), ChangeType = "Stock_In", Quantity = 10 },
                        new StockHistory { VehicleId = 18, UserId = 9, ChangeDate = new DateTime(2024, 12, 18), ChangeType = "Stock_Out", Quantity = 8 },
                        new StockHistory { VehicleId = 19, UserId = 10, ChangeDate = new DateTime(2024, 12, 19), ChangeType = "Stock_In", Quantity = 9 },
                        new StockHistory { VehicleId = 20, UserId = 10, ChangeDate = new DateTime(2024, 12, 20), ChangeType = "Stock_Out", Quantity = 7 }
                    );
                    await appContext.SaveChangesAsync();
                }

                if (!appContext.Billings.Any())
                {
                    // Insert seed data for Billing table
                    appContext.Billings.AddRange(
                        new Billing { SaleOrderId = 1, UserId = 1, BillingDate = new DateTime(2024, 12, 1), Amount = 250000, PaymentMethod = "Credit Card", Status = "Paid", Notes = "Payment successful", PaidDate = new DateTime(2024, 12, 1) },
                        new Billing { SaleOrderId = 2, UserId = 2, BillingDate = new DateTime(2024, 12, 2), Amount = 150000, PaymentMethod = "Cash", Status = "Paid", Notes = "Cash payment" },
                        new Billing { SaleOrderId = 3, UserId = 3, BillingDate = new DateTime(2024, 12, 3), Amount = 300000, PaymentMethod = "Credit Card", Status = "Pending", Notes = "Pending payment" },
                        new Billing { SaleOrderId = 4, UserId = 4, BillingDate = new DateTime(2024, 12, 4), Amount = 500000, PaymentMethod = "Bank Transfer", Status = "Failed", Notes = "Transfer failed" },
                        new Billing { SaleOrderId = 5, UserId = 5, BillingDate = new DateTime(2024, 12, 5), Amount = 750000, PaymentMethod = "Cash", Status = "Paid", Notes = "Paid in full" },
                        new Billing { SaleOrderId = 6, UserId = 6, BillingDate = new DateTime(2024, 12, 6), Amount = 200000, PaymentMethod = "Credit Card", Status = "Paid", Notes = "Payment processed successfully", PaidDate = new DateTime(2024, 12, 6) },
                        new Billing { SaleOrderId = 7, UserId = 7, BillingDate = new DateTime(2024, 12, 7), Amount = 125000, PaymentMethod = "Cash", Status = "Pending", Notes = "Awaiting payment" },
                        new Billing { SaleOrderId = 8, UserId = 8, BillingDate = new DateTime(2024, 12, 8), Amount = 400000, PaymentMethod = "Bank Transfer", Status = "Paid", Notes = "Transfer completed", PaidDate = new DateTime(2024, 12, 8) },
                        new Billing { SaleOrderId = 9, UserId = 9, BillingDate = new DateTime(2024, 12, 9), Amount = 550000, PaymentMethod = "Credit Card", Status = "Paid", Notes = "Payment successful", PaidDate = new DateTime(2024, 12, 9) },
                        new Billing { SaleOrderId = 10, UserId = 10, BillingDate = new DateTime(2024, 12, 10), Amount = 320000, PaymentMethod = "Bank Transfer", Status = "Pending", Notes = "Pending bank transfer" },
                        new Billing { SaleOrderId = 1, UserId = 1, BillingDate = new DateTime(2024, 12, 11), Amount = 420000, PaymentMethod = "Cash", Status = "Paid", Notes = "Payment completed" },
                        new Billing { SaleOrderId = 2, UserId = 2, BillingDate = new DateTime(2024, 12, 12), Amount = 320000, PaymentMethod = "Credit Card", Status = "Paid", Notes = "Paid by credit card", PaidDate = new DateTime(2024, 12, 12) },
                        new Billing { SaleOrderId = 3, UserId = 3, BillingDate = new DateTime(2024, 12, 13), Amount = 700000, PaymentMethod = "Bank Transfer", Status = "Failed", Notes = "Transfer failed" },
                        new Billing { SaleOrderId = 4, UserId = 4, BillingDate = new DateTime(2024, 12, 14), Amount = 250000, PaymentMethod = "Credit Card", Status = "Paid", Notes = "Payment processed", PaidDate = new DateTime(2024, 12, 14) },
                        new Billing { SaleOrderId = 5, UserId = 5, BillingDate = new DateTime(2024, 12, 15), Amount = 950000, PaymentMethod = "Cash", Status = "Pending", Notes = "Awaiting payment" },
                        new Billing { SaleOrderId = 6, UserId = 6, BillingDate = new DateTime(2024, 12, 16), Amount = 220000, PaymentMethod = "Credit Card", Status = "Paid", Notes = "Payment successful", PaidDate = new DateTime(2024, 12, 16) },
                        new Billing { SaleOrderId = 7, UserId = 7, BillingDate = new DateTime(2024, 12, 17), Amount = 180000, PaymentMethod = "Cash", Status = "Paid", Notes = "Payment received" },
                        new Billing { SaleOrderId = 8, UserId = 8, BillingDate = new DateTime(2024, 12, 18), Amount = 550000, PaymentMethod = "Bank Transfer", Status = "Paid", Notes = "Bank transfer completed", PaidDate = new DateTime(2024, 12, 18) },
                        new Billing { SaleOrderId = 9, UserId = 9, BillingDate = new DateTime(2024, 12, 19), Amount = 640000, PaymentMethod = "Credit Card", Status = "Pending", Notes = "Pending payment" },
                        new Billing { SaleOrderId = 10, UserId = 10, BillingDate = new DateTime(2024, 12, 20), Amount = 300000, PaymentMethod = "Cash", Status = "Paid", Notes = "Payment received", PaidDate = new DateTime(2024, 12, 20) }
                    );
                    await appContext.SaveChangesAsync();
                }

            }
        }

    }
}
