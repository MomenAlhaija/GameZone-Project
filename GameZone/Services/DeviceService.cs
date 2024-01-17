namespace GameZone.Services
{

    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _context;
        public DeviceService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectListDevices()
        {
            return _context.Devices.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).AsNoTracking().
            OrderBy(c => c.Text).ToList();
        }
    }
}
