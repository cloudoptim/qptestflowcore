using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Organizations;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<Organization> _repository;
        private readonly IMapper _mapper;
        public OrganizationService(IRepository<Organization> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public OrganizationResponse GetById(int id)
        {
            var org = _repository.GetQuery()
                .Where(p => p.Orgid == id)
                .ProjectTo<OrganizationResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

            return org;
        }
    }
}
