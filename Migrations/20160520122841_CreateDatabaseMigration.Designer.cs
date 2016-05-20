using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BoggleBoardMaker;

namespace BoggleBoardMaker.Migrations
{
    [DbContext(typeof(BoggleBoardContext))]
    [Migration("20160520122841_CreateDatabaseMigration")]
    partial class CreateDatabaseMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("BoggleBoardMaker.Board", b =>
                {
                    b.Property<string>("BoardId");

                    b.Property<string>("BoardStr");

                    b.HasKey("BoardId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("BoggleBoardMaker.BoardWord", b =>
                {
                    b.Property<string>("BoardWordId");

                    b.Property<string>("BoardId");

                    b.Property<string>("Word");

                    b.HasKey("BoardWordId");

                    b.HasIndex("BoardId");

                    b.ToTable("BoardWords");
                });

            modelBuilder.Entity("BoggleBoardMaker.BoardWord", b =>
                {
                    b.HasOne("BoggleBoardMaker.Board")
                        .WithMany()
                        .HasForeignKey("BoardId");
                });
        }
    }
}
