using Microsoft.AspNetCore.Mvc;
using ShopDev.APIModels;
using ShopDev.APIModels.Models;
using ShopDev.DAL.Repositories;

namespace ShopDev.Server.Controllers.Administration;

[ApiController]
[Route("Administration/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UsersController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<GenericRepsonse<List<User>>> GetAll(RequestBase rb)
    {
        var users = new List<User>();

        foreach (var usr in await _userRepository.GetAllAsync())
        {
            users.Add(new()
            {
                Id = usr.Id,
                Username = usr.Username,
                Name = usr.Name,
                RoleID = usr.RoleID,
                AbsenceReason = usr.AbsenceReason,
                AbsenceDate = usr.AbsenceDate,
                Active = usr.Active,
                AdminNote = usr.AdminNote,
                Email = usr.Email,
                ExpirationDate = usr.ExpirationDate,
                LastUsed = usr.LastUsed,
                SlackID = usr.SlackID
            });
        }

        return GenericRepsonse<List<User>>.Ok().WithValue(users);
    }
}