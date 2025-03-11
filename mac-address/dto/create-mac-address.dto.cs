namespace mac_api.mac_address;

public class CreateMacAddressDto {
  public string Address { get; set; } = "";
  public string Email { get; set; } = "";
  public DateTime ValidUntil { get; set; }
}