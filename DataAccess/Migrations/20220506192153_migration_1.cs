using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations {
	public partial class migration_1 : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Categories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Customers",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					AdressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Customers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "GroupClaims",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					OperationClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_GroupClaims", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Groups",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Groups", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Logs",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
					TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
					Exception = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Logs", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "OperationClaims",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_OperationClaims", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "UserGroups",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_UserGroups", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "UserOperationClaims",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					OperationClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Gender = table.Column<bool>(type: "bit", nullable: false),
					BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Status = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
					PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					UnitInStock = table.Column<short>(type: "smallint", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Products", x => x.Id);
					table.ForeignKey(
						name: "FK_Products_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ProductImages",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_ProductImages", x => x.Id);
					table.ForeignKey(
						name: "FK_ProductImages_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_ProductImages_ProductId",
				table: "ProductImages",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Customers");

			migrationBuilder.DropTable(
				name: "GroupClaims");

			migrationBuilder.DropTable(
				name: "Groups");

			migrationBuilder.DropTable(
				name: "Logs");

			migrationBuilder.DropTable(
				name: "OperationClaims");

			migrationBuilder.DropTable(
				name: "ProductImages");

			migrationBuilder.DropTable(
				name: "UserGroups");

			migrationBuilder.DropTable(
				name: "UserOperationClaims");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "Products");

			migrationBuilder.DropTable(
				name: "Categories");
		}
	}
}
