﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PrintCenter.Data;

namespace PrintCenter.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200408185618_2-RefactModels")]
    partial class _2RefactModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PrintCenter.Data.Models.CompositeSerialProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PackageSize")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CompositeSerialProductions");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.CompositeSerialProductionSerialProduction", b =>
                {
                    b.Property<int>("CompositeSerialProductionId")
                        .HasColumnType("integer");

                    b.Property<int>("SerialProductionId")
                        .HasColumnType("integer");

                    b.HasKey("CompositeSerialProductionId", "SerialProductionId");

                    b.HasIndex("SerialProductionId");

                    b.ToTable("CompositeSerialProductionSerialProductions");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Number");

                    b.HasIndex("AuthorId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Count")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("MinimalCount")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("NormalCount")
                        .HasColumnType("double precision");

                    b.Property<double>("Parameter")
                        .HasColumnType("double precision");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Unit")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Parameter")
                        .IsUnique();

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialConsumption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.Property<int?>("SerialProductionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("SerialProductionId");

                    b.ToTable("MaterialConsumptions");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialConsumptionSerialProduction", b =>
                {
                    b.Property<int>("MaterialConsumptionId")
                        .HasColumnType("integer");

                    b.Property<int>("SerialProductionId")
                        .HasColumnType("integer");

                    b.HasKey("MaterialConsumptionId", "SerialProductionId");

                    b.HasIndex("SerialProductionId");

                    b.ToTable("MaterialConsumptionSerialProductions");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialConsumptionStream", b =>
                {
                    b.Property<int>("MaterialConsumptionId")
                        .HasColumnType("integer");

                    b.Property<int>("StreamId")
                        .HasColumnType("integer");

                    b.HasKey("MaterialConsumptionId", "StreamId");

                    b.HasIndex("StreamId");

                    b.ToTable("MaterialConsumptionStreams");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Count")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<int?>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("UserId");

                    b.ToTable("MaterialMovements");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Number");

                    b.HasIndex("AuthorId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Number");

                    b.HasIndex("AuthorId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.SerialProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PackageSize")
                        .HasColumnType("integer");

                    b.Property<double>("SizeX")
                        .HasColumnType("double precision");

                    b.Property<double>("SizeY")
                        .HasColumnType("double precision");

                    b.Property<int?>("TechnologyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TechnologyId");

                    b.ToTable("SerialProductions");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Stream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PackageSize")
                        .HasColumnType("integer");

                    b.Property<int>("PackagesCount")
                        .HasColumnType("integer");

                    b.Property<int?>("PlanId")
                        .HasColumnType("integer");

                    b.Property<int?>("RequestId")
                        .HasColumnType("integer");

                    b.Property<double>("SizeX")
                        .HasColumnType("double precision");

                    b.Property<double>("SizeY")
                        .HasColumnType("double precision");

                    b.Property<int?>("TechnologyId")
                        .HasColumnType("integer");

                    b.Property<string>("YaltaNumber")
                        .HasColumnType("text");

                    b.Property<string>("YaltaPosition")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PlanId");

                    b.HasIndex("RequestId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("Streams");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.UserTechnology", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("TechnologyId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("UserTechnologies");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.CompositeSerialProductionSerialProduction", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.CompositeSerialProduction", "CompositeSerialProduction")
                        .WithMany("CompositeSerialProductionSerialProductions")
                        .HasForeignKey("CompositeSerialProductionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintCenter.Data.Models.SerialProduction", "SerialProduction")
                        .WithMany("CompositeSerialProductionSerialProductions")
                        .HasForeignKey("SerialProductionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Invoice", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.User", "Author")
                        .WithMany("Invoices")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialConsumption", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.Material", "Material")
                        .WithMany("MaterialConsumptions")
                        .HasForeignKey("MaterialId");

                    b.HasOne("PrintCenter.Data.Models.SerialProduction", null)
                        .WithMany("MaterialConsumptions")
                        .HasForeignKey("SerialProductionId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialConsumptionSerialProduction", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.MaterialConsumption", "MaterialConsumption")
                        .WithMany("MaterialConsumptionSerialProductions")
                        .HasForeignKey("MaterialConsumptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintCenter.Data.Models.SerialProduction", "SerialProduction")
                        .WithMany("MaterialConsumptionSerialProductions")
                        .HasForeignKey("SerialProductionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialConsumptionStream", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.MaterialConsumption", "MaterialConsumption")
                        .WithMany("MaterialConsumptionStreams")
                        .HasForeignKey("MaterialConsumptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintCenter.Data.Models.Stream", "Stream")
                        .WithMany("MaterialConsumptionStreams")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrintCenter.Data.Models.MaterialMovement", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId");

                    b.HasOne("PrintCenter.Data.Models.Material", "Material")
                        .WithMany("MaterialMovements")
                        .HasForeignKey("MaterialId");

                    b.HasOne("PrintCenter.Data.Models.User", "User")
                        .WithMany("MaterialMovements")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Notification", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Plan", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.User", "Author")
                        .WithMany("Plans")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Request", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.User", "Author")
                        .WithMany("Requests")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.SerialProduction", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.Stream", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.Customer", "Customer")
                        .WithMany("Streams")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PrintCenter.Data.Models.Invoice", "Invoice")
                        .WithMany("Streams")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("PrintCenter.Data.Models.Plan", "Plan")
                        .WithMany("Streams")
                        .HasForeignKey("PlanId");

                    b.HasOne("PrintCenter.Data.Models.Request", "Request")
                        .WithMany("Streams")
                        .HasForeignKey("RequestId");

                    b.HasOne("PrintCenter.Data.Models.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId");
                });

            modelBuilder.Entity("PrintCenter.Data.Models.UserTechnology", b =>
                {
                    b.HasOne("PrintCenter.Data.Models.Technology", "Technology")
                        .WithMany("UserTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintCenter.Data.Models.User", "User")
                        .WithMany("UserTechnologies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
