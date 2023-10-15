using Jwt.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Infra.Contexts.AccountContexts.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder
                .Property(x => x.Image)
                .HasColumnName("Image")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .OwnsOne(x => x.Email)
                    .Property(x => x.Address)
                    .HasColumnName("Email")
                    .IsRequired();

            builder
                .OwnsOne(x => x.Email)
                    .OwnsOne(x => x.Verification)
                        .Property(x => x.Code)
                        .HasColumnName("EmailVerificationCode")
                        .IsRequired();

            builder
                .OwnsOne(x => x.Email)
                    .OwnsOne(x => x.Verification)
                        .Ignore(x => x.IsActive);

            builder
                .OwnsOne(x => x.Email)
                    .OwnsOne(x => x.Verification)
                        .Property(x => x.ExpiresAt)
                        .HasColumnName("EmailVerificationExpiresAt")
                        .IsRequired(false);

            builder
                .OwnsOne(x => x.Email)
                    .OwnsOne(x => x.Verification)
                        .Property(x => x.VerifiedAt)
                        .HasColumnName("EmailVerificationVerifiedAt")
                        .IsRequired(false);

            builder
                .OwnsOne(x => x.Password)
                    .Property(x => x.Hash)
                    .HasColumnName("PasswordHash")
                    .IsRequired();

            builder
                .OwnsOne(x => x.Password)
                    .Property(x => x.ResetCode)
                    .HasColumnName("PasswordResetCode")
                    .IsRequired();

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    user => user
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    role => role
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
