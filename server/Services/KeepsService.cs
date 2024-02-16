


namespace Keepr.Services;

public class KeepsService(KeepsRepository repo)
{
    private readonly KeepsRepository repo = repo;
    // ---------------------------------------------------

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = repo.Create(keepData);
        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = repo.GetAll();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = repo.GetById(keepId);
        if (keep == null) throw new Exception($"No keep at id: {keepId}");
        return keep;
    }

    internal Keep UpdateKeep(Keep updateData)
    {
        Keep original = GetKeepById(updateData.Id);
        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;
        Keep update = repo.Update(original);
        return update;

    }
}