using System.Security.Claims;

namespace Terjeki.Scheduler.Application
{

    public class MockDatabase : IMockDatabase
    {
        private List<RoleModel> GenerateRoles()
        {
            var roles = new List<RoleModel>
                    {
                        AdminRole,
                        DriverRole,
                        ServiceRole
                    };

            return roles;
        }
        private List<ServiceModel> GenerateServices()
        {
            return Enum.GetValues(typeof(ServiceTypes))
                .Cast<ServiceTypes>()
                .Where(x=> x != ServiceTypes.None)
                .Select(service => new ServiceModel
                {
                         
                    Name = service.GetDescription(),   // enum név stringként
                    Type = service               // eredeti enum érték
                })
                .ToList();
        }
        private List<HolidayModel> GenerateHolidays()
        {
            return Enum.GetValues(typeof(HolidayTypes))
                .Cast<HolidayTypes>()
                .Where(x => x != HolidayTypes.None)
                .Select(service => new HolidayModel
                {

                    Name = service.GetDescription(),   
                    Type = service            
                })
                .ToList();
        }
        private List<UserModel> GenerateUsers()
        {
            var users = new List<UserModel>
                    {
                        new UserModel { Id = Guid.NewGuid(), Name = "Juhász István", Email = "juhasz3129@gmail.com", Role = DriverRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Vikor Zsolt", Email = "vikor70@freemail.hu", Role = DriverRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Csikai Attila", Email = "csikaiattila73@gmail.com", Role = DriverRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Bujdosó János", Email = "boby930922@gmail.com", Role = DriverRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Pintér László", Email = "laszlopinter779@gmail.com", Role = DriverRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Nyakó Zoltán", Email = "nyakozoltan68@gmail.com", Role = DriverRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "László Dániel", Email = "d4ni721@gmail.com", Role = DriverRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Terjékiné Éva", Email = "terjekibusz@terjekibusz.t-online.hu", Role = AdminRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Terjéki Zsolt", Email = "terjekizsolt1985@gmail.com", Role = AdminRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Terjékiné Kiss Nikolett", Email = "niki.terjekibusz@gmail.com", Role = AdminRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Laczkó Vera", Email = "vera.terjekibusz@gmail.com", Role = AdminRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Lengyel Kata", Email = "kata.terjekibusz@gmail.com", Role = AdminRole },
                        new UserModel { Id = Guid.NewGuid(), Name = "Palaticki János", Email = "palatickijanos@gmail.com", Role = ServiceRole }
                    };

            return users;
        }

