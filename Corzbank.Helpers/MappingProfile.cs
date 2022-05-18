using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Corzbank.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(um => string.Join('0', um.Firstname, um.Lastname)));

            CreateMap<TransferModel, Transfer>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<DepositModel, Deposit>();

            CreateMap<ExchangeModel, Exchange>();

            CreateMap<TokenModel, Token>();

            CreateMap<CardForUpdateModel, Card>();
        }
    }
}
