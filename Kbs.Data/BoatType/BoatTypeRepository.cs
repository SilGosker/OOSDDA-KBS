using Dapper;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kbs.Data.BoatType;

public class BoatTypeRepository : IBoatTypeRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public List<BoatTypeEntity> GetAll()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype").ToList();
    }
    public void Delete(BoatTypeEntity Boattype)
    {     
        /*
        if (Boattype.BoattypeID == 0 || Boattype.BoattypeID == null) return;
        _connection.Execute("DELETE FROM Boattype WHERE BoatTypeID = @BoatTypeID", new { BoatTypeID = Boattype.BoattypeID });
        */

         //Dit werkt wel volgens chat (als in het verwijdert niks maar het geeft geen foutmelding -> voor testen waar het fout gaat)
        // Controleer of Boattype null is voordat je verder gaat
        if (Boattype == null)
        {
            Console.WriteLine("Boattype is null. Kan niet verwijderen.");
            return; // Voorkom verdere uitvoering als Boattype null is
        }

        // Zorg ervoor dat BoatTypeID niet gelijk is aan 0
        if (Boattype.BoattypeID == 0)
        {
            Console.WriteLine("BoattypeID is 0. Kan niet verwijderen.");
            return; // Voorkom verdere uitvoering als BoatTypeID 0 is
        }

        // Als de checks succesvol zijn, voer dan de delete uit
        _connection.Execute(
            "DELETE FROM Boattype WHERE BoatTypeID = @BoatTypeID",
            new { BoatTypeID = Boattype.BoattypeID }
        );
        
    }
   

    public BoatTypeEntity GetByReservationId(int reservationID)
    {
        const string query = @"
        SELECT bt.* 
        FROM Boattype bt
        INNER JOIN Boat b ON bt.BoattypeID = b.BoattypeID
        INNER JOIN Reservation r ON r.BoatID = b.BoatID
        WHERE r.ReservationID = @ReservationID";

        return _connection.Query<BoatTypeEntity>(query, new { ReservationID = reservationID }).FirstOrDefault();
    }
    public void Create(BoatTypeEntity boatType)
    {
        boatType.BoattypeID = _connection.Execute(
            "INSERT INTO Boattype (Name, HasSteeringWheel, RequiredExperience, Seats, Speed) VALUES (@Name, @HasSteeringWheel, @RequiredExperience, @Seats, @Speed); SELECT SCOPE_IDENTITY()",
            boatType);
    }
    
    public List<BoatTypeEntity> GetName()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype").ToList();
    }
    public BoatTypeEntity GetByName(string name)
    {
        //return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype WHERE name = @Name", name);
        const string query = @"SELECT * FROM Boattype WHERE Name = @Name";
        return _connection.Query<BoatTypeEntity>(query, new { Name = name }).FirstOrDefault();
    }
    
    public BoatTypeEntity GetByBoatTypeID(int id)
    {
        const string query = @"SELECT * FROM Boattype WHERE BoattypeID = @BoattypeID";
        return _connection.Query<BoatTypeEntity>(query, new { BoatTypeID = id }).FirstOrDefault();
    }
    /*
    public void Delete(List<BoatTypeEntity> entity)
    {
        throw new NotImplementedException();
    }
    */

}