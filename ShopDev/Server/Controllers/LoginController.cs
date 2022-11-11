using Microsoft.AspNetCore.Mvc;
using ShopDev.APIModels.Login;
using ShopDev.DAL.Models;
using ShopDev.DAL.Repositories;

namespace ShopDev.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController : ControllerBase
{
    private readonly UserRepository Users;
    private readonly TokenRepository Tokens;

    public LoginController(UserRepository users, TokenRepository tokens)
    {
        Users = users;
        Tokens = tokens;
    }

    [HttpPost]
    public async Task<LoginResponseModel> LoginAsync(LoginRequestModel lr)
    {
        if (!lr.Validate())
            return LoginResponseModel.Fail();

        var usr = await Users.GetByUsernameAsync(lr.Username!);

        if (usr == null)
            return LoginResponseModel.Fail().WithMessage("User or password incorrect!", "danger");

        if (!PasswordHasher.Verify(lr.Password!, usr.Password!))
            return LoginResponseModel.Fail().WithMessage("User or password incorrect!", "danger");

        var token = new Token
        {
            Id = Guid.NewGuid(),
            UserID = usr.Id,
            Type = TokenType.Login,
            ExpireTime = DateTime.Now.AddDays(7)
        };

        await Tokens.InsertAsync(token);

        return LoginResponseModel.Ok(m =>
        {
            m.UserId = usr.Id;
            m.Permissions = new();
            m.Limits = new();
            m.Token = new APIModels.Token { Content = token.Id.ToString(), ExpireTime = token.ExpireTime };
        });
    }
}