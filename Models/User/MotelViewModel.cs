
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public class MotelViewModel
{
     public Motel Motel { get; set; }
    public List<Motel> ListMotel { get; set; }

}
