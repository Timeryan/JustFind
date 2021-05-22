using System;
using Advertisement.Domain;
using Advertisement.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.EntityConfiguration
{
    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.HasKey(x => x.Id);
                
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
                
            builder.Property(x => x.Name).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.Price);
            
            builder.Property(x => x.Status)
                .HasConversion<string>(s => s.ToString(), 
                    s => Enum.Parse<Statuses>(s));
            builder.Property(x => x.Text);
            builder.Property(x => x.Views);
            builder.Property(x => x.LocationKladrId);
            builder.Property(x => x.LocationText);
            builder.Property(x => x.LocationX);
            builder.Property(x => x.LocationY);
            builder.Property(x => x.ModerationComment);


            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);;
            
            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(s => s.CategoryId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}