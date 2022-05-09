using AutoMapper;
using Corzbank.Data.Entities;
using Corzbank.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corzbank.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, User>()
                .ForMember(u => u.UserName, opt=>opt.MapFrom(x=>string.Join('0', x.Firstname, x.Lastname)));

            CreateMap<TransferModel, Transfer>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
