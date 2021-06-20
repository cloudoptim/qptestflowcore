using QPCore.Service.Interfaces;
using QPCore.Service;
using QPCore.Data.Enitites;
using QPCore.Model.CompositeWebElements;
using QPCore.Data;
using AutoMapper;
using System.Threading.Tasks;
using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;

namespace QPCore.Service
{
    public class CompositeWebElementService : BaseGroupService<CompositeWebElement, CompositeWebElementResponse, CreateCompositeWebElementRequest, EditCompositeWebElementRequest>, ICompositeWebElementService
    {
        public CompositeWebElementService(IBaseRepository<CompositeWebElement> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<CompositeWebElementResponse> EditAsync(EditCompositeWebElementRequest entity, int userId)
        {
            var data = this.Repository.GetQuery().FirstOrDefault(p => p.Id == entity.Id);
            if (data != null)
            {
                data.Name = entity.Name;
                data.GroupId = entity.GroupId;
                data.UpdatedBy = userId;
                data.UpdatedDate = DateTime.Now;

                await this.Repository.UpdateAsync(data);

                // Update child element
                var updatedChilds = new List<CompositeWebElement>();
                foreach (var item in entity.Childs)
                {
                    var childItem = this.Repository.GetQuery()
                        .FirstOrDefault(c => c.Id == item.Id && c.ParentId == entity.Id);
                    
                    if (childItem != null)
                    {
                        childItem.Command = item.Command;
                        childItem.Index = item.Index;
                        childItem.WebElementId = item.WebELementId;
                        childItem.UpdatedBy = userId;
                        childItem.UpdatedDate = DateTime.Now;

                        updatedChilds.Add(childItem);
                    }
                }

                await this.Repository.UpdateRangeAsync(updatedChilds);

                return GetById(entity.Id);
            }

            return null;
        }
        protected override CompositeWebElement ConvertEntity(CreateCompositeWebElementRequest entity, int userId)
        {
            var insertEntity = Mapper.Map<CompositeWebElement>(entity);
            insertEntity.CreatedBy = userId;
            insertEntity.CreatedDate = DateTime.Now;
            insertEntity.UpdatedBy = userId;
            insertEntity.UpdatedDate = DateTime.Now;

            Parallel.ForEach(insertEntity.Childs, child => {
                child.CreatedBy = userId;
                child.UpdatedBy = userId;
                child.GroupId = entity.GroupId;
                child.CreatedDate = DateTime.Now;
                child.UpdatedDate = DateTime.Now;
            });

            return insertEntity;
        }
    }
}