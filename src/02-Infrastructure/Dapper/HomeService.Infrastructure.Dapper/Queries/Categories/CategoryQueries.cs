namespace HomeService.Infrastructure.Dapper.Queries.Categories;

public static class CategoryQueries
{
    public readonly static string GetAll =
        "SELECT c.Id, c.Title, c.ImagePath, " +
        "(SELECT COUNT(*) " +
        "FROM SubCategories sc " +
        "JOIN SubServices ss ON sc.Id = ss.SubCategoryId " +
        "WHERE sc.CategoryId = c.Id AND ss.IsActive = 1) AS SubServiceCount " +
        "FROM Categories c " +
        "WHERE c.IsActive = 1";
}