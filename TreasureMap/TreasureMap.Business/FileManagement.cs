using System;
using System.Collections.Generic;
using System.IO;
using TreasureMap.Data;

namespace TreasureMap.Business
{
    public static class FileManagement
    {
        /// <summary>
        /// Extraire les lignes de données à partir du fichier d'entrée
        /// </summary>
        /// <param name="filePath">Chemin du fichier d'entrée</param>
        /// <returns></returns>
        public static List<string> GetDataLinesFromFile(string filePath)
        {
            List<string> dataLines = new List<string>();
            string line;
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (!line.StartsWith("#"))
                    dataLines.Add(line);
            }
            return dataLines;
        }

        /// <summary>
        /// Vérifier l'intégralité des données du fichier d'entrée
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static (bool, string) DataLinesAreValid(string filePath)
        {
            bool check = true;
            string msg = string.Empty;
            if (!filePath.EndsWith(".txt"))
            {
                check = false;
                msg = Messages.FileTypeError;
            }
            else
            {
                List<string> dataLines = GetDataLinesFromFile(filePath);
                List<string> mapLines = dataLines.FindAll(l => l.StartsWith("C"));
                if (mapLines?.Count != 1)
                {
                    check = false;
                    msg += Messages.NoMapSizeError + System.Environment.NewLine;
                }
                List<string> treasureLines = dataLines.FindAll(l => l.StartsWith("T"));
                if (treasureLines?.Count == 0)
                {
                    check = false;
                    msg += Messages.NoTreasureError + System.Environment.NewLine;
                }
                List<string> adventurerLines = dataLines.FindAll(l => l.StartsWith("A"));
                if (adventurerLines?.Count == 0)
                {
                    check = false;
                    msg += Messages.NoAdventurerError;
                }
            }
            return (check, msg);
        }

        /// <summary>
        /// Sauvegarder le résultat final de la simulation dans le fichier de sortie 
        /// </summary>
        /// <param name="map">Carte au trèsors après simulation</param>
        /// <param name="resultFilePath">Chemin du fichier de sortie à écrire</param>
        public static string SaveSimulation(Map map, string resultFilePath)
        {
            List<string> linesToSave = new List<string>
            {
                "# {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur}",
                "C - " + map.Width + " - " + map.Height
            };
            List<Cell> mountainCells = map?.Cells.FindAll(c => c.Type == CellType.Mountain);
            linesToSave.Add("# {M comme Montagne} - {Axe horizontal} - {Axe vertical}");
            foreach (Cell mountain in mountainCells)
            {
                linesToSave.Add("M - " + mountain.X + " - " + mountain.Y);
            }
            List<Cell> treasureCells = map?.Cells.FindAll(c => c.Type == CellType.Treasure && c.TreasureAmount > 0);
            linesToSave.Add("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}");
            foreach (Cell treasure in treasureCells)
            {
                linesToSave.Add("T - " + treasure.X + " - " + treasure.Y + " - " + treasure.TreasureAmount);
            }
            linesToSave.Add("# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb.trésors ramassés}");
            foreach (Adventurer adventurer in map?.Adventurers)
            {
                linesToSave.Add("A - " + adventurer.Name + " - " + adventurer.X + " - " + adventurer.Y 
                            + " - " + adventurer.Orientation + " - " + adventurer.CollectedTreasure);
            }
            string msgToReturn = Messages.FileSavedOK + resultFilePath;
            try
            {
                File.WriteAllLines(resultFilePath, linesToSave);
            }
            catch (Exception ex)
            {
                msgToReturn = Messages.FileToSaveKO + ex.Message;
            }
            return msgToReturn;
        }
    }
}
