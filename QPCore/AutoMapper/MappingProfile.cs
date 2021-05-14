using AutoMapper;
using DB=QPCore.Data.Enitites;
using QPCore.Model.Accounts;
using QPCore.Model.DataBaseModel.TestFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Model.Organizations;
using QPCore.Model.Applications;
using QPCore.Model.Roles;
using QPCore.Model.UserRoles;

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

            CreateMap<DB.OrgUser, AccountResponse>()
                .ForMember(d => d.OrgName, s => s.MapFrom(f => f.Org.OrgName));

            // Organization
            CreateMap<DB.Organization, OrganizationResponse>();
            CreateMap<CreateOrganizationRequest, DB.Organization>();

            CreateMap<DB.Role, RoleResponse>();

            // Application
            CreateMap<DB.Application, ApplicationResponse>()
                .ForMember(d => d.OrgName, s => s.MapFrom(r => r.Org.OrgName));

            CreateMap<CreateApplicationRequest, DB.Application>();

            // User Role
            CreateMap<DB.UserRole, UserRoleResponse>()
                .ForMember(d => d.RoleName, s => s.MapFrom(f => f.Role.Rolename))
                .ForMember(d => d.FirstName, s => s.MapFrom(f => f.OrgUser.FirstName))
                .ForMember(d => d.LastName, s => s.MapFrom(f => f.OrgUser.LastName));

            CreateMap<DB.UserRole, UserRoleInRoleResponse>()
                .ForMember(d => d.RoleName, s => s.MapFrom(f => f.Role.Rolename));

            CreateMap<DB.UserRole, UserRoleInUserResponse>()
                .ForMember(d => d.FirstName, s => s.MapFrom(f => f.OrgUser.FirstName))
                .ForMember(d => d.LastName, s => s.MapFrom(f => f.OrgUser.LastName));

            CreateMap<CreateUserRoleRequest, DB.UserRole>();
        }
    }
}
