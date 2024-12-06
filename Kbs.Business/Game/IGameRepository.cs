namespace Kbs.Business.Game;

public interface IGameRepository
{
    public void Create(GameEntity game);
    public List<GameEntity> GetByGameId(int userId);

}