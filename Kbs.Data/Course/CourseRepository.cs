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
    public void Create(CourseEntity course)
    {
        course.CourseId = _connection.QueryFirst<int>(
            "INSERT INTO Course (Name, Difficulty, Description, Image) VALUES (@Name, @Difficulty, @Description, @Image); SELECT SCOPE_IDENTITY()",
            course);
    }

    public void Update(CourseEntity course)
    {
        _connection.Execute("UPDATE Course SET Name = @Name, Description = @Description, Difficulty = @Difficulty, Image = @Image WHERE CourseID = @CourseID", course);
    }
    public void Delete(int id) 
    {
        _connection.Execute("UPDATE Game SET CourseId = NULL WHERE CourseId = @CourseId;", new { CourseId = id });
        _connection.Execute("DELETE FROM Course WHERE CourseId = @CourseId", new { CourseId = id });
    }
}