using System;
// komihåg att den måste heta samma
namespace Skolprojekt_spelregister
{
    class Program
    {

        static Spel[] spelInfo = new Spel[0];
        static void Main(string[] args)
        {

            while (true)
            {
                SkrivUtMeny();
                int menyVal = TestaInt();
                if (menyVal == 1)
                {
                    RegistreraSpel();
                }
                else if (menyVal == 2)
                {
                    SökSpel();
                }
                else if (menyVal == 3)
                {
                    TaBortSpel();
                }
                else if (menyVal == 4)
                {
                    SpelBibliotek(spelInfo);
                }
                else if (menyVal == 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Oj! här blev det lite fel, försök att ange en siffra mellan 1-5");
                }

            }
        }

        public static void SkrivUtMeny()
        {
            Console.WriteLine("[1] Registrera spel");
            Console.WriteLine("----------------------");
            Console.WriteLine("[2] Sök efter spel");
            Console.WriteLine("----------------------");
            Console.WriteLine("[3] Ta bort spel");
            Console.WriteLine("----------------------");
            Console.WriteLine("[4] Visa spelbibliotek");
            Console.WriteLine("----------------------");
            Console.WriteLine("[5] Avsluta");
            Console.WriteLine("----------------------");
            Console.Write("Ange menyval: ");

        }
        public static void RegistreraSpel()
        {
            Console.Write("Är du säker?: ");
            string svar = Console.ReadLine();
            while (svar == "ja" || svar == "JA" || svar == "Ja")
            {
                Spel game = new Spel();
                Console.Write("Ange titel: ");
                game.titel = Console.ReadLine();
                Console.Write("Ange genre: ");
                game.genre = Console.ReadLine();
                Console.Write("Ange betyg: ");
                game.betyg = TestaInt();

                LagraSpel(game);
                Console.WriteLine("Vill du lägga till ett nytt spel? ");
                svar = Console.ReadLine();
            }
        }
        public static void LagraSpel(Spel game)
        {
            int antalElement = spelInfo.Length + 1;
            Spel[] result = new Spel[antalElement];

            for (int i = 0; i < spelInfo.Length; i++)
            {
                result[i] = spelInfo[i];
            }

            result[antalElement - 1] = game;

            spelInfo = result;


        }
        public static void SökSpel()
        {
            Console.Write("Titel: ");
            string sökfras = Console.ReadLine();
            Spel[] hittadetitlar = sökSpelViaTitel(sökfras);
            HittadeSpel(hittadetitlar);
        }
        public static Spel[] sökSpelViaTitel(string sökfras)
        {
            Spel[] hittadeSpel = new Spel[spelInfo.Length];
            int spel = 0;



            for (int i = 0; i < spelInfo.Length; i++)
            {
                if (spelInfo[i].titel.ToLower().Contains(sökfras.ToLower()) || spelInfo[i].genre.ToLower().Contains(sökfras.ToLower()))
                {
                    hittadeSpel[spel++] = spelInfo[i];
                }
            }
            Spel[] hittadespeltrimmade = new Spel[spel];
            for (int i = 0; i < spel; i++)
            {
                hittadespeltrimmade[i] = hittadeSpel[i];
            }
            return hittadespeltrimmade;
        }
        public static void TaBortSpel()
        {
            Console.WriteLine("lägg kod för Ta bort spel här");
            Console.ReadLine();
        }
        public static void SpelBibliotek(Spel[] spelInfo)
        {
            Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
            Console.WriteLine("Visar alla lagrade Spel");
            Console.WriteLine(" ");
            foreach (Spel spelinfos in spelInfo)
            {
                Console.WriteLine("Titel: " + spelinfos.titel + "\nGenre: " + spelinfos.genre + "\nBetyg: " + spelinfos.betyg);
                Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                Console.Write("Tryck enter/retur för att gå till huvudmenyn ");
                Console.ReadLine();

            }
        }
        public static void HittadeSpel(Spel[] spel)
        {
            Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
            Console.WriteLine("Visar alla spel som matchar din sökning.");
            Console.WriteLine(" ");
            foreach (Spel spelinfos in spel)
            {
                Console.WriteLine("Titel: " + spelinfos.titel + "\nGenre: " + spelinfos.genre + "\nBetyg: " + spelinfos.betyg);
                Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                Console.Write("Tryck enter/retur för att gå till huvudmenyn ");
                Console.ReadLine();

            }

        }
        public static int TestaInt()
        {

            int d = 0;

            while (!int.TryParse(Console.ReadLine(), out d))
            {
                Console.Write("Måste vara ett numeriskt värde\nFörsök igen: ");


            }
            return d;
        }
    }
}