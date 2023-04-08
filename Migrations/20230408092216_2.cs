using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankManagement.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bankAccTypeTbls",
                columns: table => new
                {
                    BankAccTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankAccTypeTbls", x => x.BankAccTypeID);
                });

            migrationBuilder.CreateTable(
                name: "statusTbls",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statusTbls", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "transTbls",
                columns: table => new
                {
                    TransID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransTypeID = table.Column<int>(type: "int", nullable: false),
                    BankAccID = table.Column<int>(type: "int", nullable: false),
                    TransChargeAmount = table.Column<float>(type: "real", nullable: false),
                    TransAmount = table.Column<float>(type: "real", nullable: false),
                    TransTotalAmount = table.Column<float>(type: "real", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transTbls", x => x.TransID);
                });

            migrationBuilder.CreateTable(
                name: "transTypeTbls",
                columns: table => new
                {
                    TransTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransType = table.Column<int>(type: "int", nullable: false),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transTypeTbls", x => x.TransTypeID);
                });

            migrationBuilder.CreateTable(
                name: "usersBankAccTbls",
                columns: table => new
                {
                    BankAccID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BankAccTypeID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    BankAccBalance = table.Column<float>(type: "real", nullable: false),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersBankAccTbls", x => x.BankAccID);
                });

            migrationBuilder.CreateTable(
                name: "userTypeTbls",
                columns: table => new
                {
                    UserTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTypeTbls", x => x.UserTypeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankAccTypeTbls");

            migrationBuilder.DropTable(
                name: "statusTbls");

            migrationBuilder.DropTable(
                name: "transTbls");

            migrationBuilder.DropTable(
                name: "transTypeTbls");

            migrationBuilder.DropTable(
                name: "usersBankAccTbls");

            migrationBuilder.DropTable(
                name: "userTypeTbls");
        }
    }
}
