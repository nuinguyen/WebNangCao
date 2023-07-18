
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public class Motel
{
    [Key]
    public int Id { get; set; }
    
    public int User_id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public Double? Acreage { get; set; }

    public Double? Price { get; set; }

    [NotMapped]
    public IFormFile? Images { get; set; }
    public string? Image { get; set; }
    public string? Address_post { get; set; }
    public string? Name { get; set; }

    public string ? Phone { get; set; }
    public string ? Address { get; set; }
    public string ? Email { get; set; }
    public DateTime ? Date_created { get; set; }
    public int ? Status { get; set; }
    

}
public class SearchMotel
{
    [Key]
    public int Id { get; set; }
    public int User_id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public Double? Acreage { get; set; }

    public Double? Price { get; set; }

    [NotMapped]
    public IFormFile? Images { get; set; }
    public string? Image { get; set; }
    public string? Address_post { get; set; }
    public string? Name { get; set; }

    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public DateTime? Date_created { get; set; }
    public int? Status { get; set; }

}