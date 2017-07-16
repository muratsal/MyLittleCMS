using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Context
{
    public interface IEFContext
    {
    } 
    public class EFContext: DbContext , IEFContext
    {
        public EFContext():base("MyLittleCMS")
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<MailSetting> MailSetting { get; set; }
        public DbSet<MembershipUser> MembershipUser { get; set; }
        public DbSet<MembershipUserRole> MembershipUserRole { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Log> Log { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                    .Where(type => !string.IsNullOrEmpty(type.Namespace))
                                    .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);

        }
    }
}
