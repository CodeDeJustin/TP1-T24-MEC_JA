/**********************************************************************
 ************ PROGRAMME - CRÉATION DE COMBINAISONS DE 6/49 ************
 **********************************************************************
 *
 * Description : Ce programme demande à l'utilisateur un nombre de
 * combinaisons de 6/49 entre 10 et 200, tire au hasard les
 * combinaisons de 6/49, les chiffres dans chaque combinaison doivent
 * être sans répétition, avec un numéro complémentaire et doit afficher
 * la combinaison dans l'ordre (le numéro complémentaire ne doit pas
 * être dans l'ordre).
 * 
 * Par la suite, une combinaison gagnante avec le chiffre complémentaire
 * sera généré
 * 
 * Indiquer les combinaisons gagnantes de l'utilisateur.
 * 
 * À la fin du programme, il y aura :
 * - des statistiques indiquant le nombre de tirages
 * - le nombre d'apparition de chaque nombre à l'intérieur des
 *   combinaisons gagnantes
 * - Le pourcentage de combinaisons pour chaque famille de résultat
 * 
 * Fait par:    Marie-Ève couture
 * Fait par:    Justin Allard
 * Le :         2023-01-12
 * Révisé par :
 * Révisé le :
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T24_TP1_MEC_JA
{
    // CRÉATION DE LA STRUCTURE
    struct Combi_Gagnante_Perdante
    {
        public int[] Combi_Perdantes;
        public int[] Combi_3_6;
        public int[] Combi_3_6_C;
        public int[] Combi_4_6;
        public int[] Combi_4_6_C;
        public int[] Combi_5_6;
        public int[] Combi_5_6_C;
        public int[] Combi_Gagnante;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // MESSAGE D'ACCUEIL
            Message_Accueil();

            int nombre_tirage = 0;
            int[] comparaison = new int[49];
            do
            {
                // SECTION UTILISATEUR
                int nombre_combinaisons = Demander_Nombre_Combinaisons(10, 200);
                int[,] Combinaison_Utilisateur = new int[nombre_combinaisons, 7];
                int[,] tableau_generer_utilisateur = Generer_Combinaisons_Utilisateur(Combinaison_Utilisateur, nombre_combinaisons);
                int[,] tableau_trier_utilisateur = Trier_Combinaison_Utilisateur(tableau_generer_utilisateur);
                int[,] combinaison_finale_utilisateur = Afficher_Tableau_Utilisateur(tableau_trier_utilisateur);

                // SECTION GÉNÉRATEUR DE LOTERIE
                int[] combinaison_gagnante = Generer_Combinaison_Gagnante();
                int[] combinaison_trier_gagnante = Trier_Combinaison_Gagnante(combinaison_gagnante);
                int[] combinaison_finale_gagnante = Afficher_Combinaison_Gagnante(combinaison_trier_gagnante);

                foreach (int numeroGagnant in combinaison_gagnante)
                {
                    comparaison[numeroGagnant - 1]++;
                }
                nombre_tirage++;

                // CLASSEMENT DES COMBINAISONS DANS LES BONNES FAMILLES
                Combi_Gagnante_Perdante tirage = new Combi_Gagnante_Perdante()
                {
                    Combi_Perdantes = Array.Empty<int>(),
                    Combi_3_6 = Array.Empty<int>(),
                    Combi_3_6_C = Array.Empty<int>(),
                    Combi_4_6 = Array.Empty<int>(),
                    Combi_4_6_C = Array.Empty<int>(),
                    Combi_5_6 = Array.Empty<int>(),
                    Combi_5_6_C = Array.Empty<int>(),
                    Combi_Gagnante = Array.Empty<int>()
                };
                for (int i = 0; i < combinaison_finale_utilisateur.GetLength(0); i++)
                {
                    int nbGagnant = 0;
                    bool avecComplementaire = false;
                    for (int j = 0; j < combinaison_finale_utilisateur.GetLength(1); j++)
                    {
                        if (Array.IndexOf(combinaison_finale_gagnante, combinaison_finale_utilisateur[i, j]) >= 0)
                        {
                            nbGagnant++;
                        }
                    }
                    switch (nbGagnant)
                    {
                        case 0:
                        case 1:
                        case 2:
                            tirage.Combi_Perdantes = tirage.Combi_Perdantes.Append(i).ToArray();
                            break;
                        case 3:
                            if (avecComplementaire)
                                tirage.Combi_3_6_C = tirage.Combi_3_6_C.Append(i).ToArray();
                            else
                                tirage.Combi_3_6 = tirage.Combi_3_6.Append(i).ToArray();
                            break;
                        case 4:
                            if (avecComplementaire)
                                tirage.Combi_4_6_C = tirage.Combi_4_6_C.Append(i).ToArray();
                            else
                                tirage.Combi_4_6 = tirage.Combi_4_6.Append(i).ToArray();
                            break;
                        case 5:
                            if (avecComplementaire)
                                tirage.Combi_5_6_C = tirage.Combi_5_6_C.Append(i).ToArray();
                            else
                                tirage.Combi_5_6 = tirage.Combi_5_6.Append(i).ToArray();
                            break;
                        case 6:
                            tirage.Combi_Gagnante = tirage.Combi_Gagnante.Append(i).ToArray();
                            break;
                    }
                }

                // STATISTIQUES DES FAMILLES
                AfficherCombiConcordantes("3 sur 6", combinaison_finale_utilisateur, tirage.Combi_3_6, nombre_combinaisons);
                AfficherCombiConcordantes("3 sur 6 avec no complémentaire", combinaison_finale_utilisateur, tirage.Combi_3_6_C, nombre_combinaisons);
                AfficherCombiConcordantes("4 sur 6", combinaison_finale_utilisateur, tirage.Combi_4_6, nombre_combinaisons);
                AfficherCombiConcordantes("4 sur 6 avec no complémentaire", combinaison_finale_utilisateur, tirage.Combi_4_6_C, nombre_combinaisons);
                AfficherCombiConcordantes("5 sur 6", combinaison_finale_utilisateur, tirage.Combi_5_6, nombre_combinaisons);
                AfficherCombiConcordantes("5 sur 6 avec no complémentaire", combinaison_finale_utilisateur, tirage.Combi_5_6_C, nombre_combinaisons);
                AfficherCombiConcordantes("gagnantes", combinaison_finale_utilisateur, tirage.Combi_Gagnante, nombre_combinaisons);

            } while (Recommencer());

            // MESSAGE DU NOMBRE DE TIRAGES
            Console.ResetColor();
            Console.Write("Le nombre de tirage est de {0}", nombre_tirage);

            // COMPARAISON DES NOMBRES GAGNANTS
            Comparaison_Nombre_Gagnant(comparaison);

            // MESSAGE DE FIN DE PROGRAMME
            Message_Fin_Programme();
        }

        #region 1 - MESSAGE D'ACCUEIL
        /// <summary>
        /// Procédure Message d'accueil à l'utilisateur
        /// </summary>

        public static void Message_Accueil()
        {
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("BIENVENUE SUR LE GÉNÉRATEUR DE NUMÉROS DE LOTERIE !");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Veuillez appuyer sur ENTRÉE à chaque étape afin de saisir les informations.");
        }

        #endregion

        #region 2 - DEMANDER NOMBRE COMBINAISONS UTILISATEUR
        /// <summary>
        /// Fonction permettant à l'utilisateur de déterminer le nombre de combinaisons voulues, entre 10 et 200
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>Un integer du nombre de combinaisons</returns>

        public static int Demander_Nombre_Combinaisons(int min, int max)
        {
            // DEMANDER À L'UTILISATEUR LE NOMBRE DE COMBINAISONS
            int nombre_combinaisons;

            while (true)
            {
                Console.SetCursorPosition(0, 5);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Veuillez entrer le nombre de combinaisons désiré (entre 10 et 200): ");
                int.TryParse(Console.ReadLine(), out nombre_combinaisons);
                Console.WriteLine();

                if (nombre_combinaisons >= min && nombre_combinaisons <= max)
                    return nombre_combinaisons;
                else
                    Console.WriteLine("Attention! Vous devez entrer un nombre de combinaisons entre 10 et 200 !");
            }
        }

        #endregion

        #region 3 - GÉNÉRER COMBINAISON UTILISATEUR
        /// <summary>
        /// Procédures permettant de demander à l'utilisateur le nombre de combinaisons voulues, de trier et d'afficher ces combinaisons
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="max"></param>

        public static int[,] Generer_Combinaisons_Utilisateur(int[,] tableau, int max)
        {
            // GÉNÉRER LES COMBINAISONS DE L'UTILISATEUR
            Random hasard = new Random();
            bool[] numero_sorti = new bool[49];
            int numero = 0;

            for (int i = 0; i <= tableau.GetUpperBound(0); i++)
            {
                Array.Clear(numero_sorti, 0, numero_sorti.Length);
                for (int j = 0; j <= tableau.GetUpperBound(1); j++)
                {
                    do
                    {
                        numero = hasard.Next(49);
                    } while (numero_sorti[numero] == true);

                    numero_sorti[numero] = true;
                    tableau[i, j] = numero + 1;
                }
            }
            return tableau;
        }

        public static int[,] Trier_Combinaison_Utilisateur(int[,] tableau)
        {
            // TRIER LES COMBINAISONS DE L'UTILISATEUR
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = tableau.GetLength(1) - 1; j > 0; j--)
                {
                    for (int k = 0; k < j - 1; k++)
                    {
                        if (tableau[i, k] > tableau[i, k + 1])
                        {
                            int temporaire = tableau[i, k];
                            tableau[i, k] = tableau[i, k + 1];
                            tableau[i, k + 1] = temporaire;
                        }
                    }
                }
            }
            return tableau;
        }

        public static int[,] Afficher_Tableau_Utilisateur(int[,] tableau)
        {
            // AFFICHER LA COMBINAISON
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i <= tableau.GetUpperBound(0); i++)
            {
                Console.Write($"Votre combinaison #{i + 1}:  ");
                for (int j = 0; j < tableau.GetUpperBound(1); j++)
                {
                    Console.Write(tableau[i, j].ToString().PadLeft(2, '0'));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

            return tableau;
        }
        #endregion

        #region 4 - TIRAGE DU NUMÉRO GAGNANT
        /// <summary>
        /// Fonctions permettant de générer, de trier et d'afficher un numéro gagnant avec le complémentaire (Complémentaire non trié)
        /// </summary>
        /// <returns>La combinaison gagnante</returns>

        public static int[] Generer_Combinaison_Gagnante()
        {
            // CRÉATION DU TABLEAU
            int[] combinaison = new int[7];
            Random hasard = new Random();
            bool[] numero_sorti = new bool[49];
            int numero;

            // RÉINITIALISATION DU TABLEAU
            Array.Clear(numero_sorti, 0, numero_sorti.Length);

            // TIRAGE AU HASARD DES NUMÉROS
            for (int i = 0; i < combinaison.Length; i++)
            {
                do
                {
                    numero = hasard.Next(49);

                } while (numero_sorti[numero] == true);

                numero_sorti[numero] = true;
                combinaison[i] = numero + 1;
            }
            return combinaison;
        }

        public static int[] Trier_Combinaison_Gagnante(int[] combinaison)
        {
            // TRIER LA COMBINAISON DE 6 CHIFFRES SANS TRIER LE COMPLÉMENTAIRE
            int tempo;
            for (int i = 0; i < combinaison.Length - 1; i++)
            {
                for (int j = i + 1; j < combinaison.Length - 1; j++)
                {
                    if (combinaison[i] > combinaison[j])
                    {
                        tempo = combinaison[i];
                        combinaison[i] = combinaison[j];
                        combinaison[j] = tempo;
                    }
                }
            }
            return combinaison;
        }

        public static int[] Afficher_Combinaison_Gagnante(int[] combinaison)
        {
            // AFFICHER LA COMBINAISON GAGNANTE
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("La combinaison ");
            Console.Write("gagnante est : \t\t");

            for (int i = 0; i < combinaison.Length; i++)
            {
                if (i == 6)
                {
                    Console.Write("    Complémentaire: ");
                }
                Console.Write(combinaison[i].ToString().PadLeft(2, '0'));
                Console.Write(" ");
            }

            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();

            return combinaison;
        }

        #endregion

        #region 5 - STATISTIQUES
        /// <summary>
        /// Procédure permettant de comparer et d'afficher les statistiques
        /// </summary>
        /// <param name="combinaison_gagnante"></param>

        public static void Comparaison_Nombre_Gagnant(int[] combinaison_gagnante)
        {
            Console.Clear();
            Console.WriteLine("Le nombre de numéros sortis par combinaison: ");
            Console.WriteLine();

            for (int i = 0; i < 49; i++)
            {
                int nb_bon = combinaison_gagnante[i];

                if (nb_bon == 0)
                {
                    continue;
                }
                int numero = i + 1;

                Console.WriteLine($"Le numéro {numero} est sorti {nb_bon} fois.");
            }

        }

        #endregion

        #region 6 - CLASSE DES COMBINAISONS
        /// <summary>
        /// Procédure permettant de comparer les classes de combinaisons selon les tirages
        /// </summary>
        /// <param name="ratioCombinaison"></param>
        /// <param name="tableau"></param>
        /// <param name="win_Lose"></param>
        /// <param name="nbrCombinaisons"></param>

        static void AfficherCombiConcordantes(string ratioCombinaison, int[,] tableau, int[] win_Lose, int nbrCombinaisons)
        {
            if (win_Lose.Length == 0)
            {
                return; //si il y a 0 gagnant, on écrit rien
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"{(double)win_Lose.Length / (double)nbrCombinaisons * 100.00}% des combinaisons {ratioCombinaison} sont gagnantes: ");
            Console.WriteLine();
            Console.ResetColor();

            for (int i = 0; i < win_Lose.Length; i++)
            {
                int winLose = win_Lose[i];

                Console.Write($"La combinaison #{winLose}: ");

                for (int j = 0; j < 6; j++)
                {
                    Console.Write(tableau[winLose, j].ToString().PadLeft(2, '0'));
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        #endregion

        #region 7 - RECOMMENCER
        /// <summary>
        /// Fonction permettant de déterminer si l'utilisateur veut recommencer et mettre fin au programme s'il ne veut pas continuer.
        /// </summary>
        /// <returns>true lorsque l'utilisateur veut recommencer</returns>

        public static bool Recommencer()
        {
            string str_recommencer;
            char recommencer;

            // MESSAGE À L'UTILISATEUR S'IL VEUT RECOMMENCER LE PROCESSUS
            do
            {
                Console.SetCursorPosition(0, 24);
                Console.WriteLine("                                                                                                                                                    ");
                Console.SetCursorPosition(0, 24);
                Console.ResetColor();
                Console.Write("Voulez vous recommencer (O/N, puis appuyez sur ENTRÉE)? : ");
                str_recommencer = Console.ReadLine().ToUpper();
                if (char.TryParse(str_recommencer, out recommencer) == false || recommencer != 'O' && recommencer != 'N')
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(4, 23);
                    Console.WriteLine("Attention! Vous devez entrer O (Oui) ou N (Non) !\a");
                    Console.ResetColor();
                }
            } while (char.TryParse(str_recommencer, out recommencer) == false || recommencer != 'O' && recommencer != 'N');

            Console.Clear();

            return recommencer == 'O';
        }

        #endregion

        #region 8 - MESSAGE DE FIN DE PROGRAMME
        /// <summary>
        /// Procédure du message de remerciement et de fermeture à l'utilisateur
        /// </summary>

        public static void Message_Fin_Programme()
        {
            // MESSAGE D'APPUYER SUR ENTRÉE POUR QUITTER LE PROGRAMME
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("MERCI D'AVOIR UTILISÉ LE GÉNÉRATEUR DE NUMÉROS DE LOTERIE !");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Veuillez appuyer sur ENTRÉE pour quitter le programme.\a");

            Console.ReadLine();
        }

        #endregion

    }
}
