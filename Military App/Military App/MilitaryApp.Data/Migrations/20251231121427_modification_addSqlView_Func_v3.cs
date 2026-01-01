using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitaryApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class modification_addSqlView_Func_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @"CREATE OR ALTER FUNCTION dbo.funJoinColumnInfo 
                    ( 
                        @name nvarchar(50) 
                    ) 
                    returns nvarchar(100) 
                    as 
                    begin 
                    DECLARE @result NVARCHAR(100);
                    SET @result = @name;
                    RETURN @result;  
                    END");
            migrationBuilder.Sql(
               @"CREATE OR ALTER VIEW dbo.getBattle 
               AS  
               SELECT * from Militaries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.getBattle");
            migrationBuilder.Sql("DROP FUNCTION dbo.funJoinColumnInfo");
        }
    }
}
