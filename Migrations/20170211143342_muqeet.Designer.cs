using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CashFlow;

namespace CashFlow.Migrations
{
    [DbContext(typeof(FlowContext))]
    [Migration("20170211143342_muqeet")]
    partial class muqeet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CashFlow.Models.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("EmailVerified");

                    b.Property<string>("PushId");

                    b.Property<string>("TokenActivate");

                    b.Property<string>("TokenForgetpw");

                    b.Property<string>("email");

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.Property<string>("phone");

                    b.Property<int>("status");

                    b.Property<string>("token");

                    b.HasKey("id");

                    b.ToTable("users");
                });
        }
    }
}
