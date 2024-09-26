using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopsHub.Api.Models;

public class Beer
{
    public int Id { get; set; }

    public required string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Alc { get; set; } = 0;

    [Column(TypeName = "decimal(3, 2)")]
    public decimal Rating { get; set; } = 0;

    //Foreign key property
    public int TypeId { get; set; }

    //Navigation Property
    public Type? Type { get; set; }
}

