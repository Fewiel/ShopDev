﻿using AutoMapper;
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

    [HttpPost]
    [Permission("administration_users_lock")]
    public async Task<GenericRepsonse<Guid>> Lock(GenericRequest<Guid> request)
    {
        var usr = await _userRepository.GetByIDAsync(request.Value);
        usr.Active = !usr.Active;
        await _userRepository.UpdateAsync(usr);
        return GenericRepsonse<Guid>.Ok().WithMessage($"User {usr.Username} Active: {usr.Active}", "info");
    }

    [HttpPost]
    [Permission("administration_users_delete")]
    public async Task<GenericRepsonse<Guid>> Delete(GenericRequest<Guid> request)
    {
        await _userRepository.DeleteAsync(await _userRepository.GetByIDAsync(request.Value));
        return GenericRepsonse<Guid>.Ok().WithMessage("User deleted", "info");
    }

    [HttpPost]
    [Permission("administration_users_update")]
    public async Task<GenericRepsonse<Guid>> Update(GenericRequest<User> request)
    {
        if (request.Value == null)
            return GenericRepsonse<Guid>.Fail().WithMessage("Update failed - No user given", "error");

        var usr = await _userRepository.GetByIDAsync(request.Value.Id);

        usr.Active = request.Value.Active;
        usr.AbsenceDate = request.Value.AbsenceDate;
        usr.ExpirationDate = request.Value.ExpirationDate;
        usr.AbsenceReason = request.Value.AbsenceReason;
        usr.AdminNote = request.Value.AdminNote;
        usr.Email = request.Value.Email;
        usr.RoleId = request.Value.RoleID;
        usr.SlackId = request.Value.SlackID;
        usr.Name = request.Value.Name;

        await _userRepository.UpdateAsync(usr);
        return GenericRepsonse<Guid>.Ok().WithMessage($"User {usr.Username} updated", "info");
    }
}