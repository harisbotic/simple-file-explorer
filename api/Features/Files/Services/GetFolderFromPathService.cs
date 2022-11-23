using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleFinder.Features.Files;

public class GetFolderFromPathService
{
    private readonly SimpleFinderDbContext _db;

    public GetFolderFromPathService(SimpleFinderDbContext db)
    {
        _db = db;
    }

    public FileNode Execute(string[] pathSequence)
    {
        var pathQueue = new Queue<string>(pathSequence);
        if (pathQueue.Count() == 0)
        {
            return FileNode.RootFileNode;
        }

        var folders = _db.FileNodes.Where(x => FileNodeTitle.From(pathQueue).Contains(x.Title) && x.Type == FileTypes.Folder).ToList();

        FileNode? fileNode = FileNode.RootFileNode;
        while (pathQueue.Count >= 1)
        {
            var fileTitleToFind = pathQueue.Dequeue();

            fileNode = folders.Where(x => x.Title.Value == fileTitleToFind && x.ParentId == fileNode?.Id).SingleOrDefault();

            if (fileNode is null)
            {
                throw new Exception($"Folder with title: {fileTitleToFind} and parentId: {fileNode?.Id} wasn't found");
            }
        }

        return fileNode;
    }
}