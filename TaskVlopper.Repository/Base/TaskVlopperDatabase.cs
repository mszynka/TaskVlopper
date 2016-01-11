namespace TaskVlopper.Base.Repository
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    using System.Transactions;
    using Base;
    using TaskVlopper.Base.Model;

    public static class DatabasePicker
    {
        private static bool databaseForTests = false;
        public static void PickTestDatabase()
        {
            databaseForTests = true;
        }

        public static void PickNormalDatabase()
        {
            databaseForTests = false;
        }

        public static string GetConnectionStringName()
        {
            if (!databaseForTests)
                return "TaskVlopperDatabase";
            else return "TaskVlopperDatabaseForTests";
        }
    }

    public class TaskVlopperDatabase<T> : DbContext
    {
        
        static bool _init = false;
        static object _initLock = new object();


        public TaskVlopperDatabase()
            : base(DatabasePicker.GetConnectionStringName())
        {
            if (!_init)
            {
                lock (_initLock)
                {
                    if (!_init)
                    {
                        using (var noTransaction = new TransactionScope(TransactionScopeOption.Suppress))
                        {
                            _init = true;
                            this.Database.CreateIfNotExists();
                            noTransaction.Complete();
                        }
                    }
                }
            }

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Test>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Meeting>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Project>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Task>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Worklog>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MeetingParticipants>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserProjectAssignment>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserTaskAssignment>().Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }

}