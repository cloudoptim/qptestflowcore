using AutoMapper;
using DB=QPCore.Data.Enitites;
using QPCore.Model.Accounts;
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

            CreateMap<DB.OrgUser, AuthenticateResponse>();

            CreateMap<RegisterRequest, DB.OrgUser>()
                .ForMember(d => d.LoginName, opt => opt.MapFrom(s => s.Email));

            CreateMap<DB.OrgUser, AccountResponse>();
        }
    }
}
