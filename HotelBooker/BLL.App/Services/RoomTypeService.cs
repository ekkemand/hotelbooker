using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.HelperClasses;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RoomTypeService :
        BaseEntityService<IAppUnitOfWork, IRoomTypeRepository, IRoomTypeServiceMapper, DAL.App.DTO.RoomType,
            BLL.App.DTO.RoomType>, IRoomTypeService
    {
        public RoomTypeService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.RoomTypes, new RoomTypeServiceMapper())
        {
        }

        private List<Reservation> GetTimePointReservations(DateTime time, List<Reservation> reservations)
        {
            var result = new List<Reservation>();
            foreach (var reservation in reservations)
            {
                var startDateResult = DateTime.Compare(reservation.StartDateTime, time);
                var endDateResult = DateTime.Compare(reservation.EndDateTime, time);
                // Reservation's start date is earlier than today's date && end date is later than today's date
                if (startDateResult <= 0 && endDateResult >= 0)
                {
                    result.Add(reservation);
                }
            }

            return result;
        }

        private DateTime GetEarliestEndingReservationTime(List<Reservation> reservations)
        {
            var items = reservations.OrderBy(o => o.EndDateTime);
            var result = items.FirstOrDefault().EndDateTime;
            return items.FirstOrDefault().EndDateTime;
        }

        private int GetTimePointRoomsTaken(List<Reservation> reservations, DateTime time)
        {
            var result = 0;
            foreach (var reservation in reservations)
            {
                if (reservation.EndDateTime != time)
                {
                    result += reservation.NumberOfRooms;
                }
            }

            return result;
        }

        public async Task<RoomTypeAdditionalData> GetEarliestReservationStartDate(Guid roomTypeId)
        {
            var roomType = await FirstOrDefaultAsync(roomTypeId);
            var totalRooms = roomType.Rooms!.Count;
            Console.WriteLine(totalRooms);
            var totalReservations = roomType.Reservations!;
            var totalReservationsCount = totalReservations.Count;
            var result = new RoomTypeAdditionalData
            {
                RoomType = roomType
            };

            if (totalRooms == 0)
            {
                result.AvailableRooms = 0;
                return result;
            }

            if (totalReservationsCount == 0)
            {
                result.EarliestReservation = DateTime.Today;
                result.AvailableRooms = totalRooms;
                return result;
            }

            var earliestEnding = DateTime.Today;

            while (true)
            {
                var reservationsAtTime = GetTimePointReservations(earliestEnding, totalReservations.ToList());
                var roomsTakenAtTime = GetTimePointRoomsTaken(reservationsAtTime, earliestEnding);
                if (totalRooms > roomsTakenAtTime)
                {
                    result.EarliestReservation = earliestEnding;
                    result.AvailableRooms = totalRooms - roomsTakenAtTime;
                    return result;
                }

                earliestEnding = GetEarliestEndingReservationTime(reservationsAtTime);
                totalReservations.Remove(totalReservations
                    .FirstOrDefault(o => o.EndDateTime == earliestEnding));
            }
        }
    }
}