﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eDrsDB.Data;

namespace eDrsDB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("ApplicationDate")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalFeeInPence")
                        .HasColumnType("int");

                    b.Property<long?>("UserId")
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
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 313, DateTimeKind.Local).AddTicks(9895),
                            Url = "transfer-and-charge"
                        },
                        new
                        {
                            RegistrationTypeId = 2L,
                            Status = true,
                            TypeCode = "lease_ext",
                            TypeName = "Remortgage",
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 314, DateTimeKind.Local).AddTicks(8437),
                            Url = "lease-extension"
                        },
                        new
                        {
                            RegistrationTypeId = 3L,
                            Status = true,
                            TypeCode = "new_lease",
                            TypeName = "Transfer of equity",
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 314, DateTimeKind.Local).AddTicks(8461),
                            Url = "new-lease"
                        },
                        new
                        {
                            RegistrationTypeId = 4L,
                            Status = true,
                            TypeCode = "rem_frm",
                            TypeName = "Restriction, hostile takeover",
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 314, DateTimeKind.Local).AddTicks(8463),
                            Url = "removal-form"
                        },
                        new
                        {
                            RegistrationTypeId = 5L,
                            Status = true,
                            TypeCode = "transfer",
                            TypeName = "Change of name",
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 314, DateTimeKind.Local).AddTicks(8465),
                            Url = "transfer"
                        },
                        new
                        {
                            RegistrationTypeId = 6L,
                            Status = true,
                            TypeCode = "transfer",
                            TypeName = "Dispositionary first lease",
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 314, DateTimeKind.Local).AddTicks(8466),
                            Url = "transfer"
                        },
                        new
                        {
                            RegistrationTypeId = 7L,
                            Status = true,
                            TypeCode = "transfer",
                            TypeName = "Transfer of part",
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 314, DateTimeKind.Local).AddTicks(8468),
                            Url = "transfer"
                        },
                        new
                        {
                            RegistrationTypeId = 8L,
                            Status = true,
                            TypeCode = "lease_ext",
                            TypeName = "Lease extension",
                            UpdatedDate = new DateTime(2021, 2, 22, 8, 43, 40, 314, DateTimeKind.Local).AddTicks(8469),
                            Url = "lease-extension"
                        });
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

                    b.Property<int>("TypeCode")
                        .HasColumnType("int");

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
                            PasswordHash = new byte[] { 153, 241, 43, 154, 217, 140, 35, 7, 93, 114, 112, 142, 226, 95, 168, 116, 204, 106, 111, 42, 44, 10, 130, 236, 147, 129, 196, 249, 79, 224, 170, 166, 56, 253, 174, 30, 146, 44, 67, 119, 213, 23, 77, 75, 142, 236, 40, 215, 240, 17, 2, 41, 16, 35, 152, 232, 157, 99, 215, 140, 173, 92, 144, 133 },
                            PasswordSalt = new byte[] { 179, 4, 29, 155, 192, 214, 189, 32, 56, 123, 170, 97, 251, 103, 158, 196, 128, 204, 103, 250, 100, 81, 70, 187, 48, 123, 198, 69, 127, 213, 174, 124, 184, 234, 153, 142, 214, 19, 245, 189, 17, 63, 81, 25, 86, 255, 233, 135, 129, 125, 63, 242, 79, 165, 197, 237, 15, 138, 47, 110, 126, 25, 155, 150, 18, 171, 193, 220, 205, 155, 103, 100, 73, 199, 184, 106, 142, 225, 100, 62, 87, 39, 56, 211, 121, 98, 36, 245, 2, 89, 167, 163, 82, 148, 1, 79, 181, 13, 83, 73, 30, 161, 103, 249, 143, 247, 128, 133, 144, 220, 87, 27, 207, 65, 23, 253, 99, 56, 70, 154, 79, 249, 151, 222, 127, 240, 118, 211 },
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

                    b.HasOne("eDrsDB.Models.User", null)
                        .WithMany("DocumentReferences")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("eDrsDB.Models.Party", b =>
                {
                    b.HasOne("eDrsDB.Models.DocumentReference", "DocumentReference")
                        .WithMany("Parties")
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
