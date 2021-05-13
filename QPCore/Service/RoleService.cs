using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Roles;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRepository<Role> roleRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public bool CheckExistedId(int id)
        {
            var query = _roleRepository.GetQuery()
                        .Any(p => p.RoleId == id);

            return query;
        }

        public List<RoleResponse> GetAll()
        {
            var query = _roleRepository.GetQuery()
                .ProjectTo<RoleResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return query;
        }

    }
}
