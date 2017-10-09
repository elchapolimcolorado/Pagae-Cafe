using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pagae.Data;

namespace Pagae.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("201710050005_AddingLastUpdateDateToApplicationUserEntity")]
    partial class AddingLastUpdateDateToApplicationUserEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Pagae.Models.ApplicationUser", b =>
            {
                b.Property<DateTime?>("LastUpdateDate");
                b.ToTable("AspNetUsers");
            });
        }
    }
}
