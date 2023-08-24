using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using lat_brm.Models;

namespace lat_brm.Data
{
    public partial class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {
        }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbMAccount> TbMAccounts { get; set; } = null!;
        public virtual DbSet<TbMAccountRole> TbMAccountRoles { get; set; } = null!;
        public virtual DbSet<TbMEducation> TbMEducations { get; set; } = null!;
        public virtual DbSet<TbMEmployee> TbMEmployees { get; set; } = null!;
        public virtual DbSet<TbMRole> TbMRoles { get; set; } = null!;
        public virtual DbSet<TbMRoom> TbMRooms { get; set; } = null!;
        public virtual DbSet<TbMUniversity> TbMUniversities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<TbMAccount>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("tb_m_accounts");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ExpiredTime)
                    .HasColumnType("datetime")
                    .HasColumnName("expired_time");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.IsUsed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_used");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Otp)
                    .HasColumnType("int(11)")
                    .HasColumnName("otp");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.TbMAccount)
                    .HasForeignKey<TbMAccount>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_accounts_employees");
            });

            modelBuilder.Entity<TbMAccountRole>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("tb_m_account_roles");

                entity.HasIndex(e => e.AccountGuid, "account_guid");

                entity.HasIndex(e => e.RoleGuid, "role_guid");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid");

                entity.Property(e => e.AccountGuid)
                    .HasMaxLength(36)
                    .HasColumnName("account_guid");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.RoleGuid)
                    .HasMaxLength(36)
                    .HasColumnName("role_guid");

                entity.HasOne(d => d.AccountGu)
                    .WithMany(p => p.TbMAccountRoles)
                    .HasForeignKey(d => d.AccountGuid)
                    .HasConstraintName("tb_m_account_roles_ibfk_1");

                entity.HasOne(d => d.RoleGu)
                    .WithMany(p => p.TbMAccountRoles)
                    .HasForeignKey(d => d.RoleGuid)
                    .HasConstraintName("tb_m_account_roles_ibfk_2");
            });

            modelBuilder.Entity<TbMEducation>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("tb_m_educations");

                entity.HasIndex(e => e.UniversityGuid, "university_guid");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Degree)
                    .HasMaxLength(100)
                    .HasColumnName("degree")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Gpa).HasColumnName("gpa");

                entity.Property(e => e.Major)
                    .HasMaxLength(100)
                    .HasColumnName("major")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.UniversityGuid)
                    .HasMaxLength(36)
                    .HasColumnName("university_guid");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.TbMEducation)
                    .HasForeignKey<TbMEducation>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_m_educations_ibfk_2");

                entity.HasOne(d => d.UniversityGu)
                    .WithMany(p => p.TbMEducations)
                    .HasForeignKey(d => d.UniversityGuid)
                    .HasConstraintName("tb_m_educations_ibfk_1");
            });

            modelBuilder.Entity<TbMEmployee>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("tb_m_employees");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birth_date");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("first_name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Gender)
                    .HasColumnType("int(11)")
                    .HasColumnName("gender");

                entity.Property(e => e.HiringDate)
                    .HasColumnType("datetime")
                    .HasColumnName("hiring_date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("last_name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Nik)
                    .HasMaxLength(6)
                    .HasColumnName("nik")
                    .IsFixedLength()
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<TbMRole>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("tb_m_roles");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<TbMRoom>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("tb_m_rooms");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid");

                entity.Property(e => e.Capacity)
                    .HasColumnType("int(11)")
                    .HasColumnName("capacity");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Floor)
                    .HasColumnType("int(11)")
                    .HasColumnName("floor");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<TbMUniversity>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("tb_m_universities");

                entity.Property(e => e.Guid)
                    .HasMaxLength(36)
                    .HasColumnName("guid");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_daate");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
