using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace SimpleFinder.Features.Files;

public class MoveTo
{
    public record struct Command
    {
        public string Title { get; init; }
        public string Path { get; init; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FileTypes Type { get; init; }

        public string DestinationPath { get; init; }
    }

    public static void Handle(Command command, SimpleFinderDbContext db)
    {
        var pathSequence = command.Path.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var destinationPathSequence = command.DestinationPath.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);


        var fileFolder = new GetFolderFromPathService(db).Execute(pathSequence);
        var fileNode = db.FileNodes.FirstOrDefault(x => 
            x.Title == new FileNodeTitle(command.Title)
            && x.ParentId == fileFolder.Id
            && x.Type == command.Type);

        if(fileNode is null)
        {
            throw new Exception($"FileNode doesn't exist with title: {command.Title}, type: {command.Type}, path: {command.Path}");
        }

        var destinationFolder = new GetFolderFromPathService(db).Execute(destinationPathSequence);

        //TODO: Check if video or folder with the same name exists in the moveTo destination
        //If it does, handle error

        fileNode.ParentId = destinationFolder.Id;
        db.SaveChanges();
    }
}
