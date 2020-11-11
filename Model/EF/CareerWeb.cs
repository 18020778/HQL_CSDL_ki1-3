namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CareerWeb : DbContext
    {
        public CareerWeb()
            : base("name=CareerWeb")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }
        public virtual DbSet<JobMajor> JobMajors { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LevelLearning> LevelLearnings { get; set; }
        public virtual DbSet<OfferJob> OfferJobs { get; set; }
        public virtual DbSet<OfferJobSkill> OfferJobSkills { get; set; }
        public virtual DbSet<PositionEmployee> PositionEmployees { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<TypeOfEnterprise> TypeOfEnterprises { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCertificate> UserCertificates { get; set; }
        public virtual DbSet<UserExperience> UserExperiences { get; set; }
        public virtual DbSet<UserForeignLanguage> UserForeignLanguages { get; set; }
        public virtual DbSet<UserLearning> UserLearnings { get; set; }
        public virtual DbSet<WorkInvitation> WorkInvitations { get; set; }
        public virtual DbSet<AppliedCandidate> AppliedCandidates { get; set; }
        public virtual DbSet<EnterpriseArea> EnterpriseAreas { get; set; }
        public virtual DbSet<EnterpriseJob> EnterpriseJobs { get; set; }
        public virtual DbSet<EnterpriseSize> EnterpriseSizes { get; set; }
        public virtual DbSet<SavedCandidate> SavedCandidates { get; set; }
        public virtual DbSet<UserMajor> UserMajors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.AccountName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccountPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.TypeOfAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeID)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.ImageLogo)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.UrlAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Interview>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<Interview>()
                .Property(e => e.Time)
                .IsUnicode(false);

            modelBuilder.Entity<Interview>()
                .Property(e => e.Date)
                .IsUnicode(false);

            modelBuilder.Entity<Interview>()
                .Property(e => e.EmployeeID)
                .IsUnicode(false);

            modelBuilder.Entity<OfferJob>()
                .Property(e => e.EmployeeID)
                .IsUnicode(false);

            modelBuilder.Entity<OfferJob>()
                .Property(e => e.OfferImage)
                .IsUnicode(false);

            modelBuilder.Entity<OfferJob>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<OfferJob>()
                .Property(e => e.ContactEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserMobile)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserImage)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.School)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.VideoProfile)
                .IsUnicode(false);

            modelBuilder.Entity<UserCertificate>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<UserCertificate>()
                .Property(e => e.ImageCertificate)
                .IsUnicode(false);

            modelBuilder.Entity<UserCertificate>()
                .Property(e => e.GetDate)
                .IsUnicode(false);

            modelBuilder.Entity<UserExperience>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<UserExperience>()
                .Property(e => e.StartTime)
                .IsUnicode(false);

            modelBuilder.Entity<UserExperience>()
                .Property(e => e.EndTime)
                .IsUnicode(false);

            modelBuilder.Entity<UserForeignLanguage>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<UserForeignLanguage>()
                .Property(e => e.LanguageLevel)
                .IsUnicode(false);

            modelBuilder.Entity<UserLearning>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<UserLearning>()
                .Property(e => e.TimeStart)
                .IsUnicode(false);

            modelBuilder.Entity<UserLearning>()
                .Property(e => e.TimeEnd)
                .IsUnicode(false);

            modelBuilder.Entity<WorkInvitation>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<WorkInvitation>()
                .Property(e => e.Salary)
                .IsUnicode(false);

            modelBuilder.Entity<AppliedCandidate>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<AppliedCandidate>()
                .Property(e => e.CreateDate)
                .IsUnicode(false);

            modelBuilder.Entity<EnterpriseSize>()
                .Property(e => e.AmountSize)
                .IsUnicode(false);

            modelBuilder.Entity<SavedCandidate>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<SavedCandidate>()
                .Property(e => e.CreateDate)
                .IsUnicode(false);

            modelBuilder.Entity<UserMajor>()
                .Property(e => e.UserID)
                .IsUnicode(false);
        }
    }
}
