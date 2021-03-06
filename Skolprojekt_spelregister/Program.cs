﻿using System;
using System.IO;
// komihåg att den måste heta samma
namespace Skolprojekt_spelregister
{
    class Program
    {

        static Spel[] spelInfo = new Spel[0]; // vi lägger våran vektor utanför main metoden men innanför program classen för att den ska vara global och vi kan arbeta med den över hela programmet.
        static void Main(string[] args)
        {LaddaInTitlar(); // Laddar in från textfilen SPEL.txt

            while (true)
            {
                 
                SkrivUtMeny(); // skriver ut vår meny till skärmen.
                int menyVal = TestaInt(); // vi använder oss av metoden TestaInt för att se om talet användaren matar in är ett heltal.
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
                    SpelBibliotek();
                }
                else if (menyVal == 5)
                {
                    SparaTitlar(); // Sparar vår data vi har i vektorn till en textfil.
                    break;
                }
                else
                {
                    Console.WriteLine("Oj! här blev det lite fel, försök att ange en siffra mellan 1-5"); // Matar användaren in ett heltal utanför intervallet 1-5 skrivs denna rad ut innan resten av menyn skrivs ut igen
                }

            }
        }
        
        public static void LaddaInTitlar()
        {
            StreamReader infil = new StreamReader("SPEL.txt");
            string rad = infil.ReadLine();
            while (rad != null)
            {
                string[] fält = rad.Split('\t');
                Spel game = new Spel();
               
                game.titel = fält[0];
                game.genre = fält[1];
                game.betyg = int.Parse(fält[2]);

                spelInfo = LäggTillSpelTillOrginalVektorn(spelInfo, game);

                rad = infil.ReadLine();
            }

            infil.Close();
        }
        public static Spel[] LäggTillSpelTillOrginalVektorn(Spel[] gamlaSpelListan, Spel nyaSpel)
        {
            Spel[] nySpelLista = new Spel[gamlaSpelListan.Length+1];
            for (int i = 0; i < gamlaSpelListan.Length; i++)
            {
                nySpelLista[i] = gamlaSpelListan[i];
            }
            nySpelLista[gamlaSpelListan.Length] = nyaSpel;
            return nySpelLista;
        }

        public static void SparaTitlar()
        {
            StreamWriter utfil = new StreamWriter("SPEL.txt");
            for (int i = 0; i < spelInfo.Length; i++)
            {
                Spel game = spelInfo[i];
                utfil.WriteLine("{0}\t{1}\t{2}\t", game.titel, game.genre, game.betyg);

               // utfil.WriteLine();
            }
            utfil.Close();
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
            Console.Write("Är du säker? Ja/Nej: ");
            string svar = Console.ReadLine();
            Console.WriteLine();
            while (svar.ToLower() == "ja" || svar == "j")
            {
                Spel game = new Spel();
                Console.Write("Ange titel: ");
                game.titel = Console.ReadLine();
                Console.Write("Ange genre: ");
                game.genre = Console.ReadLine();
                Console.Write("Ange betyg: ");
                game.betyg = TestaInt();
                LagraSpel(game);

                Console.Write("Vill du lägga till ett nytt spel? Ja/Nej: ");
                svar = Console.ReadLine();
            }
            Console.WriteLine();
            if (svar.ToLower() == "nej")
            {
                return;
            }
            else
            {
                RegistreraSpel();
            }
            
        }
        public static void LagraSpel(Spel games)
        {
            // int antalElement = spelInfo.Length + 1;
            Spel[] result = new Spel[spelInfo.Length + 1];

            for (int i = 0; i < spelInfo.Length; i++)
            {
                result[i] = spelInfo[i];
            }

            result[(spelInfo.Length + 1) - 1] = games;

            spelInfo = result;
        }
        public static void SökSpel()
        {

            Console.Write("Titel/Genre: ");
            string sökfras = Console.ReadLine();
            Spel[] hittadeTitlar = sökSpelViaTitel(sökfras);
            HittadeSpel(hittadeTitlar);

           Console.Write("Vill du göra en ny sökning? Ja/Nej: ");
             string svar = Console.ReadLine();
            if (svar.ToLower()=="ja")
            {
                SökSpel();
            }
            Console.WriteLine();

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
            Spel[] hittadeSpelTrimmadVektor = new Spel[spel];
            for (int i = 0; i < spel; i++)
            {
                hittadeSpelTrimmadVektor[i] = hittadeSpel[i];
            }
            return hittadeSpelTrimmadVektor;
        }
        public static void TaBortSpel()


        {
            Console.Write("Ange titel att ta bort: ");
            string titel = Console.ReadLine();
            Spel game = sokeftertitel(titel);
            if (game == null)
            {
                Console.WriteLine("Spel med denna titeln finns inte");
                return;
            }
            kollaOmSpeletFinns(game);
        }
        public static Spel sokeftertitel(string sokeftertitel)
        {

            for (int i = 0; i < spelInfo.Length; i++)
            {

                if (spelInfo[i].titel.ToLower().Contains(sokeftertitel.ToLower()))
                {
                    return spelInfo[i];
                }
            }
            return null;
        }

        public static void kollaOmSpeletFinns(Spel game)
        {
            for (int i = 0; i < spelInfo.Length; i++)
            {
                if (spelInfo[i] == game)
                {
                    taBortSpeletPaRiktigt(i);
                    return;
                }
            }
        }

        public static void taBortSpeletPaRiktigt(int index)
        {
            Console.WriteLine("HITTAD ETT SPEL MED DENNA TITELN , TAR BORT");

            Spel[] temp = new Spel[spelInfo.Length - 1];

            for (int i = 0; i < index; i++)
            {
                temp[i] = spelInfo[i];
            }
            for (int i = index + 1; i < spelInfo.Length; i++)
            {
                temp[i - 1] = spelInfo[i];
            }
            spelInfo = temp;

        }

        public static void SpelBibliotek()
        {
            if (spelInfo.Length == 0)
            {
                Console.WriteLine("Finns inga spel lagrade ännu");
                Console.Write("Tryck enter/retur för att gå tillbaka till huvudmenyn");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                Console.WriteLine("Visar alla lagrade Spel");
                Console.WriteLine();
                foreach (Spel spelinfos in spelInfo)
                {
                    Console.WriteLine("Titel: " + spelinfos.titel + "\nGenre: " + spelinfos.genre + "\nBetyg: " + spelinfos.betyg);
                    Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                }
                Console.Write("Tryck enter/retur för att gå tillbaka till huvudmenyn");
                Console.ReadLine();
            }
        }
        public static void HittadeSpel(Spel[] hittadeSpel)
        {
            if (hittadeSpel.Length == 0)
            {
                Console.WriteLine("Hittade inga spel som matchar din sökning");
                Console.Write("Tryck enter/retur för att gå tillbaka till huvudmenyn");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                Console.WriteLine("Visar alla spel som matchar din sökning.");
                Console.WriteLine();
                foreach (Spel spelinfos in hittadeSpel)
                {
                    Console.WriteLine("Titel: " + spelinfos.titel + "\nGenre: " + spelinfos.genre + "\nBetyg: " + spelinfos.betyg);
                    Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");


                }
                Console.WriteLine();
            }
        }
        public static int TestaInt()
        {

            int d;

            while (!int.TryParse(Console.ReadLine(), out d))
            {
                Console.Write("Måste vara ett numeriskt värde\nFörsök igen: ");


            }
            return d;
        }
    }
}