        private List<BusModel> GenerateBuses()
        {
            var buses = new List<BusModel>
            {
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Neoplan Tourliner",
                    LicensePlateNumber = "TR-JK-002",
                    Capacity = new CapacityModel { Seats = 49, Extra = 2 },
                    Driver = new DriverModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Juhász István",
                        
                    }
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Setra 515 HD",
                    LicensePlateNumber = "TDM-530",
                    Capacity = new CapacityModel { Seats = 49, Extra = 2 },
                    Driver = new DriverModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vikor Zsolt",
                      
                    }
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Neoplan Cityliner",
                    LicensePlateNumber = "PTK-623",
                    Capacity = new CapacityModel { Seats = 49, Extra = 2 },
                    Driver = new DriverModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Csikai Attila",
                        
                    }
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Setra 415 GT-HD",
                    LicensePlateNumber = "PKJ-431",
                    Capacity = new CapacityModel { Seats = 49, Extra = 2 },
                    Driver = new DriverModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bujdosó János",
                        
                    }
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Mercedes Tourismo",
                    LicensePlateNumber = "MAP-630",
                    Capacity = new CapacityModel { Seats = 53, Extra = 2 },
                    Driver = new DriverModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Pintér László",
                       
                    }
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Man Lion's Coach",
                    LicensePlateNumber = "RGU-770",
                    Capacity = new CapacityModel { Seats = 54, Extra = 2 },
                    Driver = new DriverModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Nyakó Zoltán",
                        
                    }
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Setra 516 HD",
                    LicensePlateNumber = "PPC-170",
                    Capacity = new CapacityModel { Seats = 57, Extra = 2 },
                    Driver = new DriverModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "László Dániel",
                    }
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Mercedes Sprinter 517",
                    LicensePlateNumber = "RMW-783",
                    Capacity = new CapacityModel { Seats = 20, Extra = 1 },
                    Driver = null
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Mercedes Sprinter 519CDI",
                    LicensePlateNumber = "PEZ-988",
                    Capacity = new CapacityModel { Seats = 20, Extra = 1 },
                    Driver = null
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Mercedes Sprinter 519CDI",
                    LicensePlateNumber = "AI-HZ-340",
                    Capacity = new CapacityModel { Seats = 20, Extra = 1 },
                    Driver = null
                },
                new BusModel
                {
                    Id = Guid.NewGuid(),
                    Brand = "Iveco 50C18",
                    LicensePlateNumber = "TDT-392",
                    Capacity = new CapacityModel { Seats = 23, Extra = 1 },
                    Driver = null
                }
            };

            return buses;
        }
        private List<DriverModel> GenerateDrivers()
        {
            return Buses.Where(x => x.Driver != null).Select(x => new DriverModel() 
            { 
                Id = x.Driver.Id,
                Name = x.Driver.Name,
                Bus = x
            

            }).ToList();
        }
        public List<CapacityModel> GenerateCapacties()
        {
            return Buses.Select(x => x.Capacity).Distinct().ToList();
        }
        public List<EventModel> GenerateEventsNoOverlap(int count)

        {
            var statuses = Enum.GetValues(typeof(EventStatuses))
                                .Cast<EventStatuses>()
                                .Where(x => x != EventStatuses.Canceled)
                                .ToList();

            var serviceTypes = Enum.GetValues(typeof(ServiceTypes))
                                    .Cast<ServiceTypes>()
                                    .Where(x => x != ServiceTypes.None)
                                    .ToList();

            var res = new List<EventModel>();
            var random = new Random();

            var cities = new List<string>
                {
                    "Budapest", "Bécs", "Pozsony", "Prága", "München", "Berlin", "Zágráb", "Ljubljana",
                    "Milánó", "Párizs", "Róma", "Koppenhága", "Amszterdam", "Brüsszel", "Varsó",
                    "Kraków", "Madrid", "Barcelona", "Genf", "Oslo"
                };

            // 4 vagy 5 busz kiválasztása tervhez
            var onlyPlan = Buses.OrderBy(x => random.Next()).Take(random.Next(4, 6)).ToList();
            var accepted = Buses.Except(onlyPlan).ToList();

            // Kizárólag Plan események generálása kapacitás alapján, ismétlődés nélkül
            var usedCapacities = new HashSet<int>();

            foreach (var bus in onlyPlan)
            {
                if (usedCapacities.Contains(bus.Capacity.Capacity))
                    continue;

                usedCapacities.Add(bus.Capacity.Capacity);
                DateTime lastEndDate = DateTime.Today;
                int eventCount = random.Next(3, 6);

                for (int i = 0; i < eventCount; i++)
                {
                    var faker = new Faker<EventModel>();

                    faker.RuleFor(e => e.Id, Guid.NewGuid)
                        .RuleFor(e => e.Capacity, bus.Capacity)
                        .RuleFor(e => e.StartDate, f => lastEndDate.AddDays(random.Next(1, 4)))
                        .RuleFor(e => e.EndDate, (f, e) => e.StartDate.AddDays(f.Random.Int(1, 5)))
                        .RuleFor(e => e.Description, f =>
                        {
                            var stops = f.Random.Int(2, 4);
                            var route = f.Random.ListItems(cities, stops);
                            return string.Join(" → ", route);
                        })
                        .RuleFor(e => e.Status, EventStatuses.Plan)
                        .RuleFor(e => e.Type, EventTypes.Event)
                        .RuleFor(e => e.ServiceType, ServiceTypes.None);

                    var generatedEvent = faker.Generate();
                    res.Add(generatedEvent);
                    lastEndDate = generatedEvent.EndDate;
                }
            }

            // Accepted státuszú események generálása busz és driver hozzárendeléssel
            foreach (var bus in accepted)
            {
                DateTime lastEndDate = DateTime.Today;
                int eventCount = random.Next(3, 6);

                for (int i = 0; i < eventCount; i++)
                {
                    var faker = new Faker<EventModel>();

                    faker.RuleFor(e => e.Id, Guid.NewGuid)
                        .RuleFor(e => e.Capacity, bus.Capacity)
                        .RuleFor(e => e.Bus, bus)
                        .RuleFor(e => e.Drivers, f => f.Random.ListItems(Drivers, f.Random.Int(1, 2)).ToList())
                        .RuleFor(e => e.StartDate, f => lastEndDate.AddDays(random.Next(1, 4)))
                        .RuleFor(e => e.EndDate, (f, e) => e.StartDate.AddDays(f.Random.Int(1, 5)))
                        .RuleFor(e => e.Description, f =>
                        {
                            var stops = f.Random.Int(2, 4);
                            var route = f.Random.ListItems(cities, stops);
                            return string.Join(" → ", route);
                        })
                        .RuleFor(e => e.Status, EventStatuses.Accepted)
                        .RuleFor(e => e.Type, EventTypes.Event)
                        .RuleFor(e => e.ServiceType, ServiceTypes.None);

                    var generatedEvent = faker.Generate();
                    res.Add(generatedEvent);
                    lastEndDate = generatedEvent.EndDate;
                }

                // Opcionális service esemény hozzáadása, ha nincs átfedés
                if (random.NextDouble() < 0.5)
                {
                    var maxAttempts = 10;
                    bool serviceAdded = false;

                    while (maxAttempts-- > 0 && !serviceAdded)
                    {
                        var serviceStart = lastEndDate.AddDays(random.Next(1, 3));
                        var serviceEnd = serviceStart.AddDays(random.Next(1, 2));

                        bool overlaps = res
                            .Where(e => e.Bus?.Id == bus.Id)
                            .Any(e => IntervalsOverlap(e.StartDate, e.EndDate, serviceStart, serviceEnd));

                        if (!overlaps)
                        {
                            var serviceEvent = new EventModel
                            {
                                Id = Guid.NewGuid(),
                                Bus = bus,
                                Drivers = null,
                                StartDate = serviceStart,
                                EndDate = serviceEnd,
                                Description = null,
                                Status = EventStatuses.Plan,
                                Type = EventTypes.Service,
                                ServiceType = serviceTypes[random.Next(serviceTypes.Count)],
                                Capacity = bus.Capacity
                            };

                            res.Add(serviceEvent);
                            serviceAdded = true;
                        }
                    }
                }
            }

            return res;
        }

        private bool IntervalsOverlap(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            return aStart < bEnd && bStart < aEnd;
        }

        private readonly TerjekiAuthenticationStateProvider authProvider;

        public List<RoleModel> Roles { get; set; }
        public List<UserModel> Users { get; set; }
        public List<BusModel> Buses { get; set; }
        public List<DriverModel> Drivers { get; set; }
        public List<EventModel> Events { get; set; }
        public List<CapacityModel> Capacities { get; set; }
        public List<ServiceModel> Services { get; set; }
        public List<HolidayModel> Holidays { get; set; }

        public RoleModel AdminRole = new RoleModel
        {
            Id = Guid.NewGuid(),
            Name = "Adminisztrátor",
            Type = RoleTypes.Admin
        };

        public RoleModel DriverRole = new RoleModel
        {
            Id = Guid.NewGuid(),
            Name = "Sofőr",
            Type = RoleTypes.Driver
        };

        public RoleModel ServiceRole = new RoleModel
        {
            Id = Guid.NewGuid(),
            Name = "Szerelő",
            Type = RoleTypes.Service
        };
        public MockDatabase(TerjekiAuthenticationStateProvider authProvider)
        {
            Holidays = GenerateHolidays();
            Services = GenerateServices();
            Roles = GenerateRoles();
            Users = GenerateUsers();
            Buses = GenerateBuses();
            Capacities = GenerateCapacties();
            Drivers = GenerateDrivers();
            Events = GenerateEventsNoOverlap(25);
            this.authProvider = authProvider;
        }
        public async Task<List<ServiceModel>> GetServiceTypes(CancellationToken cancellationToken)
        {
            return Services;

        }
        public async Task<List<RoleModel>> GetRoles(CancellationToken cancellationToken)
        {
            return Roles;

        }
        public async Task<List<CapacityModel>> GetCapacities(CancellationToken cancellationToken)
        {
            return Capacities;
        }
        public async Task<UserModel> CreateUser(CreateUserCommand model, CancellationToken cancellationToken)
        {
            var user = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Role = model.Role,

            };
            Users.Add(user);
            return Users.Where(x => x.Id == user.Id).FirstOrDefault();

        }
        public async Task<UserModel> UpdateUser(UpdateUserCommand model, CancellationToken cancellationToken)
        {
            var current = Users.Where(x => x.Id == model.Id).FirstOrDefault();
            if (current != null)
            {
                current.Name = model.Name;
                current.Email = model.Email;
                current.Role = model.Role;
            }

            return current;

        }
        public async Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var current = Users.Where(x => x.Id == id).FirstOrDefault();
            if (current == null) return false;

            Users.Remove(current);
            return true;

        }
        public async Task<UserModel> GetUser(Guid id, CancellationToken cancellationToken)
        {
            return Users.Where(x => x.Id == id).FirstOrDefault();

        }
        public async Task<List<UserModel>> GetUsers(CancellationToken cancellationToken)
        {
            return Users;

        }
        public async Task<BusModel> CreateBus(CreateBusCommand model, CancellationToken cancellationToken)
        {
            var bus = new BusModel()
            {
                Id = Guid.NewGuid(),
                Capacity = model.Capacity,
                Description = model.Description,
                LicensePlateNumber = model.LicensePlateNumber,
                Brand = model.Name,
                Driver = model.Driver,
            };
            Buses.Add(bus);
            return Buses.Where(x => x.Id == bus.Id).FirstOrDefault();

        }

        public async Task<DriverModel> CreateDriver(CreateDriverCommand model, CancellationToken cancellationToken)
        {
            var driver = new DriverModel()
            {
                Id = Guid.NewGuid(),
                Name = model.Driver.Name,
                Bus = model.Bus,
            };
            Drivers.Add(driver);
            var currentDriver = Drivers.Where(x => x.Id == driver.Id).FirstOrDefault();
            return currentDriver;
        }

        public async Task<EventModel> CreateEvent(CreateEventCommand model, CancellationToken cancellationToken)
        {
            var newEvent = new EventModel()
            {
                Id = Guid.NewGuid(),
                Capacity = model.Capacity,
                Bus = model.Bus,
                Description = model.Description,
                Drivers = model.Drivers,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Status = model.Status,
                Type = model.Type,
                ServiceType = model.ServiceType,
                
            };
            Events.Add(newEvent);
            if (newEvent.Type == EventTypes.Service)
            {
                var currentBus = Buses.Where(x => x.Id == model.Bus.Id).FirstOrDefault();
                if (currentBus != null) { currentBus.CurrentMileage = model.Bus.CurrentMileage; };
            }

            return Events.Where(x => x.Id == newEvent.Id).FirstOrDefault();

        }

        public async Task<bool> DeleteBus(Guid id, CancellationToken cancellationToken)
        {
            var current = Buses.Where(x => x.Id == id).FirstOrDefault();
            if (current == null) return false;
            var currentCapacity = current.Capacity;
            Buses.Remove(current);

            if (Capacities.Where(x => x.Capacity == currentCapacity.Capacity).Count() == 1) Capacities.Remove(currentCapacity);

            return true;
        }

        public async Task<bool> DeleteDriver(Guid id, CancellationToken cancellationToken)
        {
            var current = Drivers.Where(x => x.Id == id).FirstOrDefault();
            if (current == null) return false;

            Drivers.Remove(current);
            return true;
        }

        public async Task<bool> DeleteEvent(Guid id, CancellationToken cancellationToken)
        {
            var current = Events.Where(x => x.Id == id).FirstOrDefault();
            if (current == null) return false;

            Events.Remove(current);
            return true;
        }

        public async Task<BusModel> GetBus(Guid id, CancellationToken cancellationToken)
        {
            return Buses.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<List<BusModel>> GetBuses(CancellationToken cancellationToken)
        {
            return Buses;
        }

        public async Task<DriverModel> GetDriver(Guid id, CancellationToken cancellationToken)
        {
            return Drivers.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<List<DriverModel>> GetDrivers(CancellationToken cancellationToken)
        {
            return Drivers;
        }

        public async Task<EventModel> GetEvent(Guid id, CancellationToken cancellationToken)
        {
            return Events.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<List<EventModel>> GetEvents(CancellationToken cancellationToken)
        {

            return Events;
        }
        public async Task<List<EventModel>> GetEvents(DateTime start, DateTime end, CancellationToken cancellationToken)
        {

            return Events.Where(e => e.StartDate.Date <= end.Date && start.Date <= e.EndDate.Date).ToList();
        }
        public async Task<List<DriverEventModel>> GetDriverEvents(DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            var isAdmin = false;
            var authState = await authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var id = Guid.Empty;
            var name = string.Empty;
            if (user.Identity.IsAuthenticated)
            {
                var idString = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(idString))
                {
                    Guid.TryParse(idString, out id);
                }
                var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Ismeretlen";
                name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                isAdmin = role == "Admin";
            }
            var driverEventModels = Drivers
                .Select(driver => new DriverEventModel
                {
                    Driver = driver,
                    Events = (isAdmin
                        ? Events // admin: minden esemény
                            .Where(e => e.StartDate.Date <= end.Date && start.Date <= e.EndDate.Date && e.Drivers != null && e.Drivers.Contains(driver))
                        : Events // nem admin: csak saját driver eseményei
                            .Where(e => e.StartDate.Date <= end.Date && start.Date <= e.EndDate.Date && e.Drivers != null && e.Drivers.Contains(driver) && driver.Name == name))
                        .DistinctBy(e => e.Id)
                        .ToList()
                })
                .ToList();

            return driverEventModels;
        }
        public async Task<List<EventModel>> GetServiceEvents(CancellationToken cancellationToken)
        {

            return Events.Where(e => e.Type == EventTypes.Service).ToList();
        }
        public async Task<BusModel> UpdateBus(UpdateBusCommand model, CancellationToken cancellationToken)
        {
            var current = Buses.Where(x => x.Id == model.Id).FirstOrDefault();
            if (current != null)
            {

                current.Capacity = model.Capacity;
                current.Description = model.Description;
                current.LicensePlateNumber = model.LicensePlateNumber;
                current.Brand = model.Name;
                current.Driver = model.Driver;
            }
            return current;
        }

        public async Task<DriverModel> UpdateDriver(UpdateDriverCommand model, CancellationToken cancellationToken)
        {
            var current = Drivers.Where(x => x.Id == model.Id).FirstOrDefault();
            var currentBusModel = Buses.Where(x => x.Id == model.Bus.Id).FirstOrDefault();
            if (current != null && currentBusModel != null)
            {
                current.Bus = currentBusModel;
                currentBusModel.Driver = current;
            }

            return current;
        }

        public async Task<EventModel> UpdateEvent(UpdateEventCommand model, CancellationToken cancellationToken)
        {
            var current = Events.Where(x => x.Id == model.Id).FirstOrDefault();
            if (current != null)
            {
                current.Id = model.Id;
                current.Capacity = model.Capacity;
                current.Bus = model.Bus;
                current.Description = model.Description;
                current.Drivers = model.Drivers;
                current.EndDate = model.EndDate;
                current.StartDate = model.StartDate;
                current.Status = model.Status;
            }
            if (current.Type == EventTypes.Service)
            {
                var currentBus = Buses.Where(x => x.Id == model.Bus.Id).FirstOrDefault();
                if (currentBus != null) { currentBus.CurrentMileage = model.Bus.CurrentMileage; };
            }
            return current;
        }

        public async Task<IEnumerable<EventGroupModel>> GetEventGroups(DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            var isAdmin = false;
            var authState = await authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            Guid userId = Guid.Empty;
            var name = string.Empty;
            if (user.Identity.IsAuthenticated)
            {
                var idString = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(idString))
                {
                    Guid.TryParse(idString, out userId);
                }
                var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Ismeretlen";
                name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                isAdmin = role == "Admin";
            }
            var plannedEvents = Capacities.Select(x => new EventGroupModel
            {
                Bus = new BusModel() { Id = new Guid(), Capacity = x },
                Capacity = x,
                Events = (isAdmin
                    ? Events.Where(e => e.Bus == null && e.Capacity != null && e.Capacity.Capacity == x.Capacity && e.StartDate.Date <= end.Date && start.Date <= e.EndDate.Date)
                    : Events.Where(e => e.Bus == null && e.Capacity != null && e.Capacity.Capacity == x.Capacity && e.StartDate.Date <= end.Date && start.Date <= e.EndDate.Date &&
                        e.Drivers != null && e.Drivers.Select(x => x.Name).Contains(name)))
                    .ToList()
            }).ToList();
            var events = Buses.Select(bus => new EventGroupModel
            {
                Bus = bus,
                Capacity = bus.Capacity,
                Events = (isAdmin
                    ? Events.Where(e => e.Bus != null && e.Bus.Id == bus.Id && e.StartDate.Date <= end.Date && start.Date <= e.EndDate.Date)
                    : Events.Where(e => e.Bus != null && e.Bus.Id == bus.Id && e.StartDate.Date <= end.Date && start.Date <= e.EndDate.Date &&
                      e.Drivers != null && e.Drivers.Select(x => x.Name).Contains(name)))
                    .ToList()
            }).ToList();
            plannedEvents.AddRange(events);
            return plannedEvents;

        }

        public async Task<EventModel> CreateHoliday(CreateHolidayCommand model, CancellationToken cancellationToken)
        {
            var newEvent = new EventModel()
            {
                Id = Guid.NewGuid(),
                
                Description = model.Description,
                Drivers = new List<DriverModel> { model.Driver },
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Type = EventTypes.Holiday,
                HolidayType = model.HolidayType,

            };
            Events.Add(newEvent);
            return Events.Where(x => x.Id == newEvent.Id).FirstOrDefault();
        }

        public async Task<EventModel> UpdateHoliday(UpdateHolidayCommand model, CancellationToken cancellationToken)
        {
            var current = Events.Where(x => x.Id == model.Id).FirstOrDefault();
            if (current != null)
            {
                current.Id = Guid.NewGuid();
                
                current.Description = model.Description;
                current.Drivers = new List<DriverModel> { model.Driver };
                current.EndDate = model.EndDate;
                current.StartDate = model.StartDate;
                current.Type = EventTypes.Holiday;
                current.HolidayType = model.HolidayType;
            }
            
            return current;
        }

        public async Task<bool> DeleteHoliday(Guid id, CancellationToken cancellationToken)
        {
            var current = Events.Where(x => x.Id == id).FirstOrDefault();
            if (current == null) return false;

            Events.Remove(current);
            return true;
        }

        public async Task<List<HolidayModel>> GetHolidayTypes(CancellationToken cancellationToken)
        {
            return Holidays;
        }
    }
}
