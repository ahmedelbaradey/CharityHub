using CharityHub.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharityHub.Infrastructure.Data.Config
{
    public class AccountRoleConfig : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.ToTable("AccountRoles");
            builder.HasKey(pc => new { pc.AccountId, pc.RoleId });
            builder.HasOne("CharityHub.Domain.Entities.Identities.Role", "Role")
                .WithMany("AccountRoles")
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasOne("CharityHub.Domain.Entities.Identities.Account", "Account")
                .WithMany("AccountRoles")
                .HasForeignKey("AccountId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.Navigation("Role");
            builder.Navigation("Account");
        }
    }
}
