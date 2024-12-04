namespace Kbs.Business.Parcours;

public interface IParcoursRepository
{
    public List<ParcoursEntity> GetAll();
    public ParcoursEntity GetById(int id);
}