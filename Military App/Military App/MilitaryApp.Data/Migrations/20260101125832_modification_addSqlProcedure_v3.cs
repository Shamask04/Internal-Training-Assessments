using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitaryApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class modification_addSqlProcedure_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"create PROCEDURE dbo.uspGetMilitary 
                @militaryId int 
                AS 
                    SELECT militaryId, name from military 
                    WHERE militaryId = @militaryId 
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.uspGetMilitary");
        }
    }
}
