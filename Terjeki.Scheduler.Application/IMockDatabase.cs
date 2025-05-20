


namespace Terjeki.Scheduler.Application
{
    public interface IMockDatabase
    {
        
        public List<BusModel> Buses { get; set;  }
        public Task<BusModel> GetBus(Guid id, CancellationToken cancellationToken);
        public Task<List<BusModel>> GetBuses(CancellationToken cancellationToken);
        public Task<BusModel> CreateBus(CreateBusCommand model, CancellationToken cancellationToken);
        public Task<BusModel> UpdateBus(UpdateBusCommand model, CancellationToken cancellationToken);
        public Task<bool> DeleteBus(Guid id, CancellationToken cancellationToken);

        public List<DriverModel> Drivers { get; set; }
        public Task<DriverModel> GetDriver(Guid id, CancellationToken cancellationToken);
        public Task<List<DriverModel>> GetDrivers(CancellationToken cancellationToken);
        public Task<DriverModel> CreateDriver(CreateDriverCommand model, CancellationToken cancellationToken);
        public Task<DriverModel> UpdateDriver(UpdateDriverCommand model, CancellationToken cancellationToken);
        public Task<bool> DeleteDriver(Guid id, CancellationToken cancellationToken);

        public List<EventModel> Events { get; set; }
        public Task<EventModel> GetEvent(Guid id, CancellationToken cancellationToken);
        public Task<List<EventModel>> GetEvents(CancellationToken cancellationToken);
        public Task<List<EventModel>> GetEvents(DateTime start, DateTime end, CancellationToken cancellationToken);
        public Task<List<DriverEventModel>> GetDriverEvents(DateTime start, DateTime end, CancellationToken cancellationToken);
        public Task<List<EventModel>> GetServiceEvents(CancellationToken cancellationToken);
        public Task<EventModel> CreateEvent(CreateEventCommand model, CancellationToken cancellationToken);
        public Task<EventModel> UpdateEvent(UpdateEventCommand model, CancellationToken cancellationToken);
        public Task<bool> DeleteEvent(Guid id, CancellationToken cancellationToken);
        public Task<List<CapacityModel>> GetCapacities(CancellationToken cancellationToken);
        public Task<IEnumerable<EventGroupModel>> GetEventGroups(DateTime start, DateTime end, CancellationToken cancellationToken);
        public Task<UserModel> CreateUser(CreateUserCommand model, CancellationToken cancellationToken);
        public Task<UserModel> UpdateUser(UpdateUserCommand model, CancellationToken cancellationToken);
        public Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken);
        public Task<UserModel> GetUser(Guid id, CancellationToken cancellationToken);
        public Task<List<UserModel>> GetUsers(CancellationToken cancellationToken);
        public Task<List<RoleModel>> GetRoles(CancellationToken cancellationToken);
        public Task<List<ServiceModel>> GetServiceTypes(CancellationToken cancellationToken);

        public Task<EventModel> CreateHoliday(CreateHolidayCommand model, CancellationToken cancellationToken);
        public Task<EventModel> UpdateHoliday(UpdateHolidayCommand model, CancellationToken cancellationToken);
        public Task<bool> DeleteHoliday(Guid id, CancellationToken cancellationToken);
        public Task<List<HolidayModel>> GetHolidayTypes(CancellationToken cancellationToken);
    }
}
