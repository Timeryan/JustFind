using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.EntityConfiguration
{
    public class PetitionConfiguration : IEntityTypeConfiguration<Petition>
    {
        public void Configure(EntityTypeBuilder<Petition> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired();
            
            builder.HasOne(x=>x.Ad)
                .WithMany()
                .HasForeignKey(s => s.AdId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}