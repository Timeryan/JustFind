using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.EntityConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).IsRequired().HasMaxLength(400);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
                    
            builder.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(s => s.AuthorId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);;
                    
            builder.HasOne(x => x.AdOwner)
                .WithMany()
                .HasForeignKey(s => s.AdId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}