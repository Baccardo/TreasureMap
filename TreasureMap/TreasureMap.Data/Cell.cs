namespace TreasureMap.Data
{
    public class Cell
    {
        /// <summary>
        /// Tyype de la case : neutre, montagne, trésor
        /// </summary>
        public CellType Type { get; set; }
        /// <summary>
        /// Axe horizontal
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Axe vertical
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Nb. de trésors
        /// </summary>
        public int TreasureAmount { get; set; } //Nb.de trésors

        /// <summary>
        /// Décrémenter le Nb. de trésors
        /// </summary>
        public void DecrementTreasure()
        {
            TreasureAmount--;
        }
    }
}
