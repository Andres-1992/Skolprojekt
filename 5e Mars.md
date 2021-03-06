using System;
using System.IO;

// komihåg att den måste heta samma
namespace Skolprojekt_spelregister
{
    class Program
    {
        static Spel[] spelInfo = new Spel[0]; // vi lägger våran vektor utanför main metoden men innanför program Klassen för att den ska vara global och vi kan arbeta med den över hela programmet.
        static void Main(string[] args)
        {
            LaddaInTitlar();
            if (spelInfo.Length == 0)
            {
                SparaTitlar();
            }
            bool fortsätt = true;
            while (fortsätt == true)
            {
                SkrivUtMeny();
                int menyVal = TestaInt(); switch (menyVal)
                {
                    case 1:
                        RegistreraSpel();
                        break;
                    case 2:
                        SökSpel();
                        break;
                    case 3:
                        TaBortSpel();
                        break;
                    case 4:
                        SorteraNamn();
                        SpelBibliotek();
                        break;
                    case 5:
                        TopFem();
                        break;
                    case 6:
                        SparaTitlar();
                        fortsätt = false;
                        break;
                    case 7:
                        fortsätt = false;
                        break;
                    default:
                        Console.WriteLine("Oj! här blev det lite fel, försök att ange en siffra mellan 1-5");
                        break;
                }
            }
        }
        /// <summary>
        /// Ladda in från textfil SPEL.txt
        /// </summary>
        public static void LaddaInTitlar()
        {
            StreamReader infil = new StreamReader("SPEL.txt");
            string rad;
            while ((rad = infil.ReadLine()) != null)
            {
                string[] fält = rad.Split('\t');
                Spel game = new Spel();
                game.titel = fält[0];
                game.genre = fält[1];
                game.betyg = int.Parse(fält[2]);
                LäggTillSpelTillOrginalVektorn(game);
            }
            infil.Close();
        }
        /// <summary>
        /// Överför innehållet från den temporära vektorn till orginalvektorn.
        /// </summary>
        /// <returns>The till spel till orginal vektorn.</returns>
        /// <param name="gamlaSpelListan">Gamla spel listan.</param>
        /// <param name="nyaSpel">Nya spel.</param>
        public static void LäggTillSpelTillOrginalVektorn(Spel nyaSpel)
        {
            Spel[] nySpelLista = new Spel[spelInfo.Length + 1];
            for (int i = 0; i < spelInfo.Length; i++)
            {
                nySpelLista[i] = spelInfo[i];
            }
            nySpelLista[spelInfo.Length] = nyaSpel;
            spelInfo = nySpelLista;
        }
        /// <summary>
        /// Skriver ut menyvalen till skärmen
        /// </summary>
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
            Console.WriteLine("[5] Visa topp 5");
            Console.WriteLine("----------------------");
            Console.WriteLine("[6] Spara ändringar och avsluta");
            Console.WriteLine("----------------------");
            Console.WriteLine("[7] Avsluta utan och spara");
            Console.WriteLine("----------------------");
            Console.Write("Ange menyval 1-7: ");
        }
        /// <summary>
        /// Under det här menyvalet att registrera spel kommer först kontrollfråga om man är säker ja/nej ifall man tryckte fel menyval och har möjlighet att hoppa ur det
        /// väljer man "Ja" så får man lägga in spel i följande ordning -> titel -> genre -> betyg
        /// efter det får man ytterligare ett val om man vill lägga in ett nytt spel eller återgå till huvudmenyn
        /// </summary>
        public static void RegistreraSpel()
        {
            Console.Write("Är du säker? Ja/Nej: ");
            string svar = KontrolleraSvar(Console.ReadLine());
            Console.WriteLine();
            while (svar.ToLower() == "ja")
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
                svar = KontrolleraSvar(Console.ReadLine());
                Console.WriteLine();
            }
        }
        /// <summary>
        /// När man har lagt in spel i ovanstående metod så lagras spelet i spelInfo
        /// </summary>

        public static void LagraSpel(Spel games)
        {
            Spel[] result = new Spel[spelInfo.Length + 1];
            for (int i = 0; i < spelInfo.Length; i++)
            {
                result[i] = spelInfo[i];
            }
            result[(spelInfo.Length + 1) - 1] = games;
            spelInfo = result;
        }

        /// <summary>
        /// Här söker man efter spel via titel eller genre, man kan även söka med några få bokstäver. Exempel Mario Kart kan sökas "Mar".
        /// Efter att man har sökt spel kommer en kontrollfråga om man vill söka ett nytt (För att slippa gå från huvudmenyn om man vill fortsätta söka) eller återgå till huvudmenyn.
        /// </summary>
        public static void SökSpel()
        {
            string svar = "ja";
            while (svar.ToLower() == "ja")
            {
                Console.Write("Titel/Genre: ");
                string sökfras = KontrolleraSökfras(Console.ReadLine().Trim());
                Spel[] hittadeTitlar = sökSpelViaTitel(sökfras);
                HittadeSpel(hittadeTitlar);
                Console.Write("Vill du göra en ny sökning? Ja/Nej: ");
                svar = KontrolleraSvar(Console.ReadLine());
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Koden som gör det möjligt att söka via med titelnamn
        /// 
        /// hittadeSpelTrimmadVektor trimmar när man söker så att man får mer exakta träffar
        ///
        /// </summary>
        /// <param name="sökfras"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Här är koden som visar om det finns spel i vektorn som stämmer överrens med vad du sökte på för spel
        /// Hittar den det man söker så skrivs titel, genre och betyg ut.
        /// </summary>
        /// <param name="hittadeSpel"></param>
        public static void HittadeSpel(Spel[] hittadeSpel)
        {
            if (hittadeSpel.Length == 0)
            {
                Console.WriteLine("Hittade inga spel som matchar din sökning");
                Console.WriteLine();
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
        
        public static void TaBortSpel()
        {
            ListaÖverSpelAttTaBort();
            if (!(spelInfo.Length == 0))
            {
                Console.Write("Ange titel att ta bort: ");
                string titel = KontrolleraSökfras(Console.ReadLine());
                Spel game = sokeftertitel(titel);
                if (game == null)
                {
                    Console.WriteLine("Spel med denna titeln finns inte");
                    return;
                }
                kollaOmSpeletFinns(game);
            }
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
            Console.WriteLine("Hittade ett spel med denna titel, tar bort");

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
        public static void ListaÖverSpelAttTaBort()
        {
            if (spelInfo.Length == 0)
            {
                Console.WriteLine("Finns inga spel lagrade ännu");
                Console.Write("Tryck enter/retur för att gå tillbaka till huvudmenyn");
                Console.ReadKey();
                Console.WriteLine();
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
                Console.WriteLine();
            }
        }
        public static void SpelBibliotek()
        {

            if (spelInfo.Length == 0)
            {
                Console.WriteLine("Finns inga spel lagrade ännu");
                Console.Write("Tryck enter/retur för att gå tillbaka till huvudmenyn");
                Console.ReadKey();
                Console.WriteLine();
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
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        public static void SorteraNamn()
        {
            for (int i = 0; i < spelInfo.Length; i++)
            {
                int minst = i;
                for (int j = i + 1; j < spelInfo.Length; j++)
                {
                    if (spelInfo[minst].titel.CompareTo (spelInfo[j].titel) > 0)
                    { minst = j; }
                }
                if (i < minst)
                {
                    Swap(spelInfo, minst, i);
                }
               
            }
        }
        public static void TopFem()
        {
            Spel[] temp = new Spel[spelInfo.Length];
            Array.Copy(spelInfo, temp, spelInfo.Length);
            bool osorterad = true;
            int end = temp.Length - 1;
            while (osorterad)
            {
                osorterad = false;
                for (int j = 0; j < end; j++)
                {
                    if (temp[j].betyg < temp[j + 1].betyg)
                    {
                        Swap(temp, j, j + 1);
                        osorterad = true;
                    }
                }
                end--;
            }
            SkrivUtTop5(temp);
            Console.ReadKey();
        }
        public static void SkrivUtTop5(Spel[] temp2)
        {
            for (int i = 0; i < temp2.Length && i < 5; i++)
            {
                Console.WriteLine(temp2[i]);
            }
        }
        public static void Swap(Spel[] temp, int a, int b)
        {
            Spel tempspel = temp[a];
            temp[a] = temp[b];
            temp[b] = tempspel;
        }
        /// <summary>
        /// Spara innehållet från våran orginal vektor till en textfil
        /// </summary>
        public static void SparaTitlar()
        {
            StreamWriter utfil = new StreamWriter("SPEL.txt");
            for (int i = 0; i < spelInfo.Length; i++)
            {
                Spel game = spelInfo[i];
                utfil.WriteLine("{0}\t{1}\t{2}\t", game.titel, game.genre, game.betyg);
            }
            utfil.Close();
        }
        public static string KontrolleraSvar(string svaret)
        {
            while (!(svaret.ToLower() == "nej" || svaret.ToLower() == "ja"))
            {
                Console.Write("Du måste svara ja eller nej: ");
                svaret = Console.ReadLine();
            }
            return svaret;
        }
        public static string KontrolleraSökfras(string sökfrasen)
        {
            while (sökfrasen == "")
            {
                Console.Write("Du måste fylla i raden: ");
                sökfrasen = Console.ReadLine().Trim();
            }
            return sökfrasen;
        }
        public static int TestaInt()
        {
            int a;
            while (!int.TryParse(Console.ReadLine(), out a))
            {
                Console.Write("Måste vara ett numeriskt värde\nFörsök igen: ");
            }
            return a;
        }
    }
}
