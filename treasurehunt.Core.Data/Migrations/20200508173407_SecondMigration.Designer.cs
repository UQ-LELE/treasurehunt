﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using treasurehunt.Core.Data;

namespace treasurehunt.Core.Data.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20200508173407_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Personnage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int")
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.ToTable("Personnages");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Personnage");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Quetes.Choix", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EvenementId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EvenementId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choix");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Quetes.Evenement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EstInitial")
                        .HasColumnType("bit");

                    b.Property<int?>("LaQuestionID")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LaQuestionID");

                    b.ToTable("Evenement");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Quetes.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LaQuestion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Quetes.Quete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Quete");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Elfe", b =>
                {
                    b.HasBaseType("treasurehunt.Core.Data.Models.Personnage");

                    b.HasDiscriminator().HasValue("Elfe");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Humain", b =>
                {
                    b.HasBaseType("treasurehunt.Core.Data.Models.Personnage");

                    b.HasDiscriminator().HasValue("Humain");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Nain", b =>
                {
                    b.HasBaseType("treasurehunt.Core.Data.Models.Personnage");

                    b.HasDiscriminator().HasValue("Nain");
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Quetes.Choix", b =>
                {
                    b.HasOne("treasurehunt.Core.Data.Models.Quetes.Evenement", null)
                        .WithMany("LesChoix")
                        .HasForeignKey("EvenementId");

                    b.HasOne("treasurehunt.Core.Data.Models.Quetes.Question", null)
                        .WithMany("LesChoix")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("treasurehunt.Core.Data.Models.Quetes.Evenement", b =>
                {
                    b.HasOne("treasurehunt.Core.Data.Models.Quetes.Question", "LaQuestion")
                        .WithMany()
                        .HasForeignKey("LaQuestionID");
                });
#pragma warning restore 612, 618
        }
    }
}
