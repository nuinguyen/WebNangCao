
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public class Favorite
{
    [Key]
    public int Id { get; set; }
    public int User_id { get; set; }

}
