using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicketBookingApp
{
    public static class TicketManager
    {
        // Trạng thái ghế: 0 = trống, 1 = đã đặt
        private static Dictionary<string, int> Seats = new Dictionary<string, int>();

        // Object khóa để đồng bộ đa luồng
        public static readonly object LockObject = new object();

        // Khởi tạo danh sách ghế
        public static void LoadSeats()
        {
            lock (LockObject)
            {
                Seats.Clear();
                string[] rows = { "A", "B", "C" };

                foreach (string row in rows)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        string seatId = row + i;
                        Seats[seatId] = 0; // 0 = trống
                    }
                }
            }
        }

        // Lấy trạng thái ghế theo dạng: A1=0;A2=1;A3=0;
        public static string GetSeatStatusString()
        {
            lock (LockObject)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var kv in Seats)
                {
                    sb.Append($"{kv.Key}={kv.Value};");
                }

                return sb.ToString();
            }
        }

        // Đặt ghế
        public static string BookSeat(string seat)
        {
            lock (LockObject)
            {
                if (!Seats.ContainsKey(seat))
                    return "ERROR|SeatNotFound";

                if (Seats[seat] == 1)
                    return "ERROR|SeatBooked";

                Seats[seat] = 1;
                return "OK|Booked";
            }
        }

        // Hủy ghế
        public static string CancelSeat(string seat)
        {
            lock (LockObject)
            {
                if (!Seats.ContainsKey(seat))
                    return "ERROR|SeatNotFound";

                if (Seats[seat] == 0)
                    return "ERROR|SeatEmpty";

                Seats[seat] = 0;
                return "OK|Canceled";
            }
        }
    }
}
