﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eDrsDB.Data;

namespace eDrsAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210310060508_userId")]
    partial class userId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDrsDB.Models.ApplicationForm", b =>
                {
                    b.Property<long>("ApplicationFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CertifiedCopy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ChargeDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("DocumentReferenceId")
                        .HasColumnType("bigint");

                    b.Property<string>("ExternalReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FeeInPence")
                        .HasColumnType("int");

                    b.Property<string>("MDRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Variety")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationFormId");

                    b.HasIndex("DocumentReferenceId");

                    b.ToTable("ApplicationForms");
                });

            modelBuilder.Entity("eDrsDB.Models.AttachmentNote", b =>
                {
                    b.Property<long>("AttachmentNotesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalProviderFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationMessageId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationService")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DocumentReferenceId")
                        .HasColumnType("bigint");

                    b.Property<string>("ExternalReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MessageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttachmentNotesId");

                    b.HasIndex("DocumentReferenceId");

                    b.ToTable("AttachmentNotes");
                });

            modelBuilder.Entity("eDrsDB.Models.Document", b =>
                {
                    b.Property<long>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ApplicationFormId")
                        .HasColumnType("bigint");

                    b.Property<string>("Base64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentId");

                    b.HasIndex("ApplicationFormId")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("eDrsDB.Models.DocumentReference", b =>
                {
                    b.Property<long>("DocumentReferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AP1WarningUnderstood")
                        .HasColumnType("bit");

                    b.Property<string>("AdditionalProviderFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationAffects")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DisclosableOveridingInterests")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalAuthority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostcodeOfProperty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RegistrationTypeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<long>("TelephoneNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("TotalFeeInPence")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("DocumentReferenceId");

                    b.HasIndex("RegistrationTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("DocumentReferences");
                });

            modelBuilder.Entity("eDrsDB.Models.ErrorLogs", b =>
                {
                    b.Property<long>("ErrorLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<string>("StackTraceString")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.HasKey("ErrorLogId");

                    b.ToTable("ErrorLogs");
                });

            modelBuilder.Entity("eDrsDB.Models.Party", b =>
                {
                    b.Property<long>("PartyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressForService")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyOrForeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DocumentReferenceId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsApplicant")
                        .HasColumnType("bit");

                    b.Property<string>("PartyType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Roles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartyId");

                    b.HasIndex("DocumentReferenceId");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("eDrsDB.Models.RegistrationType", b =>
                {
                    b.Property<long>("RegistrationTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("TypeCode")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.HasKey("RegistrationTypeId");

                    b.ToTable("RegistrationTypes");

                    b.HasData(
                        new
                        {
                            RegistrationTypeId = 1L,
                            Status = true,
                            TypeCode = "trns_chrge",
                            TypeName = "Transfer and charge",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 894, DateTimeKind.Local).AddTicks(9392),
                            Url = "transfer-and-charge"
                        },
                        new
                        {
                            RegistrationTypeId = 2L,
                            Status = true,
                            TypeCode = "rem_gage",
                            TypeName = "Remortgage",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 895, DateTimeKind.Local).AddTicks(8777),
                            Url = "remortgage"
                        },
                        new
                        {
                            RegistrationTypeId = 3L,
                            Status = true,
                            TypeCode = "trns_eqty",
                            TypeName = "Transfer of equity",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 895, DateTimeKind.Local).AddTicks(8802),
                            Url = "transfer-equity"
                        },
                        new
                        {
                            RegistrationTypeId = 4L,
                            Status = true,
                            TypeCode = "rem_frm",
                            TypeName = "Restriction, hostile takeover",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 895, DateTimeKind.Local).AddTicks(8806),
                            Url = "removal-form"
                        },
                        new
                        {
                            RegistrationTypeId = 5L,
                            Status = true,
                            TypeCode = "chngName",
                            TypeName = "Change of name",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 895, DateTimeKind.Local).AddTicks(8808),
                            Url = "change-name"
                        },
                        new
                        {
                            RegistrationTypeId = 6L,
                            Status = true,
                            TypeCode = "dispositionary",
                            TypeName = "Dispositionary first lease",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 895, DateTimeKind.Local).AddTicks(8810),
                            Url = "dispositionary"
                        },
                        new
                        {
                            RegistrationTypeId = 7L,
                            Status = true,
                            TypeCode = "transfer",
                            TypeName = "Transfer of part",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 895, DateTimeKind.Local).AddTicks(8812),
                            Url = "transfer"
                        },
                        new
                        {
                            RegistrationTypeId = 8L,
                            Status = true,
                            TypeCode = "lease_ext",
                            TypeName = "Lease extension",
                            UpdatedDate = new DateTime(2021, 3, 10, 11, 35, 7, 895, DateTimeKind.Local).AddTicks(8814),
                            Url = "lease-extension"
                        });
                });

            modelBuilder.Entity("eDrsDB.Models.Representation", b =>
                {
                    b.Property<long>("RepresentationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CareOfName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CareOfReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DocumentReferenceId")
                        .HasColumnType("bigint");

                    b.Property<string>("DxExchange")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DxNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RepresentativeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RepresentationId");

                    b.HasIndex("DocumentReferenceId");

                    b.ToTable("Representations");
                });

            modelBuilder.Entity("eDrsDB.Models.RequestLog", b =>
                {
                    b.Property<long>("RequestLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DocumentReferenceId")
                        .HasColumnType("bigint");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestLogId");

                    b.HasIndex("DocumentReferenceId");

                    b.ToTable("RequestLogs");
                });

            modelBuilder.Entity("eDrsDB.Models.SupportingDocuments", b =>
                {
                    b.Property<long>("SupportingDocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CertifiedCopy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DocumentReferenceId")
                        .HasColumnType("bigint");

                    b.HasKey("SupportingDocumentId");

                    b.HasIndex("DocumentReferenceId");

                    b.ToTable("SupportingDocuments");
                });

            modelBuilder.Entity("eDrsDB.Models.TitleNumber", b =>
                {
                    b.Property<long>("TitleNumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<long>("DocumentReferenceId")
                        .HasColumnType("bigint");

                    b.Property<string>("LesseeTitleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("TitleNumberCode")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TitleNumberId");

                    b.HasIndex("DocumentReferenceId");

                    b.ToTable("TitleNumbers");
                });

            modelBuilder.Entity("eDrsDB.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(350)")
                        .HasMaxLength(350);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Designation = "admin",
                            Email = "dushyanthaccura@gmail.com",
                            Firstname = "Admin",
                            PasswordHash = new byte[] { 90, 88, 214, 71, 202, 16, 67, 43, 240, 18, 89, 241, 201, 146, 185, 161, 65, 3, 167, 15, 32, 17, 164, 131, 218, 71, 181, 233, 90, 41, 247, 54, 119, 33, 235, 90, 61, 47, 123, 31, 193, 3, 27, 6, 179, 65, 26, 157, 109, 192, 235, 73, 74, 42, 183, 163, 10, 158, 26, 142, 192, 217, 220, 190 },
                            PasswordSalt = new byte[] { 4, 61, 22, 92, 58, 63, 129, 62, 125, 164, 112, 24, 251, 230, 212, 150, 78, 130, 82, 114, 4, 54, 26, 46, 17, 163, 48, 86, 100, 99, 86, 205, 131, 32, 24, 105, 217, 181, 128, 145, 100, 8, 65, 25, 37, 99, 172, 116, 158, 82, 116, 85, 159, 233, 218, 186, 248, 199, 163, 209, 100, 14, 49, 76, 166, 118, 177, 29, 236, 82, 72, 107, 105, 14, 242, 200, 191, 57, 59, 201, 216, 109, 1, 179, 238, 4, 249, 15, 77, 34, 218, 138, 183, 216, 184, 244, 143, 5, 40, 10, 93, 213, 106, 44, 11, 159, 22, 99, 99, 67, 147, 99, 6, 150, 193, 119, 93, 158, 143, 136, 211, 57, 21, 176, 235, 128, 230, 232 },
                            Status = true,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("eDrsDB.Models.ApplicationForm", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("Applications")
                        .HasForeignKey("DocumentReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.AttachmentNote", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("AttachmentNotes")
                        .HasForeignKey("DocumentReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.Document", b =>
                {
                    b.HasOne("eDrsDB.Models.ApplicationForm", "ApplicationForm")
                        .WithOne("Document")
                        .HasForeignKey("eDrsDB.Models.Document", "ApplicationFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.DocumentReference", b =>
                {
                    b.HasOne("eDrsDB.Models.RegistrationType", "RegistrationType")
                        .WithMany()
                        .HasForeignKey("RegistrationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eDrsDB.Models.User", "User")
                        .WithMany("DocumentReferences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.Party", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("Parties")
                        .HasForeignKey("DocumentReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.Representation", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("Representations")
                        .HasForeignKey("DocumentReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.RequestLog", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("RequestLogs")
                        .HasForeignKey("DocumentReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.SupportingDocuments", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("SupportingDocuments")
                        .HasForeignKey("DocumentReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eDrsDB.Models.TitleNumber", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("Titles")
                        .HasForeignKey("DocumentReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}