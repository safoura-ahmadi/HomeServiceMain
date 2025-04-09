namespace HomeService.Infrastructure.Dapper.Queries.Categories;

public static class SubCategoryQueries
{
    public readonly static string GetAll =
        "SELECT sc.Id, sc.Title, c.Title AS CategoryName " +
        "FROM SubCategories sc " +
        "JOIN Categories c ON c.Id = sc.CategoryId " +
        "WHERE sc.IsActive = 1";
}
