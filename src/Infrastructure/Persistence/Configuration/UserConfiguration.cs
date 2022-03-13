using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistance.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.Role).IsRequired();
            builder.HasIndex(p => p.UserName).IsUnique();
            builder.Property(p => p.Deposit).HasPrecision(16,2);
        }
    }
}
