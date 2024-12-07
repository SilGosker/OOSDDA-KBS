namespace Kbs.Business.Game;

 public interface IGameRepository
 {
     public void Create(GameEntity game);
     public GameEntity GetById(int gameId);
     public List<GameEntity> GetMany();
     public List<GameEntity> GetManyByCourse(int courseId);
     public List<GameEntity> GetManyByName(string name);
     public List<GameEntity> GetManyByNameAndCourse(string name, int courseId);
     public void DeleteById(int gameId);
     public void Update(GameEntity game);
 }