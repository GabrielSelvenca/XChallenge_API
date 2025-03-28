﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using XChallenge_API.Models;

namespace XChallenge_API.Contexts;

public partial class MainContext : DbContext
{
    public MainContext()
    {
    }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acesso> Acessos { get; set; }

    public virtual DbSet<DataNacional> DataNacionals { get; set; }

    public virtual DbSet<LogAcesso> LogAcessos { get; set; }

    public virtual DbSet<Modalidade> Modalidades { get; set; }

    public virtual DbSet<Noticium> Noticia { get; set; }

    public virtual DbSet<Parceiro> Parceiros { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillModalidade> SkillModalidades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=XChallenge;User Id=sa;Password=Senai@134;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acesso>(entity =>
        {
            entity.HasKey(e => e.IdAcesso);

            entity.ToTable("Acesso");

            entity.Property(e => e.IdAcesso).HasColumnName("idAcesso");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.SenhaAcesso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("senhaAcesso");
        });

        modelBuilder.Entity<DataNacional>(entity =>
        {
            entity.HasKey(e => e.IdNacional);

            entity.ToTable("DataNacional");

            entity.Property(e => e.IdNacional).HasColumnName("idNacional");
            entity.Property(e => e.AnoMundial).HasColumnName("anoMundial");
            entity.Property(e => e.IdModalidade)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("idModalidade");
            entity.Property(e => e.IniCompeticao)
                .HasColumnType("smalldatetime")
                .HasColumnName("iniCompeticao");
            entity.Property(e => e.LocalCompeticao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("localCompeticao");
            entity.Property(e => e.TerminoCompeticao)
                .HasColumnType("smalldatetime")
                .HasColumnName("terminoCompeticao");

            entity.HasOne(d => d.IdModalidadeNavigation).WithMany(p => p.DataNacionals)
                .HasForeignKey(d => d.IdModalidade)
                .HasConstraintName("FK_DataNacional_Modalidade");
        });

        modelBuilder.Entity<LogAcesso>(entity =>
        {
            entity.HasKey(e => e.Idlog);

            entity.ToTable("LogAcesso");

            entity.Property(e => e.Idlog).HasColumnName("idlog");
            entity.Property(e => e.DataHoraAcesso)
                .HasColumnType("datetime")
                .HasColumnName("dataHoraAcesso");
            entity.Property(e => e.DataHoraSaida).HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.LogAcessos)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("FK_LogAcesso_Acesso");
        });

        modelBuilder.Entity<Modalidade>(entity =>
        {
            entity.HasKey(e => e.IdModalidade);

            entity.ToTable("Modalidade");

            entity.Property(e => e.IdModalidade)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("idModalidade");
            entity.Property(e => e.DescModalidade)
                .HasColumnType("ntext")
                .HasColumnName("descModalidade");
            entity.Property(e => e.NomeModalidade)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nomeModalidade");
        });

        modelBuilder.Entity<Noticium>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Noticia)
                .HasColumnType("text")
                .HasColumnName("noticia");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Titulo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Parceiro>(entity =>
        {
            entity.HasKey(e => e.IdParceiro);

            entity.ToTable("Parceiro");

            entity.Property(e => e.IdParceiro).HasColumnName("idParceiro");
            entity.Property(e => e.Descricao).HasColumnType("text");
            entity.Property(e => e.Logo)
                .HasColumnType("image")
                .HasColumnName("logo");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.IdSkill);

            entity.Property(e => e.IdSkill).HasColumnName("idSkill");
            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<SkillModalidade>(entity =>
        {
            entity.ToTable("SkillModalidade");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idmodalidade)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("idmodalidade");
            entity.Property(e => e.Idskill).HasColumnName("idskill");

            entity.HasOne(d => d.IdmodalidadeNavigation).WithMany(p => p.SkillModalidades)
                .HasForeignKey(d => d.Idmodalidade)
                .HasConstraintName("FK_SkillModalidade_Modalidade");

            entity.HasOne(d => d.IdskillNavigation).WithMany(p => p.SkillModalidades)
                .HasForeignKey(d => d.Idskill)
                .HasConstraintName("FK_SkillModalidade_Skills");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
