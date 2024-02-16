namespace Keepr.Interfaces;

public interface IRepository<Type>
{
    Type Create(Type createData);
    List<Type> GetAll();
    Type GetById(int id);
    Type Update(Type updateData);
    void Delete(int id);
}