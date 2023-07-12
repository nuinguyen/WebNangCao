
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public class Staff
{
    [Key]
    public int Id { get; set; }
    [Required]

    public string ? Name { get; set; }
    [Required]

    public string ? Email { get; set; }

    public DateTime ? Birthday { get; set; }

    public string ? Gender { get; set; }

    public string ? Phone { get; set; }
    public string ? Address { get; set; }
    public string ? Password { get; set; }

}
