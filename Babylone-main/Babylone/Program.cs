using System;
using System.Collections.Generic;
using System.Windows;
using MyProgram.Entities;

namespace MyProgram {

    class Program {
        public static int sleepTime = 1500;
        public static Character joueur1;
        public static Character joueur2;
        public static List < Character > ListePersonnages = new List < Character > ();

        public static bool BetweenRanges(int a, int b, int number) {
            return (a <= number && number <= b);
        }

        public static int Combat(Character whoStarts, Character whoFollows, List < Attacks > ListeAttaques, int _whoStarts, int _whoFollows) {
            int winner = 0;
            bool fight = true;
            int choixAction;
            int attaqueChoisie;
            Random random = new Random();

            Console.WriteLine("----------");
            Console.WriteLine("C'est le Joueur " + _whoStarts + " qui commence !");

            while (fight) {
                Console.WriteLine("----------");
                Console.WriteLine("Rappel des stats :");
                Console.WriteLine("----------");
                RappelStats(whoStarts, _whoStarts);
                Console.WriteLine("----------");
                RappelStats(whoFollows, _whoFollows);

                Thread.Sleep(Program.sleepTime);

                // Le joueur qui commence attaque

                if (whoStarts.energy != 0) {
                    switch (whoStarts.isTransfo) {
                        case false:
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoStarts + ", que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Transformation");
                            Console.WriteLine("3. Se reposer (+1 point d'énergie)");
                            Console.WriteLine("----------");
                            choixAction = Convert.ToInt32(Console.ReadLine());

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoStarts, ListeAttaques, _whoStarts);
                                    Console.WriteLine("----------");
                                    Character.Attaque(whoStarts, whoFollows, ListeAttaques, attaqueChoisie, _whoStarts);
                                    Thread.Sleep(Program.sleepTime);
                                    break;

                                case 2:
                                    Console.WriteLine("----------");
                                    Character.Transformation(whoStarts, ListeAttaques);
                                    whoStarts.isTransfo = true;
                                    Thread.Sleep(sleepTime);
                                    break;

                                case 3:
                                    whoStarts.Repos(whoStarts);
                                    Thread.Sleep(sleepTime);
                                    break;
                            }
                            break;

                        case true:
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoStarts + ", que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Se reposer (+1 point d'énergie)");
                            Console.WriteLine("----------");
                            choixAction = Convert.ToInt32(Console.ReadLine());

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoStarts, ListeAttaques, _whoStarts);
                                    break;

