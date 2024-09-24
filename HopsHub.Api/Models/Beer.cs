using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopsHub.Api.Models;

public class Beer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Alc { get; set; } = 0;
}

