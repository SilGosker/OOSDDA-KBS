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

        public List<ReservationEntity> GetByUserId (int userId) 
        { 
            StringBuilder s = new StringBuilder();
            List<ReservationEntity> SpecifiekeLijst = new List<ReservationEntity>();
            return SpecifiekeLijst;
        }
        public List<ReservationEntity> Get() 
        { 
            StringBuilder s = new StringBuilder();
            List<ReservationEntity> VolledigeLijst = new List<ReservationEntity>();
            return VolledigeLijst;
        }
    }
}
