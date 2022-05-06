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
            CreateMap<UserModel, User>();
        }
    }
}
