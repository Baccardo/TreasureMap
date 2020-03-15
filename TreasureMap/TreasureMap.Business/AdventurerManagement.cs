using System.Linq;
using TreasureMap.Data;

namespace TreasureMap.Business
{
    public static class AdventurerManagement
    {
        /// <summary>
        /// Vérifier si l'aventurier peut avancer
        /// </summary>
        /// <param name="adventurer">Aventurier</param>
        /// <param name="map">Carte aux trésors</param>
        /// <returns>Retourne True si l'aventurier peut avancer, sinon False</returns>
        public static bool AdventurerCanMoveForward(Adventurer adventurer, Map map)
        {
            int newX = -1, newY = -1;
            switch (adventurer.Orientation)
            {
                case Orientation.E:
                    newX = adventurer.X + 1;
                    newY = adventurer.Y;
                    break;
                case Orientation.N:
                    newX = adventurer.X;
                    newY = adventurer.Y - 1;
                    break;
                case Orientation.O:
                    newX = adventurer.X - 1;
                    newY = adventurer.Y;
                    break;
                case Orientation.S:
                    newX = adventurer.X;
                    newY = adventurer.Y + 1;
                    break;
            }
            if (newX < map?.Width && newX >= 0 && newY < map?.Height && newY >= 0)
            {
                Cell newCell = map?.Cells.FirstOrDefault(c => c.X == newX && c.Y == newY);
                if (newCell.Type != CellType.Mountain)
                {
                    Adventurer otherAdventurer = map?.Adventurers.FirstOrDefault(a => a.X == newX && a.Y == newY);
                    if (otherAdventurer == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Obtenir le movement à traiter à partir de la séquence de mouvements de l'aventurier
        /// </summary>
        /// <param name="adventurer">Aventurier</param>
        /// <returns>Retourne la lettre désignant le mouvement à traiter</returns>
        public static char GetCurrentMoveToProcess(Adventurer adventurer)
        {
            return !string.IsNullOrEmpty(adventurer?.MovesSequence) ? (char)adventurer?.MovesSequence[0] : '-';
        }

        /// <summary>
        /// Supprimer le dernier mouvement traité de la séquence de mouvements de l'aventurier
        /// </summary>
        /// <param name="adventurer">Aventurier</param>
        public static void RemoveProcessedMove(Adventurer adventurer)
        {
            if (!string.IsNullOrEmpty(adventurer?.MovesSequence))
                adventurer.MovesSequence = adventurer?.MovesSequence?.Substring(1);
        }

        /// <summary>
        /// Effectuer le mouvement d'un aventurier sur la carte
        /// </summary>
        /// <param name="adventurer">Aventurier</param>
        /// <param name="map">Carte aux trésors</param>
        /// <param name="moveToProcess">Mouvement à traiter</param>
        public static void ProcessAdventurerMove(Adventurer adventurer, Map map, char moveToProcess)
        {
            switch (moveToProcess)
            {
                case 'A':
                    if (AdventurerCanMoveForward(adventurer, map))
                    {
                        adventurer.MoveForward();
                        Cell currentCell = map?.Cells.FirstOrDefault(c => c.X == adventurer.X && c.Y == adventurer.Y);
                        if(currentCell?.Type == CellType.Treasure)
                        {
                            adventurer.PickTreasure();
                            currentCell.DecrementTreasure();
                        }
                    }
                    break;
                case 'D':
                    adventurer.TurnRight();
                    break;
                case 'G':
                    adventurer.TurnLeft();
                    break;
            }
        }
    }
}
