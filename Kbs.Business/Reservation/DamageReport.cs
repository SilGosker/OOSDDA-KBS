using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    public class DamageReport
    {
        public int BoatID {  get; set; }
        public string DamageDescription { get; set; }
        public DateTime? DateReported { get; set; }j
        public byte[] Image { get; set; }
    }
}
