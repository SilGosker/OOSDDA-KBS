using Kbs.Business.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Medal
{
    public interface IMedalRepository
    {
        public void Create(MedalEntity medal);
        public void Delete(MedalEntity medal);
    }
}
