
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public class Favourite_detail
{
    [Key]
    public int Id { get; set; }
    public int Favourite_id { get; set; }
    public int Motel_id { get; set; }

}
