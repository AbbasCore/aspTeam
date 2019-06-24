﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamTaskManager.Data;

namespace TeamTaskManager.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190530131330_DbCreate_f")]
    partial class DbCreate_f
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamTaskManager.Models.MProject", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("description");

                    b.Property<DateTime>("endDate");

                    b.Property<DateTime>("startDate");

                    b.Property<int>("teamIdFK");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("teamIdFK")
                        .IsUnique();

                    b.ToTable("project");
                });

            modelBuilder.Entity("TeamTaskManager.Models.MTeam", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("team");
                });

            modelBuilder.Entity("TeamTaskManager.Models.MUser", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email");

                    b.Property<string>("name");

                    b.Property<byte[]>("passwordHash");

                    b.Property<byte[]>("passwordSalt");

                    b.Property<string>("phoneNumber");

                    b.Property<int>("teamIdFK");

                    b.Property<string>("userName");

                    b.HasKey("id");

                    b.HasIndex("teamIdFK");

                    b.ToTable("user");
                });

            modelBuilder.Entity("TeamTaskManager.Models.MProject", b =>
                {
                    b.HasOne("TeamTaskManager.Models.MTeam", "Team")
                        .WithOne("project")
                        .HasForeignKey("TeamTaskManager.Models.MProject", "teamIdFK")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamTaskManager.Models.MUser", b =>
                {
                    b.HasOne("TeamTaskManager.Models.MTeam", "Team")
                        .WithMany("user")
                        .HasForeignKey("teamIdFK")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
