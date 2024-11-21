﻿using Dapper;
using Kbs.Business.Boat;
using Kbs.Business.User;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Kbs.Data.Boat;

public class BoatRepository : IBoatRepository
{
    private readonly SqlConnection _connection = new SqlConnection(DatabaseConstants.ConnectionString);
    public List<BoatEntity> GetMany()
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat").ToList();
    }

    public List<BoatEntity> GetManyByType(int boatTypeId)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE BoatTypeId = @boatTypeId", new { boatTypeId }).ToList();
    }

    public List<BoatEntity> GetManyByName(string name)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE LOWER(Name) LIKE @name", new { name = $"%{name.ToLower()}%" }).ToList();
    }

    public List<BoatEntity> GetManyByNameAndType(string name, int boatTypeId)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE LOWER(Name) LIKE @name AND BoatTypeId = @boatTypeId", new { name = $"%{name.ToLower()}%", boatTypeId }).ToList();
    }

    public BoatEntity GetById(int boatId)
    {
        return _connection.QueryFirstOrDefault<BoatEntity>("SELECT * FROM Boat WHERE BoatId = @boatId", new { boatId });
    }

    public void UpdateDeleteRequestDate(int boatId, DateTime? requestDate)
    {
        _connection.Execute("UPDATE Boat SET DeleteRequestDate = @DeleteRequestDate WHERE BoatId = @boatId", new { boatId, requestDate });
    }

    public void DeleteById(int boatId)
    {
        _connection.Execute("DELETE WHERE BoatId = @boatId", new { boatId });
    }
}