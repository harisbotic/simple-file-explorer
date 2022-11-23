using System;
using System.Linq;

namespace SimpleFinder.Features.Files;

public class GetFilesFromPath
{
    public record struct Result
    {
        public string Title { get; init; }
        public string Type { get; init; }
        public string Path { get; init; }
    }

    public static Result[] Handle(string path, SimpleFinderDbContext db)
    {
       var pathSequence = path.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var folder = new GetFolderFromPathService(db).Execute(pathSequence);

        //TODO: Add pagination
        return db.FileNodes.Where(x => x.ParentId == folder.Id).OrderBy(x => x.UpdatedAt).Select(x => new Result
        {
            Title = x.Title.Value,
            Type = x.Type.ToString(),
            Path = String.Join("/", pathSequence)
        }).ToArray(); 
    }
}
