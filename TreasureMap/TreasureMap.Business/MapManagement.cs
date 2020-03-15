using System;
using System.Collections.Generic;
using System.Linq;
using TreasureMap.Data;

namespace TreasureMap.Business
{
    public static class MapManagement
    {
        /// <summary>
        /// Créer la carte avec les lignes extraites du fichier d'entrée
        /// </summary>
        /// <param name="dataLines">Lignes extraites du fichier d'entrée</param>
        /// <returns></returns>
        public static Map CreateMapWithInitialData(List<string> dataLines)
        {
            Map map = new Map();
            string mapSizeDataLine = dataLines?.FirstOrDefault(l => l.StartsWith("C"));
            map.InitializeCells(mapSizeDataLine);
            List<string> mountainDataLines = dataLines?.FindAll(l => l.StartsWith("M"));
            map?.InitializeMountains(mountainDataLines);
            List<string> treasureDataLines = dataLines?.FindAll(l => l.StartsWith("T"));
            map?.InitializeTreasures(treasureDataLines);
            List<string> adventurerDataLines = dataLines?.FindAll(l => l.StartsWith("A"));
            map?.InitializeAdventurers(adventurerDataLines);
            return map;
        }

        /// <summary>
        /// Simuler la recherche des trésors par l'ensemble des aventurier sur la carte
        /// </summary>
        /// <param name="map">Carte au trésors</param>
        public static void SimulateTreasureSearch(Map map)
        {
            List<Adventurer> adventurerWithMovesToProcess = map?.Adventurers.FindAll(a => !string.IsNullOrEmpty(a.MovesSequence));
            while (adventurerWithMovesToProcess?.Count > 0)
            {
                foreach (Adventurer adventurer in adventurerWithMovesToProcess)
                {
                    char move = AdventurerManagement.GetCurrentMoveToProcess(adventurer);
                    AdventurerManagement.ProcessAdventurerMove(adventurer, map, move);
                    AdventurerManagement.RemoveProcessedMove(adventurer);
                }
                adventurerWithMovesToProcess = map?.Adventurers.FindAll(a => !string.IsNullOrEmpty(a.MovesSequence));
            }
        }

        /// <summary>
        /// Traiter le fichier d'entrée
        /// </summary>
        /// <param name="mapFilePath">Chemin du fichier d'entrée</param>
        public static string Process(string mapFilePath)
        {
            List<string> dataLines = FileManagement.GetDataLinesFromFile(mapFilePath);
            Map map = CreateMapWithInitialData(dataLines);
            SimulateTreasureSearch(map);
            string resultFilePath = mapFilePath.Replace(".txt", "_results.txt");
            return FileManagement.SaveSimulation(map, resultFilePath);
        }
    }
}
