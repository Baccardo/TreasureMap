using System;
using System.Collections.Generic;
using System.Linq;

namespace TreasureMap.Data
{
    public class Map
    {
        /// <summary>
        /// Nb. de case en hauteur
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Nb. de case en largeur
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Ensemble des cases formant la carte
        /// </summary>
        public List<Cell> Cells { get; set; }
        /// <summary>
        /// Ensemble des aventuriers présents sur la carte
        /// </summary>
        public List<Adventurer> Adventurers { get; set; }

        /// <summary>
        /// Initialise l'ensemble des cases de la carte
        /// </summary>
        /// <param name="cellsDataLine">Ligne dimension de la carte</param>
        public void InitializeCells(string cellsDataLine)
        {
            if(!string.IsNullOrEmpty(cellsDataLine))
            {
                string[] cellsData = cellsDataLine.Split('-');
                int.TryParse(cellsData[1].Trim(), out int width);
                int.TryParse(cellsData[2].Trim(), out int height);
                Width = width;
                Height = height;
                if (Width > 0 && Height > 0)
                {
                    List<Cell> cells = new List<Cell>();
                    for (int i = 0; i < Width; i++)
                    {
                        for (int j = 0; j < Height; j++)
                        {
                            cells.Add(new Cell { Type = CellType.Neutral, X = i, Y = j });
                        }
                    }
                    Cells = cells;
                }
            }
        }

        /// <summary>
        /// Initialise les montagnes présentes sur la carte
        /// </summary>
        /// <param name="mountainDataLines">Lignes montagnes</param>
        public void InitializeMountains(List<string> mountainDataLines)
        {
            foreach (string mountainLine in mountainDataLines ?? Enumerable.Empty<string>())
            {
                string[] mountainData = mountainLine.Split('-');
                if (mountainData.Length >= 3
                    && int.TryParse(mountainData[1].Trim(), out int x)
                    && int.TryParse(mountainData[2].Trim(), out int y))
                {
                    Cell cell = Cells?.FirstOrDefault(c => c.X == x && c.Y == y);
                    if(cell != null)
                        cell.Type = CellType.Mountain;
                }
            }
        }

        /// <summary>
        /// Initialise les trésors présents sur la carte
        /// </summary>
        /// <param name="treasureDataLines">Lignes trésors</param>
        public void InitializeTreasures(List<string> treasureDataLines)
        {
            foreach (string treasureLine in treasureDataLines ?? Enumerable.Empty<string>())
            {
                string[] treasureData = treasureLine.Split('-');
                if (treasureData.Length >= 3
                    && int.TryParse(treasureData[1].Trim(), out int x)
                    && int.TryParse(treasureData[2].Trim(), out int y))
                {
                    Cell cell = Cells?.FirstOrDefault(c => c.X == x && c.Y == y);
                    if (cell != null)
                    {
                        cell.Type = CellType.Treasure;
                        if (treasureData.Length >= 4 && int.TryParse(treasureData[3].Trim(), out int amount))
                        {
                            cell.TreasureAmount = amount;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initialise les aventuriers présents sur la carte
        /// </summary>
        /// <param name="adventurerDataLines">Lignes aventuriers</param>
        public void InitializeAdventurers(List<string> adventurerDataLines)
        {
            if (adventurerDataLines?.Count > 0)
            {
                Adventurers = new List<Adventurer>();
                foreach (string adventurerLine in adventurerDataLines)
                {
                    string[] adventurerData = adventurerLine.Split('-');
                    Adventurer adventurer = new Adventurer();
                    if (adventurerData.Length >= 6
                        && int.TryParse(adventurerData[2].Trim(), out int x)
                        && int.TryParse(adventurerData[3].Trim(), out int y))
                    {
                        adventurer.Name = adventurerData[1].Trim(); //Nom de l’aventurier
                        adventurer.X = x; //Axe horizontal
                        adventurer.Y = y; //Axe vertical
                        adventurer.Orientation = (Orientation)Enum.Parse(typeof(Orientation), adventurerData[4].Trim()); //Orientation
                        adventurer.MovesSequence = adventurerData[5].Trim(); //Séquence de mouvement
                    }
                    Adventurers.Add(adventurer);
                }
            }
        }
    }
}
