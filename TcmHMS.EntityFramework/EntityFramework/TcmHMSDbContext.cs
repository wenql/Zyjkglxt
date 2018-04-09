using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using TcmHMS.Authorization.Roles;
using TcmHMS.Authorization.Users;
using TcmHMS.Entities;
using TcmHMS.Entities.Constitution;
using TcmHMS.MultiTenancy;

namespace TcmHMS.EntityFramework
{
    public class TcmHMSDbContext : AbpZeroDbContext<Tenant, Role, User>
    {    
        public IDbSet<Department> Departments { get; set; }

        public IDbSet<Disease> Diseases { get; set; }

        public IDbSet<Rank> Ranks { get; set; }

        public IDbSet<Medicine> Medicines { get; set; }

        public IDbSet<ConstitutionSuggest> ConstitutionSuggests { get; set; }

        public IDbSet<ConstitutionSubject> ConstitutionSubjects { get; set; }

        public IDbSet<ConstitutionSubjectOption> ConstitutionSubjectOptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().ToTable("TcmDepartment")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Disease>().ToTable("TcmDisease")
                .HasKey(t => t.Id)
                .HasRequired(t => t.Department)
                .WithMany(t => t.Disease)
                .HasForeignKey(t => t.DepartmentId);

            modelBuilder.Entity<Rank>().ToTable("TcmRank")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Medicine>().ToTable("TcmMedicine")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ConstitutionSuggest>().ToTable("TcmConstitutionSuggest")
                .HasKey(t => t.Id);
            modelBuilder.Entity<ConstitutionSubject>().ToTable("TcmConstitutionSubject")
                .HasKey(t => t.Id);
            modelBuilder.Entity<ConstitutionSubjectOption>().ToTable("TcmConstitutionSubjectOption")
                .HasKey(t => t.Id)
                .HasRequired(o => o.Subject)
                .WithMany(s => s.Options)
                .HasForeignKey(t => t.SubjectId);
        }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public TcmHMSDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in TcmHMSDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of TcmHMSDbContext since ABP automatically handles it.
         */
        public TcmHMSDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public TcmHMSDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public TcmHMSDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
