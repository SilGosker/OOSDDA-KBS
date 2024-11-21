using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    public interface IReservationRepository
    {
        public void Create(ReservationEntity reservation);
        public void Update(ReservationEntity reservation);
        public void Delete(ReservationEntity reservation);
        public ReservationEntity GetById(int id);

        public List<ReservationEntity> Get();
        public List<ReservationEntity> OrderByStatus(List<ReservationEntity> reservations);
    }
}
