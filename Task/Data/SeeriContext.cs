
using Microsoft.EntityFrameworkCore;
using Task.Data.Entities;

namespace Task.Data;

public partial class SeeriContext : DbContext
{
    public SeeriContext()
    {
    }

    public SeeriContext(DbContextOptions<SeeriContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TypeStateEntity> TypeStates { get; set; }

    public virtual DbSet<TaskEntity> Tasks { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PRIMARY");

            entity.ToTable("task");

            entity.HasIndex(e => e.State, "type_state_id_idx");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.CreateDate).HasColumnName("createDate");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Titulo)
                .HasMaxLength(45)
                .HasColumnName("titulo");
            entity.Property(e => e.UpdateDate).HasColumnName("updateDate");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.State)
                .HasConstraintName("type_state_id");
        });

        modelBuilder.Entity<TypeStateEntity>(entity =>
        {
            entity.HasKey(e => e.TypeStateId).HasName("PRIMARY");

            entity.ToTable("type_state");

            entity.Property(e => e.TypeStateId).HasColumnName("type_state_id");
            entity.Property(e => e.CreateDate).HasColumnName("create_date");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
