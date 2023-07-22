
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public class Village
{
    [Key]
    public int Id { get; set; }
    
    public string ? Name { get; set; }
    public int ? District_id { get; set; }

}
