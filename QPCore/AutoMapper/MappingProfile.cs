﻿using AutoMapper;
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
using QPCore.Model.TestPlans;
using QPCore.Model.TestPlanTestCases;
using QPCore.Model.TestFlowCategories;
using QPCore.Model.TestFlowCategoryAssocs;
using QPCore.Model.DataBaseModel;
using DataBaseModel;

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

            // TestPlan
            CreateMap<DB.TestPlan, TestPlanResponse>()
                .ForMember(d => d.ParentName, s => s.MapFrom(p => p.Parent.Name))
                .ForMember(d => d.AssignToFirstName, s => s.MapFrom(p => p.OrgUser.FirstName))
                .ForMember(d => d.AssignToLastName, s => s.MapFrom(p => p.OrgUser.LastName));

            CreateMap<CreateTestPlanRequest, DB.TestPlan>();

            // TestPlan TestCase
            CreateMap<DB.TestPlanTestCaseAssociation, TestPlanTestCaseResponse>()
                .ForMember(d => d.TestPlanName, s => s.MapFrom(e => e.TestPlan.Name))
                .ForMember(d => d.TestCaseName, s => s.MapFrom(e => e.TestCase.TestFlowName));
            
            // TestFlow Category
            CreateMap<DB.TestFlowCategory, TestFlowCategoryResponse>()
                .ForMember(d => d.ApplicationName, s => s.MapFrom(p => p.Client.ApplicationName));
            
            CreateMap<CreateTestFlowCategoryRequest, DB.TestFlowCategory>();
            
            // TestFlow Category Association
            CreateMap<DB.TestFlowCategoryAssoc, TestFlowCategoryAssocResponse>()
                .ForMember(d => d.CategoryName, s => s.MapFrom(p => p.Category.CategoryName))
                .ForMember(d => d.TestFlowName, s => s.MapFrom(p => p.TestFlow.TestFlowName));
            
            CreateMap<CreateTestFlowCategoryAssocRequest, DB.TestFlowCategoryAssoc>();

            // Web Element
            CreateMap<EditWebElementRequest, WebElement>();
        }
    }
}
