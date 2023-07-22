
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public class District
{
    [Key]
    public int Id { get; set; }
    
    public string ? Name { get; set; }
    public int ? City_id { get; set; }

}
