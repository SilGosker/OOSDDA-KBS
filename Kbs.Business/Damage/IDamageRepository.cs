using Kbs.Business.Boat;

namespace Kbs.Business.Damage;

public interface IDamageRepository
{
    List<DamageEntity> GetByBoat(BoatEntity boat);
    List<DamageEntity> GetSolvedByBoat(BoatEntity boat);
    DamageEntity GetById(int id);
    public void Solve(DamageEntity damage);
    public void Create(DamageEntity damage);

}