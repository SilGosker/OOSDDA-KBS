namespace Kbs.Business.Medal;

public interface IMedalRepository
{
    public void Create(MedalEntity medal);
    List<MedalEntity> GetAllByUserId(int userId);
    public void RemoveById(int medalId);
}