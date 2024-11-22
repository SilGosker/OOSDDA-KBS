﻿using Dapper;
using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.BoatType;

public class BoatTypeRepository : IBoatTypeRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public List<BoatTypeEntity> GetAll()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM boatType").ToList();
    }
    public void Delete(BoatTypeEntity boatType)
    {
        ThrowHelper.ThrowIfNull(boatType);

        _connection.Execute("DELETE FROM boatType WHERE BoatTypeID = @BoatTypeID", boatType);
        
    }
   
    public BoatTypeEntity GetByReservationId(int reservationID)
    {
        const string query = @"
        SELECT bt.* 
        FROM boatType bt
        INNER JOIN Boat b ON bt.BoattypeID = b.BoattypeID
        INNER JOIN Reservation r ON r.BoatID = b.BoatID
        WHERE r.ReservationID = @ReservationID";

        return _connection.Query<BoatTypeEntity>(query, new { ReservationID = reservationID }).FirstOrDefault();
    }

    public void Create(BoatTypeEntity boatType)
    {
        boatType.BoattypeID = _connection.Execute(
            "INSERT INTO boatType (Name, HasSteeringWheel, RequiredExperience, Seats, Speed) VALUES (@Name, @HasSteeringWheel, @RequiredExperience, @Seats, @Speed); SELECT SCOPE_IDENTITY()",
            boatType);
    }
    
    public List<BoatTypeEntity> GetName()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM boatType").ToList();
    }
   
    
    public BoatTypeEntity GetByBoatTypeID(int id)
    {
        const string query = @"SELECT * FROM boatType WHERE BoattypeID = @BoattypeID";
        return _connection.Query<BoatTypeEntity>(query, new { BoatTypeID = id }).FirstOrDefault();
    }
}