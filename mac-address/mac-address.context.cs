using Microsoft.EntityFrameworkCore;

namespace mac_api.mac_address;

public class MacAddressContext(DbContextOptions<MacAddressContext> options) : DbContext(options) {
  public DbSet<MacAddress> Addresses { get; set; }
}