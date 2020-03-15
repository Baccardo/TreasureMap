namespace TreasureMap.Data
{
    public class Adventurer
    {
        /// <summary>
        /// Nom de l’aventurier
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Axe horizontal de l’aventurier
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Axe vertical de l’aventurier
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Orientation de l’aventurier
        /// </summary>
        public Orientation Orientation { get; set; }
        /// <summary>
        /// Séquence de mouvement de l'aventurier
        /// </summary>
        public string MovesSequence { get; set; }
        /// <summary>
        /// Nb. trésors ramassés par l'aventurier
        /// </summary>
        public int CollectedTreasure { get; set; }

        /// <summary>
        /// Collecter un trésor
        /// </summary>
        public void PickTreasure()
        {
            CollectedTreasure++;
        }

        /// <summary>
        /// Avancer dans la direction actuelle de l'aventurier
        /// </summary>
        public void MoveForward()
        {
            switch (Orientation)
            {
                case Orientation.E :
                    X += 1;
                    break;
                case Orientation.N:
                    Y -= 1;
                    break;
                case Orientation.O:
                    X -= 1;
                    break;
                case Orientation.S:
                    Y += 1;
                    break;
            }
        }

        /// <summary>
        /// Tourner à droite
        /// </summary>
        public void TurnRight()
        {
            switch (Orientation)
            {
                case Orientation.E:
                    Orientation = Orientation.S;
                    break;
                case Orientation.N:
                    Orientation = Orientation.E;
                    break;
                case Orientation.O:
                    Orientation = Orientation.N;
                    break;
                case Orientation.S:
                    Orientation = Orientation.O;
                    break;
            }
        }

        /// <summary>
        /// Tourner à gauche
        /// </summary>
        public void TurnLeft()
        {
            switch (Orientation)
            {
                case Orientation.E:
                    Orientation = Orientation.N;
                    break;
                case Orientation.N:
                    Orientation = Orientation.O;
                    break;
                case Orientation.O:
                    Orientation = Orientation.S;
                    break;
                case Orientation.S:
                    Orientation = Orientation.E;
                    break;
            }
        }
    }
}
