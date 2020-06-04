﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransDB;

namespace TransDB.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200601074303_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TransDB.Models.Answer", b =>
                {
                    b.Property<int>("AnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Acreatetime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Isadopted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("AnswerID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("UserID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TransDB.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Qcreatetime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Reward")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("QuestionID");

                    b.HasIndex("UserID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TransDB.Models.Token", b =>
                {
                    b.Property<string>("TokenID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("TokenID");

                    b.HasIndex("UserID");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("TransDB.Models.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Wealth")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TransDB.Models.Answer", b =>
                {
                    b.HasOne("TransDB.Models.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransDB.Models.User", null)
                        .WithMany("Answers")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("TransDB.Models.Question", b =>
                {
                    b.HasOne("TransDB.Models.User", null)
                        .WithMany("Questions")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("TransDB.Models.Token", b =>
                {
                    b.HasOne("TransDB.Models.User", null)
                        .WithMany("Tokens")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
