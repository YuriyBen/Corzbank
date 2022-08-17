using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.DTOs;
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

            CreateMap<Transfer, TransferDTO>().ReverseMap();

            CreateMap<DepositModel, Deposit>().ReverseMap();

            CreateMap<Deposit, DepositDTO>().ReverseMap();

            CreateMap<ExchangeModel, Exchange>();

            CreateMap<TokenModel, Token>();

            CreateMap<Card, CardDTO>().ReverseMap();
        }
    }
}
