using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TeamManager.Models;

namespace TeamManager.Data;

public partial class DbAb0bdeTalentseedsContext : DbContext
{
    public DbAb0bdeTalentseedsContext()
    {
    }

    public DbAb0bdeTalentseedsContext(DbContextOptions<DbAb0bdeTalentseedsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TfaCategory> TfaCategories { get; set; }

    public virtual DbSet<TfaRol> TfaRols { get; set; }

    public virtual DbSet<TfaTeam> TfaTeams { get; set; }

    public virtual DbSet<TfaTeamstatus> TfaTeamstatuses { get; set; }

    public virtual DbSet<TfaUser> TfaUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL1001.site4now.net;Initial Catalog=db_ab0bde_talentseeds;User Id=db_ab0bde_talentseeds_admin;Password=1qazxsw23edc");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AI");

        modelBuilder.Entity<TfaCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__TFA_CATE__23CAF1F8D2B0FF74");

            entity.ToTable("TFA_CATEGORIES");

            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.CategoryDeadLine).HasColumnName("categoryDeadLine");
            entity.Property(e => e.CategoryDescription)
                .HasMaxLength(100)
                .HasColumnName("categoryDescription");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("categoryName");
            entity.Property(e => e.CategoryPoints).HasColumnName("categoryPoints");
        });

        modelBuilder.Entity<TfaRol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__TFA_ROL__5402365444BEE72A");

            entity.ToTable("TFA_ROL");

            entity.Property(e => e.RolId).HasColumnName("rolID");
            entity.Property(e => e.RolDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("rolDescription");
            entity.Property(e => e.RolName)
                .HasMaxLength(50)
                .HasColumnName("rolName");
        });

        modelBuilder.Entity<TfaTeam>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__TFA_TEAM__5ED7534A6BB50E22");

            entity.ToTable("TFA_TEAMS");

            entity.Property(e => e.TeamId).HasColumnName("teamID");
            entity.Property(e => e.CategoriesId).HasColumnName("categoriesID");
            entity.Property(e => e.TeamDescription)
                .HasMaxLength(50)
                .HasColumnName("teamDescription");
            entity.Property(e => e.TeamLeadId).HasColumnName("teamLeadID");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .HasColumnName("teamName");
            entity.Property(e => e.TeamStatusId).HasColumnName("teamStatusID");

            entity.HasOne(d => d.Categories).WithMany(p => p.TfaTeams)
                .HasForeignKey(d => d.CategoriesId)
                .HasConstraintName("FK_TFA_TEAMS_TFA_CATEGORIES");

            entity.HasOne(d => d.TeamLead).WithMany(p => p.TfaTeams)
                .HasForeignKey(d => d.TeamLeadId)
                .HasConstraintName("FK_teamLead");

            entity.HasOne(d => d.TeamStatus).WithMany(p => p.TfaTeams)
                .HasForeignKey(d => d.TeamStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teamStatusID");
        });

        modelBuilder.Entity<TfaTeamstatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__TFA_TEAM__36257A388E96798A");

            entity.ToTable("TFA_TEAMSTATUS");

            entity.Property(e => e.StatusId).HasColumnName("statusID");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("statusName");
        });

        modelBuilder.Entity<TfaUser>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PK__TFA_USER__788FDD2552307940");

            entity.ToTable("TFA_USERS");

            entity.Property(e => e.UsersId).HasColumnName("usersID");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(999)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.RolId).HasColumnName("rolID");
            entity.Property(e => e.RolIdaddional).HasColumnName("rolIDAddional");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .HasColumnName("userEmail");
            entity.Property(e => e.UserLastName)
                .HasMaxLength(50)
                .HasColumnName("userLastName");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
            entity.Property(e => e.UserPoints).HasColumnName("userPoints");

            entity.HasOne(d => d.Rol).WithMany(p => p.TfaUserRols)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK_userRol");

            entity.HasOne(d => d.RolIdaddionalNavigation).WithMany(p => p.TfaUserRolIdaddionalNavigations)
                .HasForeignKey(d => d.RolIdaddional)
                .HasConstraintName("FK_userRolAddional");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
