namespace Kbs.Business.Game;

public interface IGameRepository
{
    public void Create(GameEntity game);
    GameEntity GetById(int gameId);
}