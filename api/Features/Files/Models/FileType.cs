using System;

namespace SimpleFinder.Features.Files;

public class FileType
{
    public FileTypes Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum FileTypes
{
    Folder,
    Video
}