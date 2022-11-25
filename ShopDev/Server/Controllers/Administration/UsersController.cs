using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopDev.APIModels;
using ShopDev.APIModels.Models;
using ShopDev.DAL.Repositories;
using ShopDev.Server.Attributes;

namespace ShopDev.Server.Controllers.Administration;

[ApiController]
[Route("Administration/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [Permission("administration_users_list")]
    public async Task<GenericRepsonse<List<User>>> GetAll(RequestBase request)
    {
        var users = new List<User>();

        foreach (var usr in await _userRepository.GetAllAsync())
        {
            users.Add(_mapper.Map<User>(usr));
        }

        return GenericRepsonse<List<User>>.Ok().WithValue(users);
    }

    [HttpPost]
    [Permission("administration_users_get")]
    public async Task<GenericRepsonse<User>> Get(GenericRequest<Guid> request)
    {
        return GenericRepsonse<User>.Ok().WithValue(_mapper.Map<User>(await _userRepository.GetByIDAsync(request.Value)));
    }
}