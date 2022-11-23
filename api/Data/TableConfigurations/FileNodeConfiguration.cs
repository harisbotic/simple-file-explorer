using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFinder.Features.Files;

public class FileNodeConfiguration : IEntityTypeConfiguration<FileNode>
{
    public void Configure(EntityTypeBuilder<FileNode> builder)
    {
        builder.ToTable("file_nodes");

        builder.Property(e => e.Id).HasColumnName("id");

        builder.Property(e => e.Title)
            .HasColumnName("title")
            .HasMaxLength(256)    
            .HasConversion(
                v => v.ToString(),
                v => new FileNodeTitle(v));

        builder.Property(e => e.Type)
            .HasColumnName("file_type_name")
            .HasMaxLength(100)
            .HasConversion(
                v => v.ToString(),
                v => (FileTypes)Enum.Parse(typeof(FileTypes), v));

        builder.HasOne<FileType>()
            .WithMany()
            .HasForeignKey(e => e.Type)
            .HasConstraintName("file_nodes_file_types_name_fk");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("statement_timestamp()");

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("statement_timestamp()");

        builder.Property(e => e.ParentId)
            .HasColumnName("parent_id");

        builder.HasOne<FileNode>(e => e.Parent)
            .WithMany()
            .HasForeignKey(d => d.ParentId)
            .HasConstraintName("file_nodes_file_nodes_id_fk");

        builder.HasIndex(e => new { e.Title, e.Type }, "file_nodes_title_file_type_name_uindex")
            .IsUnique()
            .HasFilter("(parent_id IS NULL)");

        builder.HasIndex(e => new { e.Title, e.ParentId, e.Type }, "file_nodes_title_parent_id_file_type_name_uindex")
            .IsUnique()
            .HasFilter("(parent_id IS NOT NULL)");
    }
}