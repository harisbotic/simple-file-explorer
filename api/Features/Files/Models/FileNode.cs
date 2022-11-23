using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleFinder.Features.Files;

public class FileNode
{
    public int? Id { get; set; }
    public FileNodeTitle Title { get; set; }
    public FileTypes Type { get; set; }

    public int? ParentId { get; set; }
    public FileNode? Parent { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static readonly FileNode RootFileNode = new FileNode { Id = null };
}

public readonly record struct FileNodeTitle
{
    //can only contain word characters (a-z, 0-9, underscore), dash and dot
    static Regex regex = new Regex(@"^[\w-.]*$", RegexOptions.Compiled);

    public string Value { get; }

    public FileNodeTitle(string value)
    {
        value = value.Trim();

        if (string.IsNullOrEmpty(value) || !regex.Match(value.Replace(" ","")).Success)
        {
            throw new Exception($"Invalid title: {value}");
        }

        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static FileNodeTitle[] From(IEnumerable<string> values) => values.Select(v => new FileNodeTitle(v)).ToArray();
}