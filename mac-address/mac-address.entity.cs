namespace mac_api.mac_address;
using FluentValidation;

public class MacAddress {
  public int Id { get; set; }
  public string Address { get; set; } = "";
  public string Email { get; set; } = "";
  public DateTime ValidUntil { get; set; }
}

public class CreateMacAddressDto {
  public string Address { get; set; } = "";
  public string Email { get; set; } = "";
  public DateTime ValidUntil { get; set; }
}

public class UpdateMacAddressDto {
  public string? Address { get; set; }
  public string? Email { get; set; }
  public DateTime? ValidUntil { get; set; }
}

public class MacAddressValidator: AbstractValidator<MacAddress> {
    public MacAddressValidator(MacAddressContext db) {
      RuleFor(x => x.Address)
        .NotEmpty()
        .WithMessage("A MAC-cím nem lehet üres")
        .Matches(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
        .WithMessage("A MAC-címnek meg kell felelnie a standard MAC-cím formátumnak (kettősponttal vagy kötőjellel elválasztva)")
        .Must((instance, address) => NotExist(db, instance))
        .WithMessage(m => $"A(z) {m.Address} MAC-cím már létezik");
      RuleFor(x => x.ValidUntil)
        .NotEmpty()
        .WithMessage("Az érvényesség nem lehet üres")
        .GreaterThan(DateTime.Now)
        .WithMessage("Az érvényességnek a jövőben kell lennie");
      RuleFor(x => x.Email)
        .NotEmpty()
        .WithMessage("Az email cím nem lehet üres")
        .EmailAddress()
        .WithMessage("Az email cím valóban email formátumú legyen");
    }

    public bool NotExist(MacAddressContext db, MacAddress macAddress) {
      return !db.Addresses.Any(m => m.Address == macAddress.Address && m.Id != macAddress.Id);
    }
  }