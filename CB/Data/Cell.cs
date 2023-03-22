namespace CB.Data
{
    public class CellBox
    {
        public int? Id { get; set; }
        public bool set { get; set; } = false;
        public string? Value { get; set; } = "X";

        public Player? Player { get; set; } = new();

        private int _id;

        public CellBox()
        {
            Id = Interlocked.Increment(ref _id);
        }

        public CellBox(int id, bool set)
        {
            Id = id;
            if (Player.print == null) Value = "";
            else Value = Player.print;

            this.set = set;
        }


    }
}