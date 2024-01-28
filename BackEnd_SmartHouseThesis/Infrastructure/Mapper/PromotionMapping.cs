﻿using AutoMapper;
using Domain.DTOs.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapper
{
    public class PromotionMapping : Profile
    {
        public PromotionMapping()
        {
            CreateMap<PromotionRequest, Promotion>()
            .ForMember(des => des.Discount, act => act.MapFrom(src => src.Discount))
            .ForMember(des => des.StartDate, act => act.MapFrom(src => src.StartDate))
            .ForMember(des => des.EndDate, act => act.MapFrom(src => src.EndDate));


            CreateMap<Guid, string>().ConstructUsing(x => x.ToString());
            CreateMap<string, Guid>().ConstructUsing(x => new Guid(x));

        }
    }
}