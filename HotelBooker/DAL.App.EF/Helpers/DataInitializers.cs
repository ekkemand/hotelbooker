using System;
using System.Linq;
using Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static void DeleteDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };

            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }


            var users =
                new (string email, string password, string firstName, string lastName, string displayName, DateTime bday
                    ,
                    string? phone, string? socialId)[]
                    {
                        ("ekmand@ttu.ee", "EkkeMand/2020", "Ekke", "Mänd", "Ekke", DateTime.Now, null, null),
                    };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.email).Result;
                if (user == null)
                {
                    user = new AppUser
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Email = userInfo.email,
                        UserName = userInfo.email,
                        DisplayName = userInfo.displayName,
                        EmailConfirmed = true,
                        PersonId = new Guid("00000000-0000-0000-0000-000000000001"),
                        Person = new Person
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            FirstName = "Ekke",
                            LastName = "Mänd",
                            BirthDate = DateTime.Now,
                            NationalIdNumber = "39812100248",
                            PhoneNumber = "56239169"
                        }
                    };

                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }

        public static void SeedData(AppDbContext context)
        {
            var owners = new[]
            {
                new OwnerCompany
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Hilton Worldwide Holdings Inc"
                },
                new OwnerCompany
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Test"
                }
            };

            foreach (var owner in owners)
            {
                if (!context.OwnerCompanies.Any(o => o.Id == owner.Id))
                {
                    context.OwnerCompanies.Add(owner);
                }
            }
            var hotels = new[]
            {
                new Hotel
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Hilton Tallinn Park",
                    Address = "Fr. R. Kreutzwaldi 23, Tallinna kesklinn, 10147 Tallinn, Eesti",
                    Website = "https://www3.hilton.com/en/hotels/estonia/hilton-tallinn-park-TLLHIHI/index.html",
                    ImageUrl = "https://www3.hilton.com/resources/media/hi/TLLHIHI/en_US/img/shared/" +
                               "full_page_image_gallery/main/HL_extnight1_1270x560_FitToBoxSmallDimension_Center.jpg",
                    Rating = 4,
                    Established = DateTime.Parse("2016-07-01"),
                    OwnerCompanyId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Hotel
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "TestHotel",
                    Address = "Some st 5, Some City, Some Parish, Some Country",
                    Website = "",
                    ImageUrl = "https://cdn.pixabay.com/photo/2018/09/08/22/37/software-3663509_1280.jpg",
                    Rating = 5,
                    Established = DateTime.Now,
                    OwnerCompanyId = new Guid("00000000-0000-0000-0000-000000000002")
                },
            };

            foreach (var hotel in hotels)
            {
                if (!context.Hotels.Any(o => o.Id == hotel.Id))
                {
                    context.Hotels.Add(hotel);
                }
            }

            var productGroups = new[]
            {
                new ProductGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Default group",
                    Description = "For all the products that have no specified group yet"
                },
                new ProductGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Test group",
                    Description = "Test purposes"
                },
            };

            foreach (var productGroup in productGroups)
            {
                if (!context.ProductGroups.Any(o => o.Id == productGroup.Id))
                {
                    context.ProductGroups.Add(productGroup);
                }
            }

            var products = new[]
            {
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Test product",
                    Description = "For test purposes only",
                    ProductGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Romantic package",
                    ProductGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                },
            };

            foreach (var product in products)
            {
                if (!context.Products.Any(o => o.Id == product.Id))
                {
                    context.Products.Add(product);
                }
            }

            var convenienceGroups = new[]
            {
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "General"
                },
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Food & Drink",
                },
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name = "Internet",
                },
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Name = "Parking",
                },
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Name = "Outdoors",
                },
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Name = "Health & Wellness",
                },
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Name = "Safety & Security",
                },
                new ConvenienceGroup
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    Name = "Languages Spoken ",
                },
            };

            foreach (var convenienceGroup in convenienceGroups)
            {
                if (!context.ConvenienceGroups.Any(o => o.Id == convenienceGroup.Id))
                {
                    context.ConvenienceGroups.Add(convenienceGroup);
                }
            }

            var conveniences = new[]
            {
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Air conditioning",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Elevator",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name = "Bar",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Name = "Restaurant",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Name = "Free wifi",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Name = "Fixed internet in rooms",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Name = "Secure parking",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000004")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    Name = "Terrace",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000005")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    Name = "Swimming pool",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000006")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    Name = "Fitness",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000006")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000011"),
                    Name = "Spa",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000006")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000012"),
                    Name = "Fire extinguishers",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000007")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000013"),
                    Name = "Smoke alarms",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000007")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000014"),
                    Name = "Safe",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000007")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000015"),
                    Name = "Estonian",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000008")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000016"),
                    Name = "English",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000008")
                },
                new Convenience
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000017"),
                    Name = "Spanish",
                    ConvenienceGroupId = new Guid("00000000-0000-0000-0000-000000000008")
                },
            };

            foreach (var convenience in conveniences)
            {
                if (!context.Conveniences.Any(o => o.Id == convenience.Id))
                {
                    context.Conveniences.Add(convenience);
                }
            }

            var roomTypes = new[]
            {
                new RoomType
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Type = "Suite",
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new RoomType
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Type = "Small",
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001")
                },
            };
            
            foreach (var roomType in roomTypes)
            {
                if (!context.RoomTypes.Any(o => o.Id == roomType.Id))
                {
                    context.RoomTypes.Add(roomType);
                }
            }

            var roomTypeProducts = new[]
            {
                new Product
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name = "Suite in Hilton Tallinn Park",
                    Description = "Get a 24H long rest in one of the sweetest hotels. To get more days, multiply the" +
                                  " price for as long as the reservation lasts to know the price beforehand!",
                    ProductGroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000001")
                }, 
            };
            
            foreach (var product in roomTypeProducts)
            {
                if (!context.Products.Any(o => o.Id == product.Id))
                {
                    context.Products.Add(product);
                }
            }

            var rooms = new[]
            {
                new Room
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    RoomNumber = "3965",
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000001"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Room
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    RoomNumber = "8913",
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000002"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Room
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    RoomNumber = "8923",
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000002"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Room
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    RoomNumber = "8923",
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000002"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Room
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    RoomNumber = "3975",
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000001"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001")
                },
            };
            
            foreach (var room in rooms)
            {
                if (!context.Rooms.Any(o => o.Id == room.Id))
                {
                    context.Rooms.Add(room);
                }
            }

            var currencies = new[]
            {
                new Currency
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Euro"
                },
                new Currency
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Dollar"
                },
            };
            
            foreach (var currency in currencies)
            {
                if (!context.Currencies.Any(o => o.Id == currency.Id))
                {
                    context.Currencies.Add(currency);
                }
            }

            var prices = new[]
            {
                new Price
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CurrencyId = new Guid("00000000-0000-0000-0000-000000000001"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000001"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Value = 20.00M
                }, 
                new Price
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    CurrencyId = new Guid("00000000-0000-0000-0000-000000000001"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Value = 100.00M
                },
                new Price
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    CurrencyId = new Guid("00000000-0000-0000-0000-000000000001"),
                    ProductId = new Guid("00000000-0000-0000-0000-000000000003"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Value = 100.00M
                },
            };
            foreach (var price in prices)
            {
                if (!context.Prices.Any(o => o.Id == price.Id))
                {
                    context.Prices.Add(price);
                }
            }

            var imageOfRooms = new[]
            {
                new ImageOfRoom
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001"),
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Hilton Suite 1",
                    Url = "https://q-cf.bstatic.com/xdata/images/hotel/max1024x768/74704159" +
                          ".jpg?k=31ca4af9863b95764d019adf8a7bf9c92cca3312f439316b2fc1a4cdd4cdea1d&o="
                },
                new ImageOfRoom
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    HotelId = new Guid("00000000-0000-0000-0000-000000000001"),
                    RoomTypeId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Hilton Suite 2",
                    Url = "https://r-cf.bstatic.com/xdata/images/hotel/max1024x768/74704171" +
                          ".jpg?k=83086c370ed9a665056c484cc9cd668826333316c6d8fb418bace1cfc7f1f930&o="
                },
            };
            foreach (var imageOfRoom in imageOfRooms)
            {
                if (!context.ImageOfRooms.Any(o => o.Id == imageOfRoom.Id))
                {
                    context.ImageOfRooms.Add(imageOfRoom);
                }
            }

            context.SaveChanges();
        }
    }
}