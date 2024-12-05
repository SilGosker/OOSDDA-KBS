using Dapper;
using Kbs.Business.Course;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Course;

public class CourseRepository : ICourseRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public List<CourseEntity> GetAll()
    {
        return _connection.Query<CourseEntity>("SELECT * FROM Course").ToList();
    }

    public CourseEntity GetById(int id)
    {
        return _connection.QueryFirstOrDefault<CourseEntity>("SELECT * FROM Course WHERE CourseID = @CourseID", new { CourseId = id });
    }
}