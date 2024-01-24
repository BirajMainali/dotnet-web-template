﻿// <auto-generated />
using System;
using App.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240124143821_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("App.User.Entity.AppUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long?>("ParentUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_user_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<char>("RecStatus")
                        .HasColumnType("character(1)")
                        .HasColumnName("rec_status");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id");

                    b.Property<Guid?>("TenantId1")
                        .HasColumnType("uuid")
                        .HasColumnName("tenant_id1");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_app_user");

                    b.HasIndex("ParentUserId")
                        .HasDatabaseName("ix_app_user_parent_user_id");

                    b.HasIndex("TenantId1")
                        .HasDatabaseName("ix_app_user_tenant_id1");

                    b.ToTable("app_user", (string)null);
                });

            modelBuilder.Entity("App.User.Entity.ApplicationTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("DatabaseName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("database_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<char>("RecStatus")
                        .HasColumnType("character(1)")
                        .HasColumnName("rec_status");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id")
                        .HasName("pk_application_tenant");

                    b.ToTable("application_tenant", (string)null);
                });

            modelBuilder.Entity("App.User.Entity.AppUser", b =>
                {
                    b.HasOne("App.User.Entity.AppUser", "ParentUser")
                        .WithMany()
                        .HasForeignKey("ParentUserId")
                        .HasConstraintName("fk_app_user_app_user_parent_user_id");

                    b.HasOne("App.User.Entity.ApplicationTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId1")
                        .HasConstraintName("fk_app_user_application_tenant_tenant_id1");

                    b.Navigation("ParentUser");

                    b.Navigation("Tenant");
                });
#pragma warning restore 612, 618
        }
    }
}
