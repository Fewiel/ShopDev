﻿using AutoMapper;

namespace ShopDev.Server.Utility;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<DAL.Models.User, APIModels.Models.User>();
		CreateMap<DAL.Models.Role, APIModels.Models.Role>();
	}
}