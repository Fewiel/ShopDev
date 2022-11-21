﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopDev.APIModels;
using ShopDev.DAL.Repositories;

namespace ShopDev.Server.Attributes;

public class PermissionAttribute : ActionFilterAttribute
{
    public string PermissionName { get; }

    public PermissionAttribute(string permissionName)
    {
        PermissionName = permissionName;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userRepo = context.HttpContext.RequestServices.GetService<UserRepository>();
        var permissionRepo = context.HttpContext.RequestServices.GetService<PermissionRepository>();

        if (!context.ActionArguments.TryGetValue("request", out var value) || value is not RequestBase rb)
            throw new InvalidOperationException("PermissionAttribute requires the argument \"request\" of type RequestBase to exist.");

        if (rb == null || rb.UserId == null || rb.Token == null || rb.Token.Content == null)
        {
            context.Result = new BadRequestResult();
            return;
        }

        var usr = await userRepo!.GetByIDAsync(rb.UserId.Value);

        if (await permissionRepo!.HasPermissionAsync(usr, PermissionName))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}