using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Infrastructure.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegisterDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Bytes = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lnguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lnguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TicketTypeEnum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPrivateAccount = table.Column<bool>(type: "bit", nullable: false),
                    NipNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFullDay = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEvents_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfEmployemnt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfDismissal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    WebsiteRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SettingsPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingsPositions_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketTypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypeTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    TicketTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketTypeTranslations_Lnguages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Lnguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketTypeTranslations_TicketTypes_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MethodOfPayment = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "Id", "Date", "Description" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kod promocyjny na suplementy w sklepie ABC = rabat12." },
                    { 2, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "W najbliższą niedzielę siłownia jest otwarta do godziny 24:00." },
                    { 3, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jutrzejsze zajęcia Crossfit zostały odwołane. Przepraszamy." },
                    { 4, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zatrudnimy trenera personalnego." },
                    { 5, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Od przyszłego miesiąca możesz kupić karnet dla swóch osób w cenie jednego." },
                    { 6, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zatrudnimy recepcjonistę." },
                    { 7, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "W poprzednim miesiącu zrobiłeś aż 12 treningów - gratulacje!" },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Szczęśliwego Nowego Roku!" },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Odbierz kod zniżkowy na suplementację w recepcji" },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kod promocyjny na suplementy w sklepie ABC = rabat12." },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kod promocyjny na suplementy w sklepie ABC = rabat12." },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kod promocyjny na suplementy w sklepie ABC = rabat12." }
                });

            migrationBuilder.InsertData(
                table: "Lnguages",
                columns: new[] { "Id", "Key", "Name" },
                values: new object[,]
                {
                    { 1, "pl", "Polski" },
                    { 2, "en", "Angielski" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Description", "Order" },
                values: new object[,]
                {
                    { 1, "E-mail", 2 },
                    { 2, "Ogólne", 1 }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Price", "TicketTypeEnum" },
                values: new object[,]
                {
                    { 1, 10m, 0 },
                    { 2, 25m, 1 },
                    { 3, 100m, 2 },
                    { 4, 1000m, 3 }
                });

            migrationBuilder.InsertData(
                table: "SettingsPositions",
                columns: new[] { "Id", "Description", "Key", "Order", "SettingsId", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "Host", "HostSmtp", 1, 1, 0, "smtp.gmail.com" },
                    { 2, "Port", "Port", 2, 1, 2, "587" },
                    { 3, "Adres e-mail nadawcy", "SenderEmail", 3, 1, 0, "" },
                    { 4, "Hasło", "SenderEmailPassword", 4, 1, 4, "" },
                    { 5, "Nazwa nadawcy", "SenderName", 5, 1, 0, "Kazimierz Szpin" },
                    { 6, "Login nadawcy", "SenderLogin", 6, 1, 0, "" },
                    { 7, "Czy wyświetlać baner na stronie głównej?", "BannerVisible", 1, 2, 1, "True" },
                    { 8, "Kolor footera strony głównej", "FooterColor", 2, 2, 5, "#dc3545" },
                    { 9, "Główny adres e-mail administratora", "AdminEmail", 3, 2, 0, "aswen124@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypeTranslations",
                columns: new[] { "Id", "LanguageId", "Name", "TicketTypeId" },
                values: new object[,]
                {
                    { 1, 1, "Jednorazowy", 1 },
                    { 2, 2, "Single", 1 },
                    { 3, 1, "Tygodniowy", 2 },
                    { 4, 2, "Weekly", 2 },
                    { 5, 1, "Miesięczny", 3 },
                    { 6, 2, "Monthly", 3 },
                    { 7, 1, "Roczny", 4 },
                    { 8, 2, "Annual", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvents_UserId",
                table: "EmployeeEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TicketId",
                table: "Invoices",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsPositions_SettingsId",
                table: "SettingsPositions",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTypeTranslations_LanguageId",
                table: "TicketTypeTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTypeTranslations_TicketTypeId",
                table: "TicketTypeTranslations",
                column: "TicketTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "EmployeeEvents");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "SettingsPositions");

            migrationBuilder.DropTable(
                name: "TicketTypeTranslations");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Lnguages");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "TicketTypes");
        }
    }
}
