using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = "a",
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        Id = "b",
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        Id = "c",
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                    //since we're using userManager this will be automatically kept in the user store, and saved to the db
                    //it also creates the passwords in a hashed and salted way, meaning although the original password isn't unique, the db holds a uniquely computed string
                    //which is quite difficult to hack and requires a lot of cpu resources
                }
            }

            if (!context.Jobs.Any())
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                //string format = "dd/MM/yyyy h:mm:ss tt";
                var jobs = new List<Job>
                {
                    
                    new Job
                    {
                        JobName = "TestJob1",
                        Company = "Shomera",
                        Replication = "VMWARE SRM",
                        Servers = "10",
                        //LastRun = DateTime.ParseExact("12/11/2019 6:29:33 AM",format,provider),
                        LastRun = DateTime.Now,
                        RTA = "5",
                        Results = "All tested - server is ok",
                        Key = "AAAA-BBBB-CCCC-DDDD",
                        RTONeeded = "10",
                    },
                    new Job
                    {
                        JobName = "TestJob2",
                        Company = "Altshuler",
                        Replication = "ZERTO",
                        Servers = "20",
                        //LastRun = DateTime.ParseExact("13/11/2019 9:49:22 AM",format,provider),
                        LastRun = DateTime.Now,
                        RTA = "15",
                        Results = "All tested - server is ok",
                        Key = "AAAA-BBBB-CCCC-DDDD",
                        RTONeeded = "20",
                    },
                    new Job
                    {
                        JobName = "TestJob3",
                        Company = "Ministery of Treasury",
                        Replication = "Netapp",
                        Servers = "300",
                        //LastRun = DateTime.ParseExact("12/11/2018 1:29:33 PM",format,provider),
                        LastRun = DateTime.Now,
                        RTA = "55",
                        Results = "All tested - server is ok",
                        Key = "AAAA-BBBB-CCCC-DDDD",
                        RTONeeded = "30",
                    },
                    new Job
                    {
                        JobName = "TestJob4",
                        Company = "Amdocs",
                        Replication = "Recover point",
                        Servers = "100",
                        //LastRun = DateTime.ParseExact("10/11/2019 8:19:33 AM",format,provider),
                        LastRun = DateTime.Now,
                        RTA = "100",
                        Results = "All tested - server is ok",
                        Key = "AAAA-BBBB-CCCC-DDDD",
                        RTONeeded = "10",
                    },
                    new Job
                    {
                        JobName = "TestJob5",
                        Company = "Open University",
                        Replication = "DoubleTake",
                        Servers = "20",
                        //LastRun = DateTime.ParseExact("12/08/2019 6:22:44 AM",format,provider),
                        LastRun = DateTime.Now,
                        RTA = "23",
                        Results = "All tested - server is ok",
                        Key = "AAAA-BBBB-CCCC-DDDD",
                        RTONeeded = "30",
                    },
                };
                await context.Jobs.AddRangeAsync(jobs);
                await context.SaveChangesAsync();
                
            }
        }
    }
}

