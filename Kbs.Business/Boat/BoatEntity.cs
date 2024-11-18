
using System.Formats.Tar;

namespace Kbs.Business.Boat;

public class BoatEntity
{
    public int BoatID { get; set; }
    public string Name { get; set; }
    public int BoatTypeId { get; set; }
    public BoatStatus Status { get; set; }
}