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

namespace QPCore.Service
{
    public class CompositeWebElementService : BaseGroupService<CompositeWebElement, CompositeWebElementResponse, CreateCompositeWebElementRequest, EditCompositeWebElementRequest>, ICompositeWebElementService
    {
        public CompositeWebElementService(IBaseRepository<CompositeWebElement> repository, IMapper mapper) : base(repository, mapper)
        {
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