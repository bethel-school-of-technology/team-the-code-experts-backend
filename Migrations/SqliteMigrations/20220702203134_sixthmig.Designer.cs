﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Helpers;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    [DbContext(typeof(SqliteDataContext))]
    [Migration("20220702203134_sixthmig")]
    partial class sixthmig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Broadcast_JWT.Models.Flag", b =>
                {
                    b.Property<int>("FlagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MessageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FlagId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("MessageId");

                    b.ToTable("Flag");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.FollowingUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FollowingUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("FollowingUser");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("MessageBody")
                        .HasColumnType("TEXT");

                    b.Property<string>("MessageTitle")
                        .HasColumnType("TEXT");

                    b.HasKey("MessageId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DownVote")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UpVote")
                        .HasColumnType("INTEGER");

                    b.HasKey("VoteId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("MessageId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("WebApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.Flag", b =>
                {
                    b.HasOne("WebApi.Entities.User", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("Broadcast_JWT.Models.Message", "Message")
                        .WithMany("Flags")
                        .HasForeignKey("MessageId");

                    b.Navigation("AppUser");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.FollowingUser", b =>
                {
                    b.HasOne("WebApi.Entities.User", "AppUser")
                        .WithMany("FollowingUsers")
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.Message", b =>
                {
                    b.HasOne("WebApi.Entities.User", "AppUser")
                        .WithMany("Messages")
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.Vote", b =>
                {
                    b.HasOne("WebApi.Entities.User", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("Broadcast_JWT.Models.Message", "Message")
                        .WithMany("Votes")
                        .HasForeignKey("MessageId");

                    b.Navigation("AppUser");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Broadcast_JWT.Models.Message", b =>
                {
                    b.Navigation("Flags");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("WebApi.Entities.User", b =>
                {
                    b.Navigation("FollowingUsers");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
