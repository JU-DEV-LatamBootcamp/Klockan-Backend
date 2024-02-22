using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KlockanAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addd_Avatar_to_Users_and_Seeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "AR", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7809), null, "Argentina", null },
                    { 2, "BO", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7811), null, "Bolivia", null },
                    { 3, "BR", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7812), null, "Brazil", null },
                    { 4, "CL", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7813), null, "Chile", null },
                    { 5, "CO", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7813), null, "Colombia", null },
                    { 6, "CR", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7814), null, "Costa Rica", null },
                    { 7, "CU", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7815), null, "Cuba", null },
                    { 8, "DO", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7816), null, "Dominican Republic", null },
                    { 9, "EC", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7817), null, "Ecuador", null },
                    { 10, "SV", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7818), null, "El Salvador", null },
                    { 11, "GT", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7818), null, "Guatemala", null },
                    { 12, "HT", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7819), null, "Haiti", null },
                    { 13, "HN", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7820), null, "Honduras", null },
                    { 14, "MX", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7821), null, "Mexico", null },
                    { 15, "NI", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7822), null, "Nicaragua", null },
                    { 16, "PA", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7823), null, "Panama", null },
                    { 17, "PY", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7824), null, "Paraguay", null },
                    { 18, "PE", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7824), null, "Peru", null },
                    { 19, "PR", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7825), null, "Puerto Rico", null },
                    { 20, "UY", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7826), null, "Uruguay", null },
                    { 21, "VE", new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7827), null, "Venezuela", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8108), null, "Admin", null },
                    { 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8110), null, "Trainer", null },
                    { 3, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8111), null, "Student", null },
                    { 4, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8112), null, "Guest", null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Code", "CountryId", "CreatedAt", "DeletedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "BA", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7870), null, "Buenos Aires", null },
                    { 2, "COR", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7872), null, "Córdoba", null },
                    { 3, "ROS", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7873), null, "Rosario", null },
                    { 4, "MDZ", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7874), null, "Mendoza", null },
                    { 5, "TUC", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7875), null, "San Miguel de Tucumán", null },
                    { 6, "LP", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7876), null, "La Plata", null },
                    { 7, "MDQ", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7877), null, "Mar del Plata", null },
                    { 8, "SLA", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7878), null, "Salta", null },
                    { 9, "SFN", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7879), null, "Santa Fe", null },
                    { 10, "SJU", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7880), null, "San Juan", null },
                    { 11, "TUC", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7881), null, "Tucumán", null },
                    { 12, "NEU", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7882), null, "Neuquén", null },
                    { 13, "BHI", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7883), null, "Bahía Blanca", null },
                    { 14, "RES", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7884), null, "Resistencia", null },
                    { 15, "FOR", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7885), null, "Formosa", null },
                    { 16, "COR", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7886), null, "Corrientes", null },
                    { 17, "POS", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7886), null, "Posadas", null },
                    { 18, "JUJ", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7887), null, "San Salvador de Jujuy", null },
                    { 19, "PAR", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7888), null, "Paraná", null },
                    { 20, "SRO", 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7889), null, "Santa Rosa", null },
                    { 21, "LP", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7890), null, "La Paz", null },
                    { 22, "SCZ", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7891), null, "Santa Cruz de la Sierra", null },
                    { 23, "CBBA", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7892), null, "Cochabamba", null },
                    { 24, "SUC", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7893), null, "Sucre", null },
                    { 25, "TJA", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7894), null, "Tarija", null },
                    { 26, "PTS", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7895), null, "Potosí", null },
                    { 27, "ORU", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7950), null, "Oruro", null },
                    { 28, "CBI", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7951), null, "Cobija", null },
                    { 29, "TRI", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7952), null, "Trinidad", null },
                    { 30, "RIB", 2, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7953), null, "Riberalta", null },
                    { 31, "SP", 3, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7953), null, "São Paulo", null },
                    { 32, "RJ", 3, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7954), null, "Rio de Janeiro", null },
                    { 33, "SCL", 4, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7955), null, "Santiago", null },
                    { 34, "VAL", 4, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7957), null, "Valparaíso", null },
                    { 35, "BOG", 5, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7958), null, "Bogotá", null },
                    { 36, "MDE", 5, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7959), null, "Medellín", null },
                    { 37, "SJ", 6, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7960), null, "San José", null },
                    { 38, "LM", 6, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7961), null, "Limon", null },
                    { 39, "LH", 7, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7962), null, "La Habana", null },
                    { 40, "SC", 7, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7963), null, "Santiago de Cuba", null },
                    { 41, "SD", 8, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7964), null, "Santo Domingo", null },
                    { 42, "SC", 8, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7965), null, "Santiago de los Caballeros", null },
                    { 43, "QT", 9, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7966), null, "Quito", null },
                    { 44, "GQ", 9, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7967), null, "Guayaquil", null },
                    { 45, "SS", 10, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7968), null, "San Salvador", null },
                    { 46, "SA", 10, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7969), null, "Santa Ana", null },
                    { 47, "GC", 11, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7970), null, "Guatemala City", null },
                    { 48, "QZ", 11, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7971), null, "Quetzaltenango", null },
                    { 49, "PA", 12, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7972), null, "Port-au-Prince", null },
                    { 50, "CH", 12, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7973), null, "Cap-Haïtien", null },
                    { 51, "TG", 13, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7974), null, "Tegucigalpa", null },
                    { 52, "SPS", 13, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7975), null, "San Pedro Sula", null },
                    { 53, "MXC", 14, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7976), null, "Mexico City", null },
                    { 54, "GDL", 14, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7977), null, "Guadalajara", null },
                    { 55, "MN", 15, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7978), null, "Managua", null },
                    { 56, "LN", 15, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7979), null, "León", null },
                    { 57, "PC", 16, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7980), null, "Panama City", null },
                    { 58, "DV", 16, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7981), null, "David", null },
                    { 59, "ASU", 17, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7982), null, "Asunción", null },
                    { 60, "CDE", 17, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7983), null, "Ciudad del Este", null },
                    { 61, "LM", 18, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7984), null, "Lima", null },
                    { 62, "AR", 18, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7985), null, "Arequipa", null },
                    { 63, "SJ", 19, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7986), null, "San Juan", null },
                    { 64, "PC", 19, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7987), null, "Ponce", null },
                    { 65, "MTV", 20, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7987), null, "Montevideo", null },
                    { 66, "ST", 20, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7988), null, "Salto", null },
                    { 67, "CCS", 21, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7989), null, "Caracas", null },
                    { 68, "MAR", 21, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(7990), null, "Maracaibo", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Birthdate", "CityId", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "RoleId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "https://randomuser.me/api/portraits/men/92.jpg", new DateOnly(1990, 5, 15), 1, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8185), null, "martin.lopez@jala.university", "Martín", "López", 1, null },
                    { 2, "https://randomuser.me/api/portraits/women/80.jpg", new DateOnly(1988, 8, 20), 3, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8225), null, "lucia.martinez@jala.university", "Lucía", "Martínez", 2, null },
                    { 3, "https://randomuser.me/api/portraits/men/57.jpg", new DateOnly(1992, 3, 10), 21, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8229), null, "carlos.gutierrez@jala.university", "Carlos", "Gutiérrez", 2, null },
                    { 4, "https://randomuser.me/api/portraits/women/30.jpg", new DateOnly(1991, 11, 25), 22, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8232), null, "maria.perez@jala.university", "María", "Pérez", 3, null },
                    { 5, "https://randomuser.me/api/portraits/men/12.jpg", new DateOnly(1985, 7, 3), 35, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8236), null, "alejandro.rodriguez@jala.university", "Alejandro", "Rodríguez", 4, null },
                    { 6, "https://randomuser.me/api/portraits/women/57.jpg", new DateOnly(1989, 9, 12), 36, new DateTime(2024, 2, 16, 16, 34, 10, 188, DateTimeKind.Utc).AddTicks(8239), null, "camila.gomez@jala.university", "Camila", "Gómez", 4, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");
        }
    }
}
