namespace Kbs.Business.Game;

public interface IGameRepository
{
    public void Create(GameEntity game);
    public GameEntity GetById(int id);

}