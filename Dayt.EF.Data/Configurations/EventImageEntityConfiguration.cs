using Dayt.EF.Data.Entities;
using Dayt.EF.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dayt.EF.Data.Configurations;

internal class EventImageEntityConfiguration : IEntityTypeConfiguration<EventImageEntity>
{
    public void Configure(EntityTypeBuilder<EventImageEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(x => x.ImageType)
            .IsRequired()
            .HasConversion(v => v.ToString(), v => (ImageTypeEnum)Enum.Parse(typeof(ImageTypeEnum), v));
        builder.Property(x => x.Order)
            .IsRequired();
        builder.HasOne(i => i.Event)
              .WithMany(e => e.Images)
              .HasForeignKey(i => i.EventId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}