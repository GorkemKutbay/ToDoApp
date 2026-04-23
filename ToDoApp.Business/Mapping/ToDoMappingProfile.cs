using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.DTOs.Auth;
using ToDoApp.Core.Entities;

namespace ToDoApp.Business.Mapping
{
    public class ToDoMappingProfile : Profile
    {
        public ToDoMappingProfile()
        {
            CreateMap<ToDoItem, TodoResponseDto>()
                    .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name));
            CreateMap<CreateToDoDto, ToDoItem>();
            CreateMap<UpdateToDoDto, ToDoItem>();



        }
    }
}
