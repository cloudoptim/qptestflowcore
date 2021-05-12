﻿using Microsoft.EntityFrameworkCore;
using QPCore.Data.Enitites;

#nullable disable

namespace QPCore.Data
{
    public partial class QPContext : DbContext
    {
        public QPContext()
        {
        }

        public QPContext(DbContextOptions<QPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationFeature> ApplicationFeatures { get; set; }
        public virtual DbSet<ConfigTestFlowConfig> ConfigTestFlowConfigs { get; set; }
        public virtual DbSet<ConfigTestFlowConfigValue> ConfigTestFlowConfigValues { get; set; }
        public virtual DbSet<JStepresult> JStepresults { get; set; }
        public virtual DbSet<OrgUser> OrgUsers { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RunConfiguration> RunConfigurations { get; set; }
        public virtual DbSet<RunLocalResult> RunLocalResults { get; set; }
        public virtual DbSet<RunTestBatch> RunTestBatches { get; set; }
        public virtual DbSet<RunTestCase> RunTestCases { get; set; }
        public virtual DbSet<RunTestColumn> RunTestColumns { get; set; }
        public virtual DbSet<RunTestRow> RunTestRows { get; set; }
        public virtual DbSet<RunTestRun> RunTestRuns { get; set; }
        public virtual DbSet<RunTestStep> RunTestSteps { get; set; }
        public virtual DbSet<StepGlossary> StepGlossaries { get; set; }
        public virtual DbSet<StepGlossaryAssoc> StepGlossaryAssocs { get; set; }
        public virtual DbSet<StepGlossaryColumn> StepGlossaryColumns { get; set; }
        public virtual DbSet<StepGlossaryFeatureAssoc> StepGlossaryFeatureAssocs { get; set; }
        public virtual DbSet<StepGlossaryRow> StepGlossaryRows { get; set; }
        public virtual DbSet<TestFlow> TestFlows { get; set; }
        public virtual DbSet<TestFlowCategory> TestFlowCategories { get; set; }
        public virtual DbSet<TestFlowCategoryAssoc> TestFlowCategoryAssocs { get; set; }
        public virtual DbSet<TestFlowColumn> TestFlowColumns { get; set; }
        public virtual DbSet<TestFlowRow> TestFlowRows { get; set; }
        public virtual DbSet<TestFlowStep> TestFlowSteps { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<WebCommand> WebCommands { get; set; }
        public virtual DbSet<WebElement> WebElements { get; set; }
        public virtual DbSet<WebModel> WebModels { get; set; }
        public virtual DbSet<WebModelGroup> WebModelGroups { get; set; }
        public virtual DbSet<WebModelProp> WebModelProps { get; set; }
        public virtual DbSet<WebPage> WebPages { get; set; }
        public virtual DbSet<WebPageGroup> WebPageGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Userclientid)
                    .HasName("userclientassoc");

                entity.Property(e => e.Userclientid)
                    .ValueGeneratedNever()
                    .HasColumnName("userclientid");

                entity.Property(e => e.Client).HasColumnName("client");

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(e => e.Application)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.Client)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_application_appuser_client");

                entity.HasOne(e => e.OrgUser)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orguser_appuser_userid");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("clientId");

                entity.ToTable("Application");

                entity.HasIndex(e => e.ApplicationId, "ApplicationId")
                    .IsUnique();

                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.ApplicationName).HasMaxLength(255);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .HasColumnName("createdBy");

