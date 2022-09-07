using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleApi.Core.ProjectAggregate;

namespace SimpleApi.Infrastructure.Data.Config
{
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.StockTicker)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Exchange)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(p => p.Isin)
                .IsUnique();

            builder.Property(p => p.Isin)
                .HasMaxLength(12)
                .HasColumnType("NVARCHAR")
                .IsRequired();          

            builder.Property(p => p.Website)
                .HasMaxLength(100); ;
        }
    }
}
