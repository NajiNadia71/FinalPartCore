using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "030FB67C-A050-4D47-98A8-C857DBA89576", "", "GetUserInfo", "" },
                    { "1EA03C12-3EEE-43D1-B9C6-5EB06321A130", "", "GetAllRolesOfGroupRole", "" },
                    { "2A18E626-6A6F-4755-BB1A-06E3BD741519", "", "EditRoleGroupd", "" },
                    { "3BD071B4-EBA4-4C47-845E-4F255BD8C4C0", "", "DeleteRoleFromRoleGroup", "" },
                    { "3F781779-E0A0-4245-8183-1837B7554A3D", "", "GetAllUsers", "" },
                    { "3FFBD3F9-B616-4164-8EA7-A473EBC4B1BD", "", "GetAllRolesGroupsForUser", "" },
                    { "6AF8DEB0-DEC7-4BD5-A734-D5AF20380857", "", "AddRolesForUser", "" },
                    { "7D5CBCE5-CBC1-4DC4-9F94-E394A2BFB7A9", "", "GetAllRolesForUser", "" },
                    { "7E3A93C4-BFA4-4098-8742-9185B123DC31", "", "DeleteRolesForUser", "" },
                    { "B1C8B66B-7B9A-4C72-A758-08B95A348188", "", "AddRoleToRoleGroup", "" },
                    { "BB109ED0-2183-47A7-BC7B-464047400115", "", "CreateRoleGroup", "" },
                    { "E0967BB8-DCB8-441D-BB3F-3628BC743302", "", "AddRoleGroupForUser", "" },
                    { "E1B4457B-2E32-431D-8A0A-A8CD59828821", "", "DeleteRoleGroupForUser", "" },
                    { "E1F01805-25A1-478F-80FF-5338B379840E", "", "EditRoleGroup", "" },
                    { "E8015CA3-DB29-46AB-95DF-04E484D78835", "", "GetAllGroupRoleNames", "" },
                    { "F18B19B5-962F-4E1E-A484-882BF1FE398E", "", "DeleteRoleGroup", "" },
                    { "FE84485D-7F8D-43E0-B069-5157BBB68F0F", "", "CreateRoleGroupd", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "030FB67C-A050-4D47-98A8-C857DBA89576");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1EA03C12-3EEE-43D1-B9C6-5EB06321A130");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2A18E626-6A6F-4755-BB1A-06E3BD741519");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3BD071B4-EBA4-4C47-845E-4F255BD8C4C0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3F781779-E0A0-4245-8183-1837B7554A3D");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3FFBD3F9-B616-4164-8EA7-A473EBC4B1BD");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6AF8DEB0-DEC7-4BD5-A734-D5AF20380857");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7D5CBCE5-CBC1-4DC4-9F94-E394A2BFB7A9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7E3A93C4-BFA4-4098-8742-9185B123DC31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B1C8B66B-7B9A-4C72-A758-08B95A348188");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "BB109ED0-2183-47A7-BC7B-464047400115");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E0967BB8-DCB8-441D-BB3F-3628BC743302");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E1B4457B-2E32-431D-8A0A-A8CD59828821");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E1F01805-25A1-478F-80FF-5338B379840E");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8015CA3-DB29-46AB-95DF-04E484D78835");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F18B19B5-962F-4E1E-A484-882BF1FE398E");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "FE84485D-7F8D-43E0-B069-5157BBB68F0F");
        }
    }
}
