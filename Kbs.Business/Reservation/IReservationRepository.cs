using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    class IReservationRepository
    {
        public void Create(ReservationEntity reservation) { }
        public void Update(ReservationEntity reservation) { }
        public void Delete(ReservationEntity reservation) { }
        public List<ReservationEntity> GetByUserId (int userId) { return null; }
        public List<ReservationEntity> Get() { return null; }
    }
}
