using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ORION_BACKEND.Models;

public partial class CustomerAddress
{
    [Key]
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Street { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerAddresses")]
    public virtual Customer? Customer { get; set; }
}
