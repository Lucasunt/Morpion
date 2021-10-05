using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    class Translations
    {
        public static Dictionary<string, string[]> Get = new Dictionary<string, string[]>
        {
            { "mWTitle", new string[]{ "Jeu du morpion", "Nullen und Kreuze Spiel" } },
            { "lblPlayer1", new string[]{ "A joueur 1 de jouer", "Spieler 1 zu spielen" } },
            { "lblPlayer2", new string[]{ "A joueur 2 de jouer", "Spieler 2 zu spielen" } },
            { "btComputer", new string[]{ "Jouer contre l'ordinateur", "Gegen den Computer spielen" } },
            { "btRestart", new string[]{ "Recommencer", "Neu starten" } },
            { "btLang", new string[]{ "Deutsch", "Français" } },
            { "msgPlayer1Won", new string[]{ "Joueur 1 a gagné!", "Spieler 1 hat gewonnen!" } },
            { "msgPlayer2Won", new string[]{ "Joueur 2 a gagné!", "Spieler 2 hat gewonnen!" } },
            { "msgDraw", new string[]{ "Match nul, commencer une nouvelle partie?", "Unentschieden, ein neues Spiel starten?" } },
            { "msgDrawInfo", new string[]{ "Question", "Frage" } },
        };
    }
}
