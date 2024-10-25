using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ORION_BACKEND.Models;

public class CustomerAddressDTO
{
    public int? Id { get; set; }

    public int? CustomerId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? Street { get; set; }

    [StringLength(50)]
    public string? PostalCode { get; set; }

    [StringLength(200)]
    public string? City { get; set; }

    [StringLength(200)]
    public string? Country { get; set; }
}
