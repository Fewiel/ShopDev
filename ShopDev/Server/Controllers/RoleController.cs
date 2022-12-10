using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopDev.APIModels;
using ShopDev.APIModels.Models;
using ShopDev.DAL.Repositories;
using ShopDev.Server.Attributes;
using System.Collections;

namespace ShopDev.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RoleController : ControllerBase
{
    private readonly RoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleController(RoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [Permission("roles_get")]
    public async Task<GenericRepsonse<IEnumerable<Role>>> GetAllAsync(RequestBase request)
    {
        return GenericRepsonse<IEnumerable<Role>>.Ok().WithValue((await _roleRepository.GetAllAsync()).Select(r => _mapper.Map<Role>(r)));
    }
}