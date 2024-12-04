using Dapper;
using Kbs.Business.Parcours;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Parcours;

public class ParcoursRepository : IParcoursRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public List<ParcoursEntity> GetAll()
    {
        return _connection.Query<ParcoursEntity>("SELECT * FROM Parcours").ToList();
    }

    public ParcoursEntity GetById(int id)
    {
        return _connection.QueryFirstOrDefault<ParcoursEntity>("SELECT * FROM Parcours WHERE ParcoursID = @ParcoursID", new { ParcoursID = id });
    }
}