using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HopsHub.Api.DTOs;


public class RatingDTO
{
    public required int BeerId { get; set; }

    public required Guid UserId { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    [Range(0.0, 10.0, ErrorMessage = "Score must be between 0.0 and 10.0")]
    public decimal Score { get; set; }

    [Column(TypeName = "text")]
    public string Comment { get; set; } = "";
}

