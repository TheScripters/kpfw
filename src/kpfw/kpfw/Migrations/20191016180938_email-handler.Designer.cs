﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kpfw.DataModels;

namespace kpfw.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191016180938_email-handler")]
    partial class emailhandler
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("kpfw.DataModels.BouncedEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BounceDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("BouncedEmail");
                });

            modelBuilder.Entity("kpfw.DataModels.ComplainedEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ComplaintDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ComplainedEmail");
                });

            modelBuilder.Entity("kpfw.DataModels.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AirDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Director")
                        .HasMaxLength(100);

                    b.Property<string>("ExecutiveProducer")
                        .HasMaxLength(200);

                    b.Property<string>("GuestStars")
                        .HasMaxLength(300);

                    b.Property<int>("Number");

                    b.Property<string>("Producer")
                        .HasMaxLength(100);

                    b.Property<string>("ProductionNumber")
                        .HasMaxLength(15);

                    b.Property<string>("Recap")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Stars")
                        .HasMaxLength(300);

                    b.Property<string>("Studio")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("Transcript")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("UrlLabel")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("Writer")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("Episode");
                });

            modelBuilder.Entity("kpfw.DataModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("kpfw.DataModels.Timeline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Timeline");
                });

            modelBuilder.Entity("kpfw.DataModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName");

                    b.Property<Guid?>("EmailConfirmation");

                    b.Property<string>("IPAddress");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("JoinDate");

                    b.Property<bool>("ShowEmail");

                    b.Property<string>("TimeZone")
                        .IsRequired();

                    b.Property<string>("TwoFactor");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("kpfw.DataModels.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("kpfw.DataModels.UserRole", b =>
                {
                    b.HasOne("kpfw.DataModels.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("kpfw.DataModels.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
