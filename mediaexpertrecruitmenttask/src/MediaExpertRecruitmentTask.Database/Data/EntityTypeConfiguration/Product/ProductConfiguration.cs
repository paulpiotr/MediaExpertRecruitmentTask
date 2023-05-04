#region using

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace MediaExpertRecruitmentTask.Database.Data.EntityTypeConfiguration.Product;

internal class ProductConfiguration : IEntityTypeConfiguration<Core.Model.Product.Product>
{
    public void Configure(EntityTypeBuilder<Core.Model.Product.Product> builder)
    {
        builder.Property(p => p.Id)
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(p => p.Price)
            .HasPrecision(18, 4);
    }
}