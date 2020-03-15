using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Data
{
    public static class Messages
    {
        public const string NoMapSizeError = @"- La ligne contenant les dimensions de la Carte est invalide ";
        public const string NoTreasureError = @"- Le fichier ne contient aucune ligne Trésor ";
        public const string NoAdventurerError = @"- Le fichier ne contient aucune ligne Aventurier ";
        public const string FileTypeError = @"Le fichier d'entrée doit être une fichier .txt";

        public const string FileSavedOK = @"Le fichier contenant les résultats de simulation se trouve dans : ";
        public const string FileToSaveKO = @"Il y a eu un problème lors de l'écriture du fichier contenant les résultats de simulation : ";

        public const string EnterMapFilePath = @"Veuillez saisir un chemin valide pour La carte aux trésors: ";
    }
}
