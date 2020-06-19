using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Matrix.Models
{
    public partial class my_testContext : DbContext
    {
        public my_testContext()
        {
        }

        public my_testContext(DbContextOptions<my_testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Object> Object { get; set; }
        public virtual DbSet<ObjectStatus> ObjectStatus { get; set; }
        public virtual DbSet<ObjectTrack> ObjectTrack { get; set; }
        public virtual DbSet<StatusType> StatusType { get; set; }

        // Unable to generate entity type for table 'public.my_probe'. Please see the warning messages.
        // Unable to generate entity type for table 'public.my_prob'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=my_test;Username=postgres;Password=masterkey");
                optionsBuilder.UseNpgsql(Config.instance.DBConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("module", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.module_id_seq'::regclass)");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.ToTable("object", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.object_id_seq'::regclass)");

                entity.Property(e => e.CrtDate).HasColumnName("crt_date");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Object)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("object_module_id_fkey");
            });

            modelBuilder.Entity<ObjectStatus>(entity =>
            {
                entity.ToTable("object_status", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.object_status_id_seq'::regclass)");

                entity.Property(e => e.CrtDate).HasColumnName("crt_date");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.StatusTypeId).HasColumnName("status_type_id");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectStatus)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("object_status_object_id_fkey");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.ObjectStatus)
                    .HasForeignKey(d => d.StatusTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("object_status_status_type_id_fkey");
            });

            modelBuilder.Entity<ObjectTrack>(entity =>
            {
                entity.ToTable("object_track", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.object_track_id_seq'::regclass)");

                entity.Property(e => e.CrtDate).HasColumnName("crt_date");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectTrack)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("object_track_object_id_fkey");
            });

            modelBuilder.Entity<StatusType>(entity =>
            {
                entity.ToTable("status_type", "main");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('main.status_type_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.HasSequence("module_id_seq");

            modelBuilder.HasSequence("object_id_seq");

            modelBuilder.HasSequence("object_status_id_seq");

            modelBuilder.HasSequence("object_track_id_seq");

            modelBuilder.HasSequence("status_type_id_seq");
        }
    }
}
