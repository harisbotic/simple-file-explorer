using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFinder.Features.Files;

public class FileTypeConfiguration : IEntityTypeConfiguration<FileType>
{
    public void Configure(EntityTypeBuilder<FileType> builder)
    {
        builder.ToTable("file_types");

        builder.HasKey(e => e.Name)
                .HasName("file_types_pkey");

        builder.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .HasConversion(
                    v => v.ToString(),
                    v => (FileTypes)Enum.Parse(typeof(FileTypes), v));

        builder.Property(e => e.Description)
            .HasMaxLength(500)
            .HasColumnName("description");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("statement_timestamp()");

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("statement_timestamp()");

        builder.HasData(
            new FileType { Name = FileTypes.Folder, Description = "Representing a folder / directory" },
            new FileType { Name = FileTypes.Video, Description = "Representing a video file" });
    }
}