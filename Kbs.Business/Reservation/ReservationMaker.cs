using Kbs.Business.Boat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
    {
    public class ReservationMaker
        {
        public IReservationRepository repo;

        public ReservationMaker(IReservationRepository repo)
            {
            this.repo = repo;
            }

        public List<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS> MakeButtonList(DateTime requestedDay, BoatEntity boat)
            {
            List<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS> result = new List<IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS> ();
            List<ReservationEntity> huh = repo.GetByBoatIDAndDay(boat, requestedDay);
            if (huh.Count == 0)
                {
                DateTime temp = new DateTime();
                temp = temp.AddDays(requestedDay.DayOfYear - 2);
                temp = temp.AddYears(requestedDay.Year - 1);

                DateTime temp2 = new DateTime();
                temp2 = temp;
                temp = temp.AddHours(ReservationValidator.Morning.Hour);
                temp = temp.AddMinutes(ReservationValidator.Morning.Minute);

                temp2 = temp2.AddHours(ReservationValidator.Evening.Hour);
                temp2 = temp2.AddMinutes(ReservationValidator.Evening.Minute);

                result.Add(new IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(temp, temp2));
                }
            else
                {
                DateTime temp = new DateTime();
                temp = temp.AddDays(requestedDay.DayOfYear-2);
                temp = temp.AddYears(requestedDay.Year-1);
                DateTime temp2 = new DateTime();
                temp2 = temp;
                temp = temp.AddHours(ReservationValidator.Morning.Hour);
                temp = temp.AddMinutes(ReservationValidator.Morning.Minute);
                DateTime temp3 = new DateTime();
                temp3 = temp2;
                temp3 = temp3.AddHours(ReservationValidator.Evening.Hour);
                temp3 = temp3.AddMinutes(ReservationValidator.Evening.Minute);


                for (int i = 0; i < huh.Count; i++)
                    {
                    temp2 = huh[i].StartTime;

                    result.Add(new IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(temp, temp2));

                    temp = huh[i].EndTime;
                    }


                result.Add(new IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(temp, temp3));

                }
            return result;
            }
        }
    public class IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS
        {

        public IHAVENOIDEAWHATIAMDOINGBUTTHISISFORBUTTONS(DateTime startTime, DateTime endTime)
            {
            this.endTime = endTime;
            this.startTime = startTime;
            if (endTime.Minute - startTime.Minute == 30)
                {
                length += 0.5;
                }
            else if (endTime.Minute - startTime.Minute == -30)
                { 
                length -= 0.5;
                }

            length += endTime.Hour - startTime.Hour;
            }


        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public double length { get; set; }
        }
    }
