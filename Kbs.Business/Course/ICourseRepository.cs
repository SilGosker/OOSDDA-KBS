namespace Kbs.Business.Course;

public interface ICourseRepository
{
    public List<CourseEntity> GetAll();
    public CourseEntity GetById(int id);
}