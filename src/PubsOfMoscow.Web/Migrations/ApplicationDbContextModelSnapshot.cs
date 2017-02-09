using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PubsOfMoscow.Web.Data;

namespace PubsOfMoscow.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("EstimateStartTime");

                    b.Property<bool>("IsChosen");

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
