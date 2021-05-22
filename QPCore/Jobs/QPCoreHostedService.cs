using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QPCore.Jobs
{
    public class QPCoreHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly JobMetadata _jobMetadata;
        //private ILogger _logger;
        public IScheduler Scheduler { get; set; }

        public QPCoreHostedService(ISchedulerFactory
            schedulerFactory,
            JobMetadata jobMetadata,
            IJobFactory jobFactory,
            ILogger logger)
        {
            this._schedulerFactory = schedulerFactory;
            this._jobMetadata = jobMetadata;
            this._jobFactory = jobFactory;
            //this._logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler();
            Scheduler.JobFactory = _jobFactory;
            var job = CreateJob(_jobMetadata);
            var trigger = CreateTrigger(_jobMetadata);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);

            //_logger.LogInformation($"Start job: {_jobMetadata.JobName} at {DateTime.Now}");

        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);

            //_logger.LogInformation($"Stop job: {_jobMetadata.JobName} at {DateTime.Now}");
        }
        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithCronSchedule(jobMetadata.CronExpression)
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }
        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder
            .Create(jobMetadata.JobType)
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }
    }
}
