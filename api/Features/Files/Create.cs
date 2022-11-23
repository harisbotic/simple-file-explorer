using System;
using System.Text.Json.Serialization;

namespace SimpleFinder.Features.Files;

public class Create
{
    public record struct Command
    {
        public string Title { get; init; }
        public string Path { get; init; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FileTypes Type { get; init; }
    }

    public static void Handle(Command command, SimpleFinderDbContext db)
    {
        var pathSequence = command.Path.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var destinationFolder = new GetFolderFromPathService(db).Execute(pathSequence);

        //TOOD: Check if a file exists with same name and same type
        db.FileNodes.Add(new FileNode
        {
            Title = new FileNodeTitle(command.Title),
            Type = command.Type,
            ParentId = destinationFolder.Id
        });

        db.SaveChanges();
    }
}
