using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Context
{
    public partial class PostgresDbContext : DbContext
    {
        public PostgresDbContext()
        {
        }

        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersPermissionMapper> UsersPermissionMappers { get; set; }
        public virtual DbSet<UsersRolesMapper> UsersRolesMappers { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleDeviceMapper> VehicleDeviceMappers { get; set; }
        public virtual DbSet<VehicleLocationMapper> VehicleLocationMappers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localHost:5432;Database=vehicle-tracking-system;Username=postgres;Password=00000000; Trust Server Certificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("devices");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("device_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("brand");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.DeviceNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("device_number");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permissions");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modified_date");

                entity.Property(e => e.PermissionDescription)
                    .HasMaxLength(1000)
                    .HasColumnName("permission_description");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(100)
                    .HasColumnName("permission_name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modified_date");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<UsersPermissionMapper>(entity =>
            {
                entity.ToTable("users_permission_mapper");

                entity.HasIndex(e => e.PermissionId, "IX_users_permission_mapper_permission_id");

                entity.HasIndex(e => e.RoleId, "IX_users_permission_mapper_role_id");

                entity.Property(e => e.UsersPermissionMapperId)
                    .HasColumnName("users_permission_mapper_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UsersPermissionMappers)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_permission_mapper_roles_permission_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsersPermissionMappers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_permission_mapper_roles_role_id");
            });

            modelBuilder.Entity<UsersRolesMapper>(entity =>
            {
                entity.ToTable("users_roles_mapper");

                entity.HasIndex(e => e.RoleId, "IX_users_roles_mapper_role_id");

                entity.HasIndex(e => e.UserId, "IX_users_roles_mapper_user_id");

                entity.Property(e => e.UsersRolesMapperId)
                    .HasColumnName("users_roles_mapper_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsersRolesMappers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_roles_mapper_roles_role_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersRolesMappers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_roles_mapper_users_user_id");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("vehicles");

                entity.Property(e => e.VehicleId)
                    .HasColumnName("vehicle_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Maker)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("maker");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("model");

                entity.Property(e => e.ModelNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("model_number");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("year");
            });

            modelBuilder.Entity<VehicleDeviceMapper>(entity =>
            {
                entity.ToTable("vehicle_device_mapper");

                entity.HasIndex(e => e.VehicleId, "IX_vehicle_device_mapper_permission_id");

                entity.HasIndex(e => e.DeviceId, "IX_vehicle_device_mapper_role_id");

                entity.HasIndex(e => e.VehicleDeviceMapperId, "IX_vehicle_device_mapper_vehicle_device_mapper_vehicle_device_m");

                entity.Property(e => e.VehicleDeviceMapperId)
                    .HasColumnName("vehicle_device_mapper_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.VehicleDeviceMappers)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_device_mapper_devices_device_id");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleDeviceMappers)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_device_mapper_vehicles_vehicle_id");
            });

            modelBuilder.Entity<VehicleLocationMapper>(entity =>
            {
                entity.ToTable("vehicle_location_mapper");

                entity.Property(e => e.VehicleLocationMapperId)
                    .HasColumnName("vehicle_location_mapper_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Details)
                    .HasColumnType("jsonb")
                    .HasColumnName("details");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Long).HasColumnName("long");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.VehicleDeviceMapperId).HasColumnName("vehicle_device_mapper_id");

                entity.HasOne(d => d.VehicleDeviceMapper)
                    .WithMany(p => p.VehicleLocationMappers)
                    .HasForeignKey(d => d.VehicleDeviceMapperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_location_mapper_vehicle_device_mapper_vehicle_device");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
