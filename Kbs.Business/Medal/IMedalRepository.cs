namespace Kbs.Business.Medal;

public interface IMedalRepository
{
    public void Create(MedalEntity medal);
    public List<MedalEntity> GetByUserId(int userId);
}