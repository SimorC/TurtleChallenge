namespace TurtleChallenge.Domain.Model
{
    public class Tile
    {
        public Coordinate Coordinate { get; set; }
        public GameObject CurrentObject { get; set; }

        public Tile(Coordinate coordinate, GameObject gameObject)
        {
            this.Coordinate = coordinate;
            this.CurrentObject = gameObject;
        }

        public Tile(int posX, int posY, GameObject gameObject)
        {
            this.Coordinate = new Coordinate(posX, posY);
            this.CurrentObject = gameObject;
        }
    }
}