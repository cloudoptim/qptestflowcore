using Microsoft.Extensions.Logging;
using QPCore.Data;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QPCore.Jobs
{
    [DisallowConcurrentExecution]
    public class ReactiveIdleLockedTestFlowJob : IJob
    {
        private readonly ILogger _logger;
        private readonly QPContext _qpContext;

        public ReactiveIdleLockedTestFlowJob(ILogger logger,
            QPContext qpContext
            )
        {
            _logger = logger;
            _qpContext = qpContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{DateTime.Now} - Execute job reactive idle locked testflows");
            try
            {
                var query = @"UPDATE public.""TestFlow"" 
                                SET ""Islocked"" = FALSE,
                                    ""LockedBy"" = NULL
                                WHERE ""Islocked"" = TRUE 
                                    AND ( ""LastUpdatedDateTime"" IS NULL OR
                                        ((DATE_PART('day', CURRENT_TIMESTAMP - ""LastUpdatedDateTime"") * 24 + 
                                         DATE_PART('hour', CURRENT_TIMESTAMP - ""LastUpdatedDateTime"")) * 60 +
                                         DATE_PART('minute', CURRENT_TIMESTAMP - ""LastUpdatedDateTime"") > 60));";
                await _qpContext.Database.ExecuteSqlRawAsync(query);

                _logger.LogInformation($"{DateTime.Now} - Complete job reactive idle locked testflows");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when it's running reactive idle locked testflow job");
            }
        }
    }
}
