﻿using Dapper;
using Kbs.Business.Boat;
using Kbs.Business.Damage;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Damage;

public class DamageRepository : IDamageRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public List<DamageEntity> GetByBoat(BoatEntity boat)
    {
       return  _connection.Query<DamageEntity>("SELECT * FROM Damage WHERE BoatID = @BoatId AND Status = 2", boat).ToList();
    }

    public List<DamageEntity> GetSolvedByBoat(BoatEntity boat)
    {
        return _connection.Query<DamageEntity>("SELECT * FROM Damage WHERE BoatID = @BoatId AND Status = 1", boat).ToList();
    }

    public DamageEntity GetById(int id)
    {
        return _connection.QueryFirstOrDefault<DamageEntity>("SELECT * FROM Damage WHERE DamageId = @Id", new { Id = id });
    }

    public bool HasDamage(BoatEntity boat)
    {
        return _connection.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM Damage WHERE BoatID = @BoatId AND Status = 2", boat) > 0;
    }
}