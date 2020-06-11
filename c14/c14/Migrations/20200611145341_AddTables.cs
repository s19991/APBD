using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace c14.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klients",
                columns: table => new
                {
                    IdKlient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(maxLength: 50, nullable: true),
                    Nazwisko = table.Column<string>(maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klients", x => x.IdKlient);
                });

            migrationBuilder.CreateTable(
                name: "Pracowniks",
                columns: table => new
                {
                    IdPracownik = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(maxLength: 50, nullable: true),
                    Nazwisko = table.Column<string>(maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracowniks", x => x.IdPracownik);
                });

            migrationBuilder.CreateTable(
                name: "WyrobCukierniczys",
                columns: table => new
                {
                    IdWyrobCukierniczy = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(maxLength: 200, nullable: true),
                    CenaZaSzt = table.Column<float>(nullable: false),
                    Typ = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WyrobCukierniczys", x => x.IdWyrobCukierniczy);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienies",
                columns: table => new
                {
                    IdZamowienia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPrzyjecia = table.Column<DateTime>(nullable: false),
                    DataRealizacji = table.Column<DateTime>(nullable: false),
                    Uwagi = table.Column<string>(maxLength: 300, nullable: true),
                    IdKlient = table.Column<int>(nullable: false),
                    IdPracownik = table.Column<int>(nullable: false),
                    KlientIdKlient = table.Column<int>(nullable: true),
                    PracownikIdPracownik = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienies", x => x.IdZamowienia);
                    table.ForeignKey(
                        name: "FK_Zamowienies_Klients_KlientIdKlient",
                        column: x => x.KlientIdKlient,
                        principalTable: "Klients",
                        principalColumn: "IdKlient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zamowienies_Pracowniks_PracownikIdPracownik",
                        column: x => x.PracownikIdPracownik,
                        principalTable: "Pracowniks",
                        principalColumn: "IdPracownik",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZamowienieWyrobCukierniczies",
                columns: table => new
                {
                    IdZamowienia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdWyrobCukierniczy = table.Column<int>(nullable: false),
                    Ilosc = table.Column<int>(nullable: false),
                    Uwagi = table.Column<string>(maxLength: 300, nullable: true),
                    WyrobCukierniczyIdWyrobCukierniczy = table.Column<int>(nullable: true),
                    ZamowienieIdZamowienia = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZamowienieWyrobCukierniczies", x => x.IdZamowienia);
                    table.ForeignKey(
                        name: "FK_ZamowienieWyrobCukierniczies_WyrobCukierniczys_WyrobCukierniczyIdWyrobCukierniczy",
                        column: x => x.WyrobCukierniczyIdWyrobCukierniczy,
                        principalTable: "WyrobCukierniczys",
                        principalColumn: "IdWyrobCukierniczy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZamowienieWyrobCukierniczies_Zamowienies_ZamowienieIdZamowienia",
                        column: x => x.ZamowienieIdZamowienia,
                        principalTable: "Zamowienies",
                        principalColumn: "IdZamowienia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Klients",
                columns: new[] { "IdKlient", "Imie", "Nazwisko" },
                values: new object[] { 1, "Linus", "Torvals" });

            migrationBuilder.InsertData(
                table: "Pracowniks",
                columns: new[] { "IdPracownik", "Imie", "Nazwisko" },
                values: new object[] { 1, "Cymbał", "Polak" });

            migrationBuilder.InsertData(
                table: "WyrobCukierniczys",
                columns: new[] { "IdWyrobCukierniczy", "CenaZaSzt", "Nazwa", "Typ" },
                values: new object[] { 1, 21.37f, "Mniami", "Mniamuwa" });

            migrationBuilder.InsertData(
                table: "ZamowienieWyrobCukierniczies",
                columns: new[] { "IdZamowienia", "IdWyrobCukierniczy", "Ilosc", "Uwagi", "WyrobCukierniczyIdWyrobCukierniczy", "ZamowienieIdZamowienia" },
                values: new object[] { 1, 1, 2137, "mniami ma byc", null, null });

            migrationBuilder.InsertData(
                table: "Zamowienies",
                columns: new[] { "IdZamowienia", "DataPrzyjecia", "DataRealizacji", "IdKlient", "IdPracownik", "KlientIdKlient", "PracownikIdPracownik", "Uwagi" },
                values: new object[] { 1, new DateTime(2020, 6, 11, 16, 53, 40, 268, DateTimeKind.Local).AddTicks(1809), new DateTime(2020, 6, 11, 16, 53, 40, 284, DateTimeKind.Local).AddTicks(2279), 1, 1, null, null, "aaaaaaaaaaaaaaaa" });

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienies_KlientIdKlient",
                table: "Zamowienies",
                column: "KlientIdKlient");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienies_PracownikIdPracownik",
                table: "Zamowienies",
                column: "PracownikIdPracownik");

            migrationBuilder.CreateIndex(
                name: "IX_ZamowienieWyrobCukierniczies_WyrobCukierniczyIdWyrobCukierniczy",
                table: "ZamowienieWyrobCukierniczies",
                column: "WyrobCukierniczyIdWyrobCukierniczy");

            migrationBuilder.CreateIndex(
                name: "IX_ZamowienieWyrobCukierniczies_ZamowienieIdZamowienia",
                table: "ZamowienieWyrobCukierniczies",
                column: "ZamowienieIdZamowienia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZamowienieWyrobCukierniczies");

            migrationBuilder.DropTable(
                name: "WyrobCukierniczys");

            migrationBuilder.DropTable(
                name: "Zamowienies");

            migrationBuilder.DropTable(
                name: "Klients");

            migrationBuilder.DropTable(
                name: "Pracowniks");
        }
    }
}
