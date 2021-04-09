using AutoMapper;
using QPCore.Model.DataBaseModel.TestFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TestFlow, TestFlowDTO>()
                .ForMember(d => d.TestFlowGroup, opt => opt.MapFrom(s => s.GroupStep()));

            CreateMap<TestFlowDTO, TestFlow>()
                .ForMember(d => d.Steps, opt => opt.MapFrom(s => s.UngroupStep()));
        }
    }
}
