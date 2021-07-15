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
using QPCore.Model.TestPlans;
using QPCore.Model.TestPlanTestCases;
using QPCore.Model.TestFlowCategories;
using QPCore.Model.TestFlowCategoryAssocs;
using QPCore.Model.DataBaseModel;
using DataBaseModel;
using QPCore.Model.WebPageGroups;
using QPCore.Model.WebPages;
using QPCore.Model.CompositeWebElements;
using QPCore.Model.WorkItemTypes;
using QPCore.Model.WorkItems;
using QPCore.Model.WorkItemTestcaseAssoc;
using QPCore.Model.TestFlows;
using QPCore.Model.Integrations;

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
                .ForMember(d => d.TestFlowName, s => s.MapFrom(p => p.TestFlow.TestFlowName))
                .ForMember(d => d.Type, s => s.MapFrom(p => p.Category.Type));
            
            CreateMap<CreateTestFlowCategoryAssocRequest, DB.TestFlowCategoryAssoc>();

            // Web Element
            CreateMap<EditWebElementRequest, WebElement>();

            // PageGroup
            CreateMap<DB.WebPageGroup, PageGroupItemResponse>();
            CreateMap<CreatePageGroupRequest, DB.WebPageGroup>();
            CreateMap<EditPageGroupRequest, DB.WebPageGroup>();

            // Page
            CreateMap<DB.WebPage, WebPageItemResponse>()
                .ForMember(d => d.GroupName, s => s.MapFrom(p => p.WebPageGroup.Name));
            CreateMap<CreateWebPageRequest, DB.WebPage>();
            CreateMap<EditWebPageRequest, DB.WebPage>();

            // Composite Web Elemenet
            CreateMap<DB.CompositeWebElement, ChildCompositeWebElementResponse>()
                // .ForMember(d => d.Name, s => s.MapFrom(p => p.WebElement.Elementaliasname))
                ;
            CreateMap<CreateChildCompositeWebElementRequest, DB.CompositeWebElement>()
                .ForMember(d => d.IsComposite, s => s.MapFrom(p => true));
            
            CreateMap<DB.CompositeWebElement, CompositeWebElementResponse>()
                .ForMember(d => d.PageName, s => s.MapFrom(p => p.WebPage.Name))
                .ForMember(d => d.WebPageGroupId, s => s.MapFrom(p => p.WebPage.GroupId))
                .ForMember(d => d.WebPageGroupName, s => s.MapFrom(p => p.WebPage.WebPageGroup.Name));
            
            CreateMap<CreateCompositeWebElementRequest, DB.CompositeWebElement>()
                .ForMember(d => d.IsComposite, s => s.MapFrom(p => true));  

            // WorkItemType
            CreateMap<DB.WorkItemType, WorkItemTypeResponse>();
            CreateMap<CreateWorkItemTypeRequest, DB.WorkItemType>();
            CreateMap<EditWorkItemTypeRequest, DB.WorkItemType>();

            // WorkItem
            CreateMap<DB.WorkItem, WorkItemResponse>()
                .ForMember(d => d.WorkItemTypeName, s => s.MapFrom(p => p.WorkItemType.Name));
            CreateMap<CreateWorkItemRequest, DB.WorkItem>();
            CreateMap<EditWorkItemRequest, DB.WorkItem>();

            // WorkItemTestcaseAssoc
            CreateMap<DB.WorkItemTestcaseAssoc, WorkItemTestcaseAssocResponse>()
                .ForMember(d => d.WorkItemName, s => s.MapFrom(p => p.WorkItem.Name))
                .ForMember(d => d.TestcaseName, s => s.MapFrom(p => p.Testcase.TestFlowName));
            CreateMap<CreateWorkItemTestcaseAssocRequest, DB.WorkItemTestcaseAssoc>();
            CreateMap<EditWorkItemTestcaseAssocRequest, DB.WorkItemTestcaseAssoc>();

            // Integrations
            CreateMap<DB.Integration, IntegrationResponse>()
                .ForMember(d => d.SourceName, s => s.MapFrom(p => p.Source.Name))
                .ForMember(d => d.Readme, s => s.MapFrom(p => p.Source.Readme))
                .ForMember(d => d.Logo, s => s.MapFrom(p => p.Source.Logo));
            
            CreateMap<CreateIntegrationRequest, DB.Integration>();
        }
    }
}
