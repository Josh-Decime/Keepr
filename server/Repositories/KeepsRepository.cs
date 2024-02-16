
namespace Keepr.Repositories;

public class KeepsRepository(IDbConnection db) : IRepository<Keep>
{
    private readonly IDbConnection db = db;
    // -------------------------------------------
    public Keep Create(Keep createData)
    {
        throw new NotImplementedException();
    }

    public List<Keep> GetAll()
    {
        throw new NotImplementedException();
    }

    public Keep GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Keep Update(Keep updateData)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}