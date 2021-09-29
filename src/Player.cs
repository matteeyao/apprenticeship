namespace src
{
    public abstract class Player
    {
        private string marker;

        public string GetMarker()
        {
            return marker;
        }

        public abstract int Move(Game game, string marker);
    }
}