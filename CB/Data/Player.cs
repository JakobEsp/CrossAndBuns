using System.Net;
using System.Net.Sockets;

namespace CB.Data
{
    public class Player
    {
        public List<IPEndPoint> UserList { get; set; } = new List<IPEndPoint>();
        public int id { get; set; }
        public string print { get; set; }

        private int _id = 0;

        public Player()
        {
            id = Interlocked.Increment(ref _id);
            if (id == 1)
            {
                print = "X";
            }
            else
            {
                print = "O";
            }
        }
    }
}