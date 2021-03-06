﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using QuickstartIdentityServer.Data;

namespace QuickstartIdentityServer.Data.Migrations.IdentityServer.UserDb
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20170202092535_AddIndices")]
    partial class AddIndices
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuickstartIdentityServer.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("SubjectId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Website")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("SubjectId")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QuickstartIdentityServer.Data.UserExternalProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProviderName");

                    b.Property<string>("ProviderSubjectId")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProviderName");

                    b.HasIndex("ProviderSubjectId");

                    b.HasIndex("UserId");

                    b.HasIndex("ProviderName", "ProviderSubjectId")
                        .IsUnique();

                    b.ToTable("UserExternalProviders");
                });

            modelBuilder.Entity("QuickstartIdentityServer.Data.UserExternalProvider", b =>
                {
                    b.HasOne("QuickstartIdentityServer.Data.User", "User")
                        .WithMany("UserExternalProviders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
