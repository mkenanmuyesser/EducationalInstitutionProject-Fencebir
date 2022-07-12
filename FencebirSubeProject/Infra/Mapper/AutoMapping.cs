using AutoMapper;
using FencebirSubeProject.Entities;
using FencebirSubeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FencebirSubeProject.Infra.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Sube, SubeViewModel>();
            CreateMap<SubeViewModel, Sube>();
        }
    }
}
