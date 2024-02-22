


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

    internal Keep UpdateKeep(Keep updateData, string userId)
    {
        Keep original = GetKeepById(updateData.Id);
        if (original.CreatorId != userId) throw new Exception("Not your keep to update!");
        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;
        Keep update = repo.Update(original);
        return update;

    }
    internal string DeleteKeep(int keepId, string userId)
    {
        Keep original = repo.GetById(keepId);
        if (original.CreatorId != userId) throw new Exception("Not your Keep to delete!");
        repo.Delete(keepId);
        return $"Keep {original.Name} has been deleted.";
    }

    internal Keep IncreaseViews(int keepId)
    {
        Keep keep = repo.GetById(keepId);
        keep.Views++;
        repo.Update(keep);
        return keep;
    }
    internal Keep IncreaseViews(Keep keep)
    {
        keep.Views++;
        repo.Update(keep);
        return keep;
    }


}