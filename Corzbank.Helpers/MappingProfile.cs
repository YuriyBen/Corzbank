using AutoMapper;
using Corzbank.Data.Models;
using Corzbank.Data.Models.DTOs;
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
            CreateMap<UserDTO, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(um => string.Join('0', um.Firstname, um.Lastname)));

            CreateMap<TransferDTO, Transfer>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Transfer, TransferDetailsDTO>().ReverseMap();

            CreateMap<DepositDTO, Deposit>().ReverseMap();

            CreateMap<Deposit, DepositDetailsDTO>().ReverseMap();

            CreateMap<ExchangeDTO, Exchange>();

            CreateMap<TokenDTO, Token>();

            CreateMap<Card, CardDetailsDTO>().ReverseMap();
        }
    }
}