                entity.Property(e => e.CreatedDateTime).HasColumnType("date");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.Orgid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AppOrgId");
            });

            modelBuilder.Entity<ApplicationFeature>(entity =>
            {
                entity.HasKey(e => e.AppFeatureId)
                    .HasName("ApplicationFeatures_pkey");

                entity.Property(e => e.AppFeatureId).ValueGeneratedNever();

                entity.Property(e => e.FeatureName).HasMaxLength(255);
            });

            modelBuilder.Entity<ConfigTestFlowConfig>(entity =>
            {
                entity.HasKey(e => e.ConfigId)
                    .HasName("TestFlowConfig_pkey");

                entity.ToTable("Config.TestFlowConfig");

                entity.Property(e => e.ConfigId).ValueGeneratedNever();

                entity.Property(e => e.ConfigName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ConfigTestFlowConfigs)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestConfig_Client");
            });

            modelBuilder.Entity<ConfigTestFlowConfigValue>(entity =>
            {
                entity.HasKey(e => e.PairId)
                    .HasName("Config.TestFlowConfigValues_pkey");

                entity.ToTable("Config.TestFlowConfigValues");

                entity.Property(e => e.PairId).ValueGeneratedNever();

                entity.Property(e => e.KeyName).HasMaxLength(200);

                entity.Property(e => e.KeyValue).HasMaxLength(1000);

                entity.HasOne(d => d.Config)
                    .WithMany(p => p.ConfigTestFlowConfigValues)
                    .HasForeignKey(d => d.ConfigId)
                    .HasConstraintName("ConfigKeyvalue");
            });

            modelBuilder.Entity<JStepresult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("j_stepresult");

                entity.Property(e => e.Georesult)
                    .HasColumnType("json")
                    .HasColumnName("georesult");
            });

            modelBuilder.Entity<OrgUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("orguser");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userid");

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("loginname");

                entity.Property(e => e.OrgId).HasColumnName("orgid");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .HasColumnName("password");

                entity.Property(e => e.PasswordReset)
                    .HasColumnName("passwordreset");

                entity.Property(e => e.UseWindowsAuth)
                    .HasColumnType("bit(1)")
                    .HasColumnName("usewindowsauth");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnName("email");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnName("created");

                entity.Property(e => e.VerificationToken)
                    .HasMaxLength(500)
                    .HasColumnName("verificationtoken");

                entity.Property(e => e.ResetToken)
                    .HasMaxLength(500)
                    .HasColumnName("resettoken");

                entity.Property(e => e.ResetTokenExpires)
                    .HasColumnName("resettokenexpires");

                entity.Property(e => e.Verified)
                    .HasColumnName("verified");
        
                entity.HasOne(d => d.Org)
                    .WithMany(p => p.OrgUsers)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orguser_fk");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrgId)
                    .HasName("orgid");

                entity.ToTable("Organization");

                entity.Property(e => e.OrgId)
                    .HasIdentityOptions(startValue: 1)
                    .HasColumnName("orgid");

                entity.Property(e => e.CreatedBy)
                   .HasColumnName("createdby");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedby");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createddate");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updateddate");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrgName).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("roleid");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isactive");

                entity.Property(e => e.Rolename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("rolename");

                entity.Property(e => e.RoleCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("rolecode");

                entity.Property(e => e.IsSystem)
                    .IsRequired()
                    .HasColumnName("issystem");

                entity.Property(e => e.IsDefault)
                    .IsRequired()
                    .HasColumnName("isdefault");
            });

            modelBuilder.Entity<RunConfiguration>(entity =>
            {
                entity.HasKey(e => e.RunCofigId)
                    .HasName("Run.Configuration_pkey");

                entity.ToTable("Run.Configuration");

                entity.Property(e => e.RunCofigId).ValueGeneratedNever();

                entity.Property(e => e.TestRunConfigName).HasMaxLength(255);

                entity.Property(e => e.TestRunKeyName).HasColumnType("character varying");

                entity.Property(e => e.TestRunKeyValue).HasMaxLength(500);
            });

            modelBuilder.Entity<RunLocalResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Run.LocalResults");

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TestResults).HasColumnType("json");
            });

            modelBuilder.Entity<RunTestBatch>(entity =>
            {
                entity.HasKey(e => e.RunBatchId)
                    .HasName("Run.TestBatch_pkey");

                entity.ToTable("Run.TestBatch");

                entity.Property(e => e.RunBatchId).ValueGeneratedNever();

                entity.Property(e => e.BatchOutcome).HasMaxLength(50);

                entity.Property(e => e.RunBy).HasMaxLength(150);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RunTestBatches)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RunbatchApplicationId");
            });

            modelBuilder.Entity<RunTestCase>(entity =>
            {
                entity.ToTable("Run.TestCase");

                entity.Property(e => e.RunTestCaseId).ValueGeneratedNever();

                entity.Property(e => e.ConfigId).HasColumnName("configId");

                entity.Property(e => e.TestCaseRunStatus).HasMaxLength(50);

                entity.Property(e => e.TestcaseName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.TestRun)
                    .WithMany(p => p.RunTestCases)
                    .HasForeignKey(d => d.TestRunId)
                    .HasConstraintName("Run.TestCase_TestRunId_fkey");
            });

            modelBuilder.Entity<RunTestColumn>(entity =>
            {
                entity.HasKey(e => e.ColumnId)
                    .HasName("runtestcolumn");

                entity.ToTable("Run.TestColumn");

                entity.Property(e => e.ColumnId).ValueGeneratedNever();

                entity.Property(e => e.ColumnName).HasMaxLength(255);

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.RunTestColumns)
                    .HasForeignKey(d => d.StepId)
                    .HasConstraintName("Run.TestColumn_StepId_fkey");
            });

            modelBuilder.Entity<RunTestRow>(entity =>
            {
                entity.HasKey(e => e.RowId)
                    .HasName("Run.TestRow_pkey");

                entity.ToTable("Run.TestRow");

                entity.Property(e => e.RowId).ValueGeneratedNever();

                entity.Property(e => e.RowValue).HasMaxLength(2000);

                entity.HasOne(d => d.Column)
                    .WithMany(p => p.RunTestRows)
                    .HasForeignKey(d => d.ColumnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Run.TestRow_ColumnId_fkey");
            });

            modelBuilder.Entity<RunTestRun>(entity =>
            {
                entity.HasKey(e => e.RunId)
                    .HasName("Run.TestRun_pkey");

                entity.ToTable("Run.TestRun");

                entity.Property(e => e.RunId).ValueGeneratedNever();

                entity.Property(e => e.ApplicationName).HasMaxLength(250);

                entity.Property(e => e.RunStatus).HasMaxLength(50);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.RunTestRuns)
                    .HasForeignKey(d => d.Batchid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Run.TestRun_BatchRunId_fkey");
            });

            modelBuilder.Entity<RunTestStep>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Run.TestSteps");

                entity.Property(e => e.StepStatus).HasMaxLength(50);

                entity.Property(e => e.TestStepName).HasMaxLength(255);
            });

            modelBuilder.Entity<StepGlossary>(entity =>
            {
                entity.HasKey(e => e.StepId)
                    .HasName("StepGlossaryStepid");

                entity.ToTable("StepGlossary");

                entity.Property(e => e.StepId).ValueGeneratedNever();

                entity.Property(e => e.DisplayStepName).HasMaxLength(8000);

                entity.Property(e => e.StepDataType).HasMaxLength(50);

                entity.Property(e => e.StepDescription).HasMaxLength(8000);

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(8000);

                entity.Property(e => e.StepSource).HasMaxLength(150);

                entity.Property(e => e.StepType).HasMaxLength(50);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.StepGlossaries)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StepGlossaryClientAssoc");
            });

            modelBuilder.Entity<StepGlossaryAssoc>(entity =>
            {
                entity.HasKey(e => e.StepAssocId)
                    .HasName("StepGlossaeryAssoc");

                entity.ToTable("StepGlossaryAssoc");

                entity.Property(e => e.StepAssocId).ValueGeneratedNever();
            });

            modelBuilder.Entity<StepGlossaryColumn>(entity =>
            {
                entity.HasKey(e => e.ColumnId)
                    .HasName("StepGlossColumn");

                entity.ToTable("StepGlossaryColumn");

                entity.Property(e => e.ColumnId).ValueGeneratedNever();

                entity.Property(e => e.ColumnName).HasMaxLength(255);

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.StepGlossaryColumns)
                    .HasForeignKey(d => d.StepId)
                    .HasConstraintName("StepColumGlossAssoc");
            });

            modelBuilder.Entity<StepGlossaryFeatureAssoc>(entity =>
            {
                entity.HasKey(e => e.FeatureAssocId)
                    .HasName("StepFeaureAssoc");

                entity.ToTable("StepGlossaryFeatureAssoc");

                entity.Property(e => e.FeatureAssocId).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.StepGlossaryFeatureAssocs)
                    .HasForeignKey(d => d.Featureid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FeaureAssoc");

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.StepGlossaryFeatureAssocs)
                    .HasForeignKey(d => d.StepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StepGlossaryAssoc");
            });

            modelBuilder.Entity<StepGlossaryRow>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StepGlossaryRow");

                entity.Property(e => e.RowValue).HasMaxLength(2000);
            });

            modelBuilder.Entity<TestFlow>(entity =>
            {
                entity.ToTable("TestFlow");

                entity.Property(e => e.TestFlowId).ValueGeneratedNever();

                entity.Property(e => e.AssignedDatetTime).HasColumnType("date");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("date");

                entity.Property(e => e.SourceFeatureName).HasMaxLength(1000);

                entity.Property(e => e.TestFlowDescription).HasMaxLength(2000);

                entity.Property(e => e.TestFlowName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.TestFlowStatus).HasMaxLength(50);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TestFlows)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestFlowClient");
            });

            modelBuilder.Entity<TestFlowCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("TestFlowCategory_pkey");

                entity.ToTable("TestFlowCategory");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TestFlowCategories)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestFlowCategoryClient_fk");
            });

            modelBuilder.Entity<TestFlowCategoryAssoc>(entity =>
            {
                entity.HasKey(e => e.TestFlowCatAssocId)
                    .HasName("TestFlowCategoryAssoc_pkey");

                entity.ToTable("TestFlowCategoryAssoc");

                entity.Property(e => e.TestFlowCatAssocId).ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TestFlowCategoryAssocs)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestFlowCategoryAssoc_Cat_fk");

                entity.HasOne(d => d.TestFlow)
                    .WithMany(p => p.TestFlowCategoryAssocs)
                    .HasForeignKey(d => d.TestFlowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestFlowCategoryAssoc_Testflow_Fk");
            });

            modelBuilder.Entity<TestFlowColumn>(entity =>
            {
                entity.HasKey(e => e.ColumnId)
                    .HasName("TestFlowColumn_pk");

                entity.ToTable("TestFlowColumn");

                entity.Property(e => e.ColumnId).ValueGeneratedNever();

                entity.Property(e => e.ColumnName).HasMaxLength(255);

                entity.HasOne(d => d.TestFlowStep)
                    .WithMany(p => p.TestFlowColumns)
                    .HasForeignKey(d => d.TestFlowStepId)
                    .HasConstraintName("TestFlowColumn_fk");
            });

            modelBuilder.Entity<TestFlowRow>(entity =>
            {
                entity.HasKey(e => e.RowId)
                    .HasName("TestFlowRow_pkey");

                entity.ToTable("TestFlowRow");

                entity.Property(e => e.RowId).ValueGeneratedNever();

                entity.Property(e => e.RowValue).HasMaxLength(2000);
            });

            modelBuilder.Entity<TestFlowStep>(entity =>
            {
                entity.ToTable("TestFlowStep");

                entity.Property(e => e.TestFlowStepId).ValueGeneratedNever();

                entity.Property(e => e.ResourceType).HasMaxLength(50);

                entity.Property(e => e.TestFlowStepDataType).HasMaxLength(50);

                entity.Property(e => e.TestFlowStepDescription).HasMaxLength(8000);

                entity.Property(e => e.TestFlowStepName)
                    .IsRequired()
                    .HasMaxLength(8000);

                entity.Property(e => e.TestFlowStepSource).HasMaxLength(150);

                entity.Property(e => e.TestFlowStepType).HasMaxLength(50);

                entity.HasOne(d => d.TestFlow)
                    .WithMany(p => p.TestFlowSteps)
                    .HasForeignKey(d => d.TestFlowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestFLows");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.ClientRoleAssoc)
                    .HasName("pk_userrole_clientroleassoc");

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleid");

                entity.Property(e => e.ClientRoleAssoc)
                    .HasIdentityOptions(startValue: 100)
                    .HasColumnName("clientroleassoc");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("isactive");

                entity.Property(e => e.UserClientId)
                    .HasColumnName("userclientid");

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_role_userrole_roleid_roleid");

                entity.HasOne(ur => ur.OrgUser)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orguser_userrole_userclientid_userid");
            });

            modelBuilder.Entity<WebCommand>(entity =>
            {
                entity.HasKey(e => e.CommandId)
                    .HasName("CommandId");

                entity.Property(e => e.CommandId).ValueGeneratedNever();

                entity.Property(e => e.CommandDescription).HasMaxLength(2000);

                entity.Property(e => e.CommandName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CommandSource).HasMaxLength(200);

                entity.Property(e => e.CommandType).HasMaxLength(100);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.WebCommands)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CommandClientId");
            });

            modelBuilder.Entity<WebElement>(entity =>
            {
                entity.HasKey(e => e.Elementid)
                    .HasName("elementid");

                entity.ToTable("WebElement");

                entity.Property(e => e.Elementid)
                    .ValueGeneratedNever()
                    .HasColumnName("elementid");

                entity.Property(e => e.Applicationsection)
                    .HasMaxLength(250)
                    .HasColumnName("applicationsection");

                entity.Property(e => e.Command)
                    .HasMaxLength(250)
                    .HasColumnName("command");

                entity.Property(e => e.Elementaliasname)
                    .HasMaxLength(550)
                    .HasColumnName("elementaliasname");

                entity.Property(e => e.Elementparentid).HasColumnName("elementparentid");

                entity.Property(e => e.Elementtype)
                    .HasMaxLength(150)
                    .HasColumnName("elementtype");

                entity.Property(e => e.Framenavigation)
                    .HasMaxLength(1000)
                    .HasColumnName("framenavigation");

                entity.Property(e => e.Itype)
                    .HasMaxLength(250)
                    .HasColumnName("itype");

                entity.Property(e => e.Ivalue)
                    .HasMaxLength(550)
                    .HasColumnName("ivalue");

                entity.Property(e => e.Locationpath)
                    .HasMaxLength(250)
                    .HasColumnName("locationpath");

                entity.Property(e => e.Pageid).HasColumnName("pageid");

                entity.Property(e => e.Screenshot)
                    .HasMaxLength(8000)
                    .HasColumnName("screenshot");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<WebModel>(entity =>
            {
                entity.HasKey(e => e.ModelId)
                    .HasName("modelid");

                entity.ToTable("WebModel");

                entity.Property(e => e.ModelId).ValueGeneratedNever();

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FeatureName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<WebModelGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("Web_Model_Group_pkey");

                entity.ToTable("WebModelGroup");

                entity.Property(e => e.GroupId).ValueGeneratedNever();

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.WebModelGroups)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ModelGroup");
            });

            modelBuilder.Entity<WebModelProp>(entity =>
            {
                entity.HasKey(e => e.PropId)
                    .HasName("ModelPropId");

                entity.ToTable("WebModelProp");

                entity.Property(e => e.PropId).ValueGeneratedNever();

                entity.Property(e => e.PropName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PropType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ValidationExpression).HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.WebModelProps)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ModelGroupProp");
            });

            modelBuilder.Entity<WebPage>(entity =>
            {
                entity.HasKey(e => e.Pageid)
                    .HasName("webpageid");

                entity.ToTable("WebPage");

                entity.Property(e => e.Pageid)
                    .ValueGeneratedNever()
                    .HasColumnName("pageid");

                entity.Property(e => e.Createdby)
                    .HasMaxLength(100)
                    .HasColumnName("createdby");

                entity.Property(e => e.Createddatetime)
                    .HasColumnType("date")
                    .HasColumnName("createddatetime");

                entity.Property(e => e.Groupid).HasColumnName("groupid");

                entity.Property(e => e.Isactive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isactive");

                entity.Property(e => e.Pagename)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("pagename");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("updatedBy");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnType("date")
                    .HasColumnName("updateddatetime");
            });

            modelBuilder.Entity<WebPageGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WebPageGroup");

                entity.Property(e => e.Createdby)
                    .HasMaxLength(100)
                    .HasColumnName("createdby");

                entity.Property(e => e.Createddatetime)
                    .HasColumnType("date")
                    .HasColumnName("createddatetime");

                entity.Property(e => e.Groupname)
                    .HasMaxLength(250)
                    .HasColumnName("groupname");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Updatedby)
                    .HasMaxLength(100)
                    .HasColumnName("updatedby");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnType("date")
                    .HasColumnName("updateddatetime");

                entity.Property(e => e.Versionid).HasColumnName("versionid");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.HasKey(e => e.RefreshTokenId)
                    .HasName("pr_refreshtoken_refreshtokenid");

                entity.Property(e => e.RefreshTokenId)
                    .HasColumnType("serial")
                    .HasColumnName("refreshtokenid");

                entity.Property(e => e.OrgUserId)
                    .HasColumnName("orguserid");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Expires)
                    .IsRequired()
                    .HasColumnName("expires");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnName("created");

                entity.Property(e => e.CreatedByIp)
                    .HasColumnName("createdbyip")
                    .HasMaxLength(15);

                entity.Property(e => e.Revoked)
                    .HasColumnName("revoked");

                entity.Property(e => e.RevokedByIp)
                    .HasColumnName("revokedbyip")
                    .HasMaxLength(15);

                entity.Property(e => e.ReplacedByToken)
                    .HasColumnName("replacedbytoken")
                    .HasMaxLength(4000);

                entity.HasOne(e => e.OrgUser)
                    .WithMany(o => o.RefreshTokens)
                    .HasForeignKey(k => k.OrgUserId)
                    .HasConstraintName("fk_orgusers_refreshtoken_orguserid");
            });

            modelBuilder.HasSequence("applicationfeaturesseq");

            modelBuilder.HasSequence("batchseq");

            modelBuilder.HasSequence("cmdseq");

            modelBuilder.HasSequence("configkeyseq");

            modelBuilder.HasSequence("configseq");

            modelBuilder.HasSequence("localrunseq");

            modelBuilder.HasSequence("runcolseq");

            modelBuilder.HasSequence("runconfigseq");

            modelBuilder.HasSequence("runrowseq");

            modelBuilder.HasSequence("runseq");

            modelBuilder.HasSequence("runstepseq");

            modelBuilder.HasSequence("runtestseq");

            modelBuilder.HasSequence("stepcolseq");

            modelBuilder.HasSequence("steprowseq");

            modelBuilder.HasSequence("stepseq");

            modelBuilder.HasSequence("test");

            modelBuilder.HasSequence("tfcolseq");

            modelBuilder.HasSequence("tfrowseq");

            modelBuilder.HasSequence("tfseq");

            modelBuilder.HasSequence("tfstepseq");

            modelBuilder.HasSequence("webelement");

            modelBuilder.HasSequence("webelementgroup").HasMin(0);

            modelBuilder.HasSequence("webelementpage");

            modelBuilder.HasSequence("webmodelgroupseq");

            modelBuilder.HasSequence("webmodelpropseq").HasMin(0);

            modelBuilder.HasSequence("webmodelseq").HasMin(0);

            modelBuilder.HasSequence("refreshtokenseq").HasMin(0);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
