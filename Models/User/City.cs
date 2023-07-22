
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public class City
{
    [Key]
    public int Id { get; set; }
    
    public string ? Name { get; set; }

}
