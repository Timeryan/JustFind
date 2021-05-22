using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.EntityConfiguration
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.KodBase64).IsRequired();
            
            builder.HasOne(x=>x.AdOwner)
                .WithMany(x=>x.Photos)
                .HasForeignKey(s => s.AdOwnerId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}