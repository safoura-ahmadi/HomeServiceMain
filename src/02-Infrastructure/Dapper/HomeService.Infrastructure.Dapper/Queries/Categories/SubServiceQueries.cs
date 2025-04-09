namespace HomeService.Infrastructure.Dapper.Queries.Categories;

public static class SubServiceQueries
{
    public readonly static string GetAll =
        "SELECT s.Id, s.Title, s.BasePrice, s.ImagePath, s.Description, sc.Title AS SubCategoryTitle " +
        "FROM SubServices s " +
        "JOIN SubCategories sc ON s.SubCategoryId = sc.Id " +
        "WHERE s.IsActive = 1;";
}
