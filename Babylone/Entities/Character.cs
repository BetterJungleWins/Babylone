namespace MyProgram.Entities {
    
    public class Character {
        public string name;
        public string transformation;
        public int transformationPoints;
        public bool isTransfo;
        public float experience;
        public int energy;
        public int dodgeChances;
        public int health;
        public int damagesMultiplicator;
        public int damages;
        public bool hit;
        public static List<Attacks> ListeAttaques = new List<Attacks>();
        public static Attacks attaque;

        public Character(string _name) {
            name = _name;
        }

        public static void Attaque(Character joueurAttaque, Character joueurDefend, List<Attacks> ListeAttaques, int attaqueChoisie) {
            Random random = new Random();
            int transfoHpLoss = 0;

            if (ListeAttaques[attaqueChoisie].percentHealthCostUnderTransformation > 0) {
                transfoHpLoss = joueurAttaque.health*ListeAttaques[attaqueChoisie].percentHealthCostUnderTransformation/100;
                joueurAttaque.health -= transfoHpLoss;

                Console.WriteLine("----------");
                Console.WriteLine(joueurAttaque.name + " Attaque avec " + ListeAttaques[attaqueChoisie].attackName + " ! " + "-" + ListeAttaques[attaqueChoisie].attackEnergyCost + " points d'énergie et -" + transfoHpLoss + " points de vie");
            }
            else {
                Console.WriteLine("----------");
                Console.WriteLine(joueurAttaque.name + " Attaque avec " + ListeAttaques[attaqueChoisie].attackName + " ! " + "-" + ListeAttaques[attaqueChoisie].attackEnergyCost + " points d'énergie");
            }
            
            joueurAttaque.energy -= ListeAttaques[attaqueChoisie].attackEnergyCost;
            Thread.Sleep(Program.sleepTime);
            int hit = random.Next(1, ListeAttaques[attaqueChoisie].attackHitChances); // Détermine si l'attaque touche ou non

            if (hit > 50) { // Si ça touche
                int damagesDealt = random.Next(ListeAttaques[attaqueChoisie].attackDamagesMin, ListeAttaques[attaqueChoisie].attackDamagesMax) * ListeAttaques[attaqueChoisie].damagesMultiplicator;
                Console.WriteLine("Touché ! " + joueurAttaque.name + " inflige " + damagesDealt + " points de dégâts.");
                Console.WriteLine("----------");

                joueurDefend.health -= damagesDealt; // Retire les hp de l'ennemi       
            }
            else {
                Console.WriteLine("Loupé !");
                Console.WriteLine("----------");
            }

        }

        public void Repos() {
            Random random = new Random();
            
        }

        public static void Transformation(Character joueurAttaque, List < Attacks > ListeAttaques) {
            Console.Clear();
            Console.WriteLine("----------");
            joueurAttaque.isTransfo = true;
            int lostHealth = joueurAttaque.health/4;
            int dodgeChancesUnderTransformation = joueurAttaque.dodgeChances*3/2;

            switch (joueurAttaque.name) {
                case "Monkey D. Luffy":
                    Console.WriteLine("Luffy active le Gear Second ! (-" + lostHealth + " points de vie)");

                    // Changer le nom des attaques
                    
                    ListeAttaques[0].attackName = "Gum Gum no Jet Pistol";
                    ListeAttaques[1].attackName = "Gum Gum no Jet Stamp";
                    ListeAttaques[2].attackName = "Gum Gum no Jet Bazooka";
                    ListeAttaques[1].attackName = "Gum Gum no Jet Rocket";
                    ListeAttaques[2].attackName = "Gum Gum no Jet Gatling Gun";

                    break;

                case "Uzumaki Naruto":
                    Console.WriteLine("Naruto active le Mode Baryon !");

                    // Changer le nom des attaques
                    
                    ListeAttaques[0].attackName = "Naruto Nisen Rendan";
                    ListeAttaques[1].attackName = "Gamakichi : Boule de feu suprême";
                    ListeAttaques[2].attackName = "Multiclonage Supra";
                    ListeAttaques[1].attackName = "Gama Bunta : Ittoryuu Iai";
                    ListeAttaques[2].attackName = "Senpo : Biju Rasenshuriken";

                    break;

                case "Son Goku":
                    Console.WriteLine("Goku active l'Ultra Instinct !");

                    // Changer le nom des attaques
                    
                    ListeAttaques[0].attackName = "Twin Dragon Shot";
                    ListeAttaques[1].attackName = "Super Kamehameha";
                    ListeAttaques[2].attackName = "Kaioken Kamehameha";
                    ListeAttaques[1].attackName = "Ranbu Gekimetsu";
                    ListeAttaques[2].attackName = "Kamehameha Divin";

                    break;
            }
            
            // Perte de vie due à la transfo

            joueurAttaque.health -= lostHealth;

            // Self harming à chaque attaque
                    
            ListeAttaques[0].percentHealthCostUnderTransformation = 5;
            ListeAttaques[1].percentHealthCostUnderTransformation = 10;
            ListeAttaques[2].percentHealthCostUnderTransformation = 15;

            // Augmenter les chances d'esquive

            joueurAttaque.dodgeChances = dodgeChancesUnderTransformation;

            // Augmenter les dégâts des attaques

            ListeAttaques[0].damagesMultiplicator = ListeAttaques[0].damagesMultiplicator*2;
            ListeAttaques[1].damagesMultiplicator = ListeAttaques[1].damagesMultiplicator*2;
            ListeAttaques[2].damagesMultiplicator = ListeAttaques[2].damagesMultiplicator*2;
            ListeAttaques[3].damagesMultiplicator = ListeAttaques[3].damagesMultiplicator*2;
            ListeAttaques[4].damagesMultiplicator = ListeAttaques[4].damagesMultiplicator*2;

        }
    }      
}