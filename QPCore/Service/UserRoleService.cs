using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.UserRoles;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<UserRole> _repository;
        private readonly IMapper _mapper;

        public UserRoleService(IRepository<UserRole> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public CheckUniqueResponse CheckUnique(int userId, int roleId, int? userRoleId = null)
        {
            var isExisted = _repository.GetQuery()
                .Any(p => p.UserId == userId
                            && p.RoleId == roleId
                            && (!userRoleId.HasValue || (userRoleId.HasValue && p.UserRoleId == userRoleId.Value)));

            var result = new CheckUniqueResponse()
            {
                IsUnique = !isExisted
            };

            return result;

        }

        public async Task<UserRoleResponse> CreateAsync(CreateUserRoleRequest createUserRoleRequest, int userId)
        {
            var data = _mapper.Map<UserRole>(createUserRoleRequest);
            data.CreatedBy = userId;
            data.CreatedDate = DateTime.Now;

            var newObject = await _repository.AddAsync(data);

            var response = GetById(newObject.UserRoleId);

            return response;
        }

        public async Task DeleteAsync(int userRoleId)
        {
            await _repository.DeleteAsync(userRoleId);
        }

        public UserRoleResponse GetById(int userRoleId)
        {
            var query = _repository.GetQuery()
                        .Where(p => p.UserRoleId == userRoleId)
                        .ProjectTo<UserRoleResponse>(_mapper.ConfigurationProvider)
                        .FirstOrDefault();

            return query;
        }

        public List<UserRoleInUserResponse> GetByRoleId(int roleId)
        {
            var query = _repository.GetQuery()
                         .Where(p => p.RoleId == roleId)
                         .ProjectTo<UserRoleInUserResponse>(_mapper.ConfigurationProvider)
                         .ToList();

            return query;
        }

        public List<UserRoleInRoleResponse> GetByUserId(int userId)
        {
            var query = _repository.GetQuery()
                         .Where(p => p.UserId == userId)
                         .ProjectTo<UserRoleInRoleResponse>(_mapper.ConfigurationProvider)
                         .ToList();

            return query;
        }

        public bool CheckExistingId(int id)
        {
            var query = _repository.GetQuery()
                        .Any(p => p.UserRoleId == id);

            return query;
        }
    }
}
