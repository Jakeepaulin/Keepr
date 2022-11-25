namespace Keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  private readonly VaultsService _vaultsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut]
  public async Task<ActionResult<Account>> UpdateAccount([FromBody] Account accountData )
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Account updatedAccount = _accountService.Edit(accountData, userInfo.Email);
      return Ok(updatedAccount);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      var userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      var myVaults = _vaultsService.GetMyVaults(userInfo?.Id);

      return Ok(myVaults);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("keeps")]
  [Authorize]
  public async Task<ActionResult<List<Keep>>> GetMyKeeps()
  {
    try
    {
      var userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> myKeeps = _accountService.GetMyKeeps(userInfo?.Id);

      return Ok(myKeeps);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
