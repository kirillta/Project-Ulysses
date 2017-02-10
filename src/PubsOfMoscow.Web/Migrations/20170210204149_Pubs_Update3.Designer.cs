using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PubsOfMoscow.Web.Data;

namespace PubsOfMoscow.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170210204149_Pubs_Update3")]
    partial class Pubs_Update3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PubsOfMoscow.Web.Models.Congratulation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsApproved");

                    b.HasKey("Id");

                    b.ToTable("Congratulations");
                });

            modelBuilder.Entity("PubsOfMoscow.Web.Models.Pub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("EstimateStartTime");

                    b.Property<string>("IconUrl");

                    b.Property<bool>("IsChosen");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(13,8)");

                    b.Property<string>("LogoUrl");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(13,8)");

                    b.Property<int>("RoundId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("Pubs");
                });

            modelBuilder.Entity("PubsOfMoscow.Web.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDone");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("PubsOfMoscow.Web.Models.Pub", b =>
                {
                    b.HasOne("PubsOfMoscow.Web.Models.Round", "Round")
                        .WithMany("Pubs")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
