namespace mac_api.mac_address;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class MacAddressController(MacAddressContext _context) : ControllerBase {
  private readonly MacAddressContext db = _context;

  [HttpGet("/mac")]
  public async Task<IEnumerable<MacAddress>> Get() {
    return await db.Addresses.ToListAsync();
  }

  [HttpGet("/mac/{id}")]
  public async Task<ActionResult<MacAddress>> Get(int id) {
    var res = await db.Addresses.FindAsync(id);
    if(res == null) return NotFound();

    return res;
  }

  [HttpPost("/mac")]
  public async Task<ActionResult<MacAddress>> Post(CreateMacAddressDto dto) {
    var validator = new MacAddressValidator(db);
    MacAddress address = new() {
      Address = dto.Address,
      Email = dto.Email,
      ValidUntil = dto.ValidUntil
    };

    var result = await validator.ValidateAsync(address);
    if(!result.IsValid) return BadRequest(result.Errors);

    db.Addresses.Add(address);
    await db.SaveChangesAsync();

    return CreatedAtAction(nameof(Get), new { id = address.Id }, address);
  }

  [HttpPatch("/mac/{id}")]
  public async Task<ActionResult<MacAddress>> Patch(int id, UpdateMacAddressDto dto) {
    var res = await db.Addresses.FindAsync(id);
    if(res == null) return NotFound();

    res.Address = dto.Address ?? res.Address;
    res.Email = dto.Email ?? res.Email;
    res.ValidUntil = dto.ValidUntil ?? res.ValidUntil;

    var validator = new MacAddressValidator(db);
    var result = await validator.ValidateAsync(res);
    if(!result.IsValid) return BadRequest(result.Errors);

    await db.SaveChangesAsync();

    return NoContent();
  }

  [HttpDelete("/mac/{id}")]
  public async Task<ActionResult> Delete(int id) {
    var res = await db.Addresses.FindAsync(id);
    if(res == null) return NotFound();

    db.Addresses.Remove(res);
    await db.SaveChangesAsync();

    return NoContent();
  }
}