                                case 2:
                                    whoStarts.Repos(whoStarts);
                                    Thread.Sleep(sleepTime);                            
                                    break;
                            }
                        break;
                    }

                }
                else {
                    Console.WriteLine("----------");
                    Console.WriteLine("Que souhaitez-vous faire ?");
                    Console.WriteLine("1. Se reposer (Gain d'énergie)");
                    Console.WriteLine("----------");
                    choixAction = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    if (choixAction == 1) {
                        whoStarts.Repos(whoStarts);
                        Thread.Sleep(Program.sleepTime);
                    }

                }

                if (whoFollows.health <= 0) {
                    winner = _whoStarts;
                    fight = false;
                }
                if (whoStarts.health <= 0) {
                    winner = _whoFollows;
                    fight =false;
                }

                // Le joueur qui suit attaque

                Console.WriteLine("----------");
                Thread.Sleep(sleepTime);

                Console.WriteLine("Rappel des stats :");
                Console.WriteLine("----------");
                RappelStats(whoStarts, _whoStarts);
                Console.WriteLine("----------");
                RappelStats(whoFollows, _whoFollows);

                if (whoFollows.energy != 0) {
                    switch (whoFollows.isTransfo) {
                        case false :
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoFollows + ", que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Transformation");
                            Console.WriteLine("3. Se reposer (+1 point d'énergie)");
                            choixAction = Convert.ToInt32(Console.ReadLine());

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoFollows, ListeAttaques, _whoFollows);
                                    Console.WriteLine("----------");
                                    Character.Attaque(whoFollows, whoStarts, ListeAttaques, attaqueChoisie, _whoFollows);
                                    Thread.Sleep(Program.sleepTime);
                                    break;

                                case 2:
                                    Console.WriteLine("----------");
                                    Character.Transformation(whoFollows, ListeAttaques);
                                    whoFollows.isTransfo = true;
                                    Thread.Sleep(sleepTime);
                                    break;

                                case 3:
                                    whoFollows.Repos(whoFollows);
                                    Thread.Sleep(sleepTime);                            
                                    break;
                            }

                        break;

                        case true:
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoFollows + ", que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Se reposer (+1 point d'énergie)");
                            choixAction = Convert.ToInt32(Console.ReadLine());

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoFollows, ListeAttaques, _whoFollows);
                                    break;

                                case 2:
                                    whoFollows.Repos(whoFollows);
                                    Thread.Sleep(sleepTime);                            
                                    break;
                            }

                        Console.Clear();
                    
                        break;
                    }

                }
                else {
                    Console.WriteLine("----------");

                    Console.WriteLine("Que souhaitez-vous faire ?");
                    Console.WriteLine("1. Se reposer (Gain d'énergie)");
                    choixAction = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    if (choixAction == 1) {
                        whoFollows.Repos(whoFollows);
                        Thread.Sleep(Program.sleepTime);
                    }

                    Console.Clear();

                }

                if (whoStarts.health <= 0) {
                    winner = _whoFollows;
                    fight = false;
                }
                if (whoFollows.health <= 0) {
                    winner = _whoStarts;
                    fight =false;
                }

            }
            
            return winner;
        }

        public static int choixAttaque(Character joueur, List < Attacks > ListeAttaques, int numeroJoueur) {
            int attaqueChoisie = 0;

            switch (numeroJoueur) {
                case 1:
                    switch (joueur.isTransfo) {
                        case true :
                            Console.WriteLine("Avec quoi attaquez-vous ? (" + joueur.energy + " énergie et " + joueur.health + " pv restants)");
                            Console.WriteLine("1 : " + Character.ListeAttaques[0].attackName + " (" + ListeAttaques[0].attackEnergyCost + " énergie et " + ListeAttaques[0].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("2 : " + Character.ListeAttaques[1].attackName + " (" + ListeAttaques[1].attackEnergyCost + " énergie et " + ListeAttaques[1].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("3 : " + Character.ListeAttaques[2].attackName + " (" + ListeAttaques[2].attackEnergyCost + " énergie et " + ListeAttaques[2].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("4 : " + Character.ListeAttaques[3].attackName + " (" + ListeAttaques[3].attackEnergyCost + " énergie et " + ListeAttaques[3].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("5 : " + Character.ListeAttaques[4].attackName + " (" + ListeAttaques[4].attackEnergyCost + " énergie et " + ListeAttaques[4].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            break;

                        case false :
                            Console.WriteLine("Avec quoi attaquez-vous ? (" + joueur.energy + " énergie restante)");
                            Console.WriteLine("1 : " + Character.ListeAttaques[0].attackName + " (" + ListeAttaques[0].attackEnergyCost + " énergie)");
                            Console.WriteLine("2 : " + Character.ListeAttaques[1].attackName + " (" + ListeAttaques[1].attackEnergyCost + " énergie)");
                            Console.WriteLine("3 : " + Character.ListeAttaques[2].attackName + " (" + ListeAttaques[2].attackEnergyCost + " énergie)");
                            Console.WriteLine("4 : " + Character.ListeAttaques[3].attackName + " (" + ListeAttaques[3].attackEnergyCost + " énergie)");
                            Console.WriteLine("5 : " + Character.ListeAttaques[4].attackName + " (" + ListeAttaques[4].attackEnergyCost + " énergie)");
                            break;
                    }

                    break;

                case 2:
                    switch (joueur.isTransfo) {
                        case true :
                            Console.WriteLine("Avec quoi attaquez-vous ? (" + joueur.energy + " énergie et " + joueur.health + " pv restants)");
                            Console.WriteLine("1 : " + Character.ListeAttaques[5].attackName + " (" + ListeAttaques[5].attackEnergyCost + " énergie et " + ListeAttaques[5].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("2 : " + Character.ListeAttaques[6].attackName + " (" + ListeAttaques[6].attackEnergyCost + " énergie et " + ListeAttaques[6].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("3 : " + Character.ListeAttaques[7].attackName + " (" + ListeAttaques[7].attackEnergyCost + " énergie et " + ListeAttaques[7].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("4 : " + Character.ListeAttaques[8].attackName + " (" + ListeAttaques[8].attackEnergyCost + " énergie et " + ListeAttaques[8].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            Console.WriteLine("5 : " + Character.ListeAttaques[9].attackName + " (" + ListeAttaques[9].attackEnergyCost + " énergie et " + ListeAttaques[9].percentHealthCostUnderTransformation*joueur.health/100 + " pv)");
                            break;

                        case false :
                            Console.WriteLine("Avec quoi attaquez-vous ? (" + joueur.energy + " énergie restante)");
                            Console.WriteLine("1 : " + Character.ListeAttaques[5].attackName + " (" + ListeAttaques[5].attackEnergyCost + " énergie)");
                            Console.WriteLine("2 : " + Character.ListeAttaques[6].attackName + " (" + ListeAttaques[6].attackEnergyCost + " énergie)");
                            Console.WriteLine("3 : " + Character.ListeAttaques[7].attackName + " (" + ListeAttaques[7].attackEnergyCost + " énergie)");
                            Console.WriteLine("4 : " + Character.ListeAttaques[8].attackName + " (" + ListeAttaques[8].attackEnergyCost + " énergie)");
                            Console.WriteLine("5 : " + Character.ListeAttaques[9].attackName + " (" + ListeAttaques[9].attackEnergyCost + " énergie)");
                            break;
                    }

                    break;
            }
            

            attaqueChoisie = Convert.ToInt32(Console.ReadLine());

            switch (attaqueChoisie) {
                case 1 :
                    attaqueChoisie = 0;
                    break;

                case 2 :
                    attaqueChoisie = 1;
                    break;

                case 3 :
                    attaqueChoisie = 2;
                    break;

                case 4 :
                    attaqueChoisie = 3;
                    break;

                case 5 :
                    attaqueChoisie = 4;
                    break;
            }

            Console.Clear();

            return attaqueChoisie;
        }

        public static void RappelStats(Character joueur, int numeroJoueur) {
            Console.WriteLine("| Joueur " + numeroJoueur);
            Console.WriteLine("| " + joueur.name);
            Console.WriteLine("| Points de vie : " + joueur.health);
            Console.WriteLine("| Énergie restante : " + joueur.energy + " |");
            Console.WriteLine("| Points de transformation : " + joueur.transformationPoints + " |");
        }

        static void AddPersonnage(Character joueur, string name, string transformation, int transformationPoints, bool canTransfo, int energy, int health, int damagesMultiplicator) {
            joueur.name = name;
            joueur.transformation = transformation;
            joueur.transformationPoints = transformationPoints;
            joueur.canTransfo = canTransfo;
            joueur.energy = energy;
            joueur.health = health;
            joueur.damagesMultiplicator = damagesMultiplicator;

            ListePersonnages.Add(joueur);
        }

        static void Main(string[] args) {
            int winFight = 0;
            bool jeu = true;
            Random random = new Random();

            // Début du jeu

            while (jeu) {
                Console.WriteLine("----------");
                Console.WriteLine("Choix des personnages");

                Thread.Sleep(Program.sleepTime);

                joueur1 = new Character("");
                joueur2 = new Character("");

                Console.WriteLine("----------");
                Console.WriteLine("Joueur 1");
                Console.WriteLine("----------");

                Thread.Sleep(sleepTime);

                Console.WriteLine("Choisissez votre personnage :");

                Console.WriteLine("1. Monkey D. Luffy");
                Console.WriteLine("2. Uzumaki Naruto");
                Console.WriteLine("3. Son Goku");
                Console.WriteLine("4. Izuku Midoriya");
                Console.WriteLine("5. Kirua Zoldyck");

                int personnageChoisiJoueur1 = Convert.ToInt32(Console.ReadLine());

                if (personnageChoisiJoueur1 == 1) { // Luffy
                    Attacks joueur1attaque1 = new Attacks("Monkey D. Luffy", "Pistol", 6, 10, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Monkey D. Luffy", "Stamp", 10, 16, 4, 480, 1, 0);
                    Attacks joueur1attaque3 = new Attacks("Monkey D. Luffy", "Bazooka", 11, 15, 3, 470, 2, 0);
                    Attacks joueur1attaque4 = new Attacks("Monkey D. Luffy", "Rocket", 12, 14, 3, 460, 2, 0);
                    Attacks joueur1attaque5 = new Attacks("Monkey D. Luffy", "Gatling Gun", 17, 23, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur1attaque1);
                    Character.ListeAttaques.Add(joueur1attaque2);
                    Character.ListeAttaques.Add(joueur1attaque3);
                    Character.ListeAttaques.Add(joueur1attaque4);
                    Character.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Monkey D. Luffy", "Gear Third", 0, false, 5, 170, 5);
                }

                else if (personnageChoisiJoueur1 == 2) { // Naruto
                    Attacks joueur1attaque1 = new Attacks("Uzumaki Naruto", "Naruto Rendan", 6, 9, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Uzumaki Naruto", "Invocation : Gama kichi", 10, 16, 4, 480, 2, 0);
                    Attacks joueur1attaque4 = new Attacks("Uzumaki Naruto", "Multiclonage", 10, 14, 4, 470, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Uzumaki Naruto", "Invocation : Gama Bunta", 12, 14, 3, 460, 3, 0);
                    Attacks joueur1attaque5 = new Attacks("Uzumaki Naruto", "Rasengan", 15, 18, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur1attaque1);
                    Character.ListeAttaques.Add(joueur1attaque2);
                    Character.ListeAttaques.Add(joueur1attaque3);
                    Character.ListeAttaques.Add(joueur1attaque4);
                    Character.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Uzumaki Naruto", "Mode Baryon", 0, false, 10, 220, 4);
                }

                else if (personnageChoisiJoueur1 == 3) { // Goku
                    Attacks joueur1attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur1attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur1attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur1attaque1);
                    Character.ListeAttaques.Add(joueur1attaque2);
                    Character.ListeAttaques.Add(joueur1attaque3);
                    Character.ListeAttaques.Add(joueur1attaque4);
                    Character.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                else if (personnageChoisiJoueur1 == 4) { // Deku
                    Attacks joueur1attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur1attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur1attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur1attaque1);
                    Character.ListeAttaques.Add(joueur1attaque2);
                    Character.ListeAttaques.Add(joueur1attaque3);
                    Character.ListeAttaques.Add(joueur1attaque4);
                    Character.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                else if (personnageChoisiJoueur1 == 5) { // Kirua
                    Attacks joueur1attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur1attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur1attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur1attaque1);
                    Character.ListeAttaques.Add(joueur1attaque2);
                    Character.ListeAttaques.Add(joueur1attaque3);
                    Character.ListeAttaques.Add(joueur1attaque4);
                    Character.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                Console.Clear();

                Console.WriteLine("----------");
                Console.WriteLine("Joueur 2");
                Console.WriteLine("----------");

                Thread.Sleep(sleepTime);

                Console.WriteLine("Choisissez votre personnage :");

                Console.WriteLine("1. Monkey D. Luffy");
                Console.WriteLine("2. Uzumaki Naruto");
                Console.WriteLine("3. Son Goku");
                Console.WriteLine("4. Izuku Midoriya");
                Console.WriteLine("5. Kirua Zoldyck");

                int personnageChoisiJoueur2 = Convert.ToInt32(Console.ReadLine());

                if (personnageChoisiJoueur2 == 1) { // Luffy
                    Attacks joueur2attaque1 = new Attacks("Monkey D. Luffy", "Gum Gum Pistol", 6, 10, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Monkey D. Luffy", "Gum Gum Stamp", 10, 16, 4, 480, 1, 0);
                    Attacks joueur2attaque3 = new Attacks("Monkey D. Luffy", "Gum Gum Bazooka", 11, 15, 3, 470, 2, 0);
                    Attacks joueur2attaque4 = new Attacks("Monkey D. Luffy", "Gum Gum Rocket", 12, 14, 3, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Monkey D. Luffy", "Gum Gum Gatling Gun", 17, 23, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur2attaque1);
                    Character.ListeAttaques.Add(joueur2attaque2);
                    Character.ListeAttaques.Add(joueur2attaque3);
                    Character.ListeAttaques.Add(joueur2attaque4);
                    Character.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Monkey D. Luffy", "Gear Third", 0, false, 5, 170, 5);
                }

                else if (personnageChoisiJoueur2 == 2) { // Naruto
                    Attacks joueur2attaque1 = new Attacks("Uzumaki Naruto", "Naruto Rendan", 6, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Uzumaki Naruto", "Invocation : Gama kichi", 10, 16, 4, 480, 2, 0);
                    Attacks joueur2attaque4 = new Attacks("Uzumaki Naruto", "Multiclonage", 10, 14, 4, 470, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Uzumaki Naruto", "Invocation : Gama Bunta", 12, 14, 3, 460, 3, 0);
                    Attacks joueur2attaque5 = new Attacks("Uzumaki Naruto", "Rasengan", 15, 18, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur2attaque1);
                    Character.ListeAttaques.Add(joueur2attaque2);
                    Character.ListeAttaques.Add(joueur2attaque3);
                    Character.ListeAttaques.Add(joueur2attaque4);
                    Character.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Uzumaki Naruto", "Mode Baryon", 0, false, 10, 220, 4);
                }

                else if (personnageChoisiJoueur2 == 3) { // Goku
                    Attacks joueur2attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur2attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur2attaque1);
                    Character.ListeAttaques.Add(joueur2attaque2);
                    Character.ListeAttaques.Add(joueur2attaque3);
                    Character.ListeAttaques.Add(joueur2attaque4);
                    Character.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                else if (personnageChoisiJoueur2 == 4) { // Deku
                    Attacks joueur2attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur2attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur2attaque1);
                    Character.ListeAttaques.Add(joueur2attaque2);
                    Character.ListeAttaques.Add(joueur2attaque3);
                    Character.ListeAttaques.Add(joueur2attaque4);
                    Character.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                else if (personnageChoisiJoueur2 == 5) { // Kirua
                    Attacks joueur2attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur2attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    Character.ListeAttaques.Add(joueur2attaque1);
                    Character.ListeAttaques.Add(joueur2attaque2);
                    Character.ListeAttaques.Add(joueur2attaque3);
                    Character.ListeAttaques.Add(joueur2attaque4);
                    Character.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                Console.Clear();

                Thread.Sleep(sleepTime);
                Console.WriteLine("----------");
                Console.WriteLine("Très bons chois !");
                Console.WriteLine(joueur1.name + " VS " + joueur2.name + " !");
                Console.WriteLine("----------");
                Thread.Sleep(sleepTime);
                Console.Clear();

                int whoStarts = random.Next(1,100);
                if (whoStarts < 50) {
                    whoStarts = 1;
                    int whoFollows = 2;
                    winFight = Combat(joueur1, joueur2, Character.ListeAttaques, 1, 2);
                }
                else {
                    whoStarts = 2;
                    int whoFollows = 1;
                    winFight = Combat(joueur2, joueur1, Character.ListeAttaques, 2, 1);
                }

                jeu = false;
            }

            // Avant de quitter

            Console.WriteLine("----------");
            Console.WriteLine("Appuyez sur une touche pour quitter");
            Console.ReadKey();
        }
    }
}