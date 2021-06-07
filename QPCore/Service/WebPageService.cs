using System.Collections.Generic;
using System.Linq;
using System.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.WebPages;
using QPCore.Service.Interfaces;
using System.Threading.Tasks;
// using Streamx.Linq.SQL;
// using Streamx.Linq.SQL.EFCore;
// using Microsoft.EntityFrameworkCore;
// using static Streamx.Linq.SQL.SQL;
// using static Streamx.Linq.SQL.PostgreSQL.SQL;
// using static Streamx.Linq.SQL.Library;
// using static Streamx.Linq.SQL.Directives;
// using static Streamx.Linq.SQL.AggregateFunctions;

namespace QPCore.Service
{
    public class WebPageService : BaseGroupService<WebPage, WebPageItemResponse, CreateWebPageRequest, EditWebPageRequest>, IWebPageService
    {
        public WebPageService(IBaseRepository<WebPage> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public List<WebPageItemResponse> GetByGroupId(int groupId)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.GroupId == groupId)
                .ProjectTo<WebPageItemResponse>(this.Mapper.ConfigurationProvider)
                .ToList();

            return result;
        }

        public List<ExistedBulkResponse> CheckExistedBulkName(ExistedBulkNameRequest data)
        {
            data.NameList
                .AsParallel()
                .ForAll(n => n = n.Trim().ToLower());

            var query = this.Repository.GetQuery()
                .Where(p => p.GroupId == data.GroupId
                    && data.NameList.Contains(p.Name.Trim().ToLower()))
                .Select(p => new
                {
                    Name = p.Name.Trim().ToLower(),
                    Id = p.Id
                })
                .ToList();

            var result = new List<ExistedBulkResponse>();

            foreach (var name in data.NameList)
            {
                var lookupItem = query.FirstOrDefault(p => p.Name == name);

                var item = new ExistedBulkResponse()
                {
                    Name = name,
                    IsExisted = lookupItem != null,
                    ExistedId = lookupItem?.Id
                };

                result.Add(item);
            }

            return result;
        }

        public async Task<List<BulkUpsertResponse>> UpsertsAsync(ExistedBulkNameRequest bulkRequest, int userId)
        {
            var checkingResult = this.CheckExistedBulkName(bulkRequest);

            var insertedItems = checkingResult.Where(p => !p.IsExisted).Distinct().ToList();
            if (insertedItems.Any())
            {
                var newItems = new List<WebPage>();
                foreach (var p in insertedItems)
                {
                    var newObject = new WebPage()
                    {
                        Name = p.Name,
                        GroupId = bulkRequest.GroupId,
                        CreatedBy = userId
                    };

                    newItems.Add(newObject);
                }

                await this.Repository.AddRangeAsync(newItems);
            }

            bulkRequest.NameList
                .AsParallel()
                .ForAll(n => n = n.Trim().ToLower());

            var query = this.Repository.GetQuery()
                .Where(p => p.GroupId == bulkRequest.GroupId
                    && bulkRequest.NameList.Contains(p.Name.Trim().ToLower()))
                .Select(p => new
                {
                    Name = p.Name.Trim().ToLower(),
                    Id = p.Id
                })
                .ToList();

            var result = new List<BulkUpsertResponse>();

            foreach (var name in bulkRequest.NameList)
            {
                var lookupItem = query.FirstOrDefault(p => p.Name == name);

                var item = new BulkUpsertResponse()
                {
                    Name = name,
                    Id = lookupItem.Id,
                    GroupId = bulkRequest.GroupId
                };

                result.Add(item);
            }

            return result;
        }

        // TODO: need to research again for Upsert Postgre for ef core
        //     public void Upserts(List<CreateWebPageRequest> items, int userId)
        //     {
        //         /*
        //             public void TestUpsert() {

        //             var newOrExisting = DbContext.Store.First();

        //             #region TestUpsert
        //             // There is a store which might already exist in the database.
        //             // Should we add it or update? (PK is not always the only UNIQUE KEY)
        //             newOrExisting.LastUpdate = DateTime.Now;

        //             var rows = DbContext.Database.Execute((Store store) => {
        //                 var view = store.@using((store.StoreId, store.AddressId, store.ManagerStaffId, store.LastUpdate));
        //                 INSERT().INTO(view);
        //                 VALUES(view.RowFrom(newOrExisting));
        //                 ON_DUPLICATE_KEY_UPDATE(() => store.LastUpdate = INSERTED_VALUES(store.LastUpdate));
        //             });

        //             Console.WriteLine($"{rows} rows affected");
        //             #endregion

        //             }
        //         */
        //         var insertedItems = items
        //             .AsQueryable()
        //             .ProjectTo<WebPage>(Mapper.ConfigurationProvider)
        //             .ToList();
        //         try
        //         {
        //             var query = this.Repository.QPDataContext.Database
        //             .Execute((WebPage entity) =>
        //             {
        //                 var view = entity.@using((entity.Name, entity.GroupId, entity.CreatedBy));
        //                 INSERT()
        //                     .INTO(view);
        //                 VALUES(view.RowsFrom(insertedItems));
        //                 ON_CONFLICT(entity == new WebPage { Name = entity.Name, GroupId = entity.GroupId, CreatedBy = userId })
        //                 .DO_NOTHING()
        //                 //.DO_UPDATE()
        //                 //.SET(() => { entity.UpdatedDate = System.DateTime.Now; })
        //                 ;
        //             });
        //         }
        //         catch (System.Exception ex)
        //         {

        //         }

        //     }
    }
}