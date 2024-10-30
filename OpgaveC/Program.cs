namespace OpgaveC
{

    class Program
    {
        static string filePath = "telefonbog.txt";
        static string[] telefonbog = new string[100]; //Størrelsen på telefonbogen er sat til 100
        static int kontaktTæller = 0; //Holder styr på antal kontakter i arrayet

        static void Main()
        {
            IndlæsData();
            bool Setup = true;
            while (Setup)
            {
                Console.Clear();
                Console.WriteLine("Telefonbog");
                Console.WriteLine("1. Tilføj kontakt");
                Console.WriteLine("2. Søg efter kontakt");
                Console.WriteLine("3. Rediger kontakt");
                Console.WriteLine("4. Slet kontakt");
                Console.WriteLine("5. Sorter kontakter");
                Console.WriteLine("6. Vis alle kontakter");
                Console.WriteLine("7. Afslut");
                Console.Write("Vælg en mulighed: ");

                string menuValg = Console.ReadLine();
                switch (menuValg)
                {
                    case "1":
                        addKontakt();
                        break;
                    case "2":
                        SøgKontakt();
                        break;
                    case "3":
                        RedigerKontakt();
                        break;
                    case "4":
                        SletKontakt();
                        break;
                    case "5":
                        SorterKontakter();
                        break;
                    case "6":
                        VisKontakter();
                        break;
                    case "7":
                        Setup = false;
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg, prøv igen.");
                        break;
                }
            }
            GemData();
        }

        static void addKontakt()
        {
            if (kontaktTæller >= telefonbog.Length)
            {
                Console.WriteLine("Telefonbogen er fuld. Kan ikke tilføje flere kontakter.");
                return;
            }

            Console.Write("Indtast navn: ");
            string navn = Console.ReadLine();
            Console.Write("Indtast telefonnummer: ");
            string tlf = Console.ReadLine();
            Console.Write("Indtast e-mail: ");
            string email = Console.ReadLine();

            telefonbog[kontaktTæller] = $"{navn},{tlf},{email}";
            kontaktTæller++;
            Console.WriteLine("Kontakt tilføjet.");
            Console.ReadKey();
        }

        static void SøgKontakt()
        {
            Console.Write("Indtast navn på kontakt du vil søge efter: ");
            string søgeord = Console.ReadLine().ToLower();
            bool fundet = false;

            for (int i = 0; i < kontaktTæller; i++)
            {
                if (telefonbog[i].Split(',')[0].ToLower().Contains(søgeord))
                {
                    Console.WriteLine(telefonbog[i]);
                    fundet = true;
                }
            }

            if (!fundet)
            {
                Console.WriteLine("Kontakt ikke fundet.");
            }
            Console.WriteLine("Tryk på en tast for at fortsætte...");
            Console.ReadKey();
        }

        static void RedigerKontakt()
        {
            Console.Write("Indtast navn på kontakt du vil redigere: ");
            string navn = Console.ReadLine().ToLower();
            int index = FindKontaktIndex(navn);

            if (index == -1)
            {
                Console.WriteLine("Kontakt ikke fundet.");
                Console.ReadKey();
                return;
            }

            Console.Write("Opdater navn (tryk Enter for at beholde nuværende): ");
            string nytNavn = Console.ReadLine();
            Console.Write("Opdater telefonnummer (tryk Enter for at beholde nuværende): ");
            string nytTelefon = Console.ReadLine();
            Console.Write("Opdater e-mail (tryk Enter for at beholde nuværende): ");
            string nyEmail = Console.ReadLine();

            var kontaktOplysninger = telefonbog[index].Split(',');
            string opdateretNavn = !string.IsNullOrWhiteSpace(nytNavn) ? nytNavn : kontaktOplysninger[0];
            string opdateretTelefon = !string.IsNullOrWhiteSpace(nytTelefon) ? nytTelefon : kontaktOplysninger[1];
            string opdateretEmail = !string.IsNullOrWhiteSpace(nyEmail) ? nyEmail : kontaktOplysninger[2];

            telefonbog[index] = $"{opdateretNavn},{opdateretTelefon},{opdateretEmail}";
            Console.WriteLine("Kontakt opdateret.");
            Console.ReadKey();
        }

        static void SletKontakt()
        {
            Console.Write("Indtast navn på kontakt du vil slette: ");
            string navn = Console.ReadLine().ToLower();
            int index = FindKontaktIndex(navn);

            if (index == -1)
            {
                Console.WriteLine("Kontakt ikke fundet.");
                Console.ReadKey();
                return;
            }

            for (int i = index; i < kontaktTæller - 1; i++)
            {
                telefonbog[i] = telefonbog[i + 1];
            }

            kontaktTæller--;
            telefonbog[kontaktTæller] = null;
            Console.WriteLine("Kontakt slettet.");
            Console.ReadKey();
        }

        static void SorterKontakter()
        {
            for (int i = 0; i < kontaktTæller - 1; i++)
            {
                for (int j = i + 1; j < kontaktTæller; j++)
                {
                    string navn1 = telefonbog[i].Split(',')[0].ToLower();
                    string navn2 = telefonbog[j].Split(',')[0].ToLower();

                    if (string.Compare(navn1, navn2) > 0)
                    {
                        string temp = telefonbog[i];
                        telefonbog[i] = telefonbog[j];
                        telefonbog[j] = temp;
                    }
                }
            }
            Console.WriteLine("Kontakter sorteret efter navn.");
            Console.ReadKey();
        }

        static void VisKontakter()
        {
            Console.Clear();
            Console.WriteLine("Alle kontakter:");
            for (int i = 0; i < kontaktTæller; i++)
            {
                Console.WriteLine(telefonbog[i]);
            }
            Console.WriteLine("Tryk på en tast for at fortsætte");
            Console.ReadKey();
        }

        static int FindKontaktIndex(string navn)
        {
            for (int i = 0; i < kontaktTæller; i++)
            {
                if (telefonbog[i].Split(',')[0].ToLower() == navn)
                {
                    return i;
                }
            }
            return -1;
        }

        static void IndlæsData()
        {
            if (File.Exists(filePath))
            {
                string[] linjer = File.ReadAllLines(filePath);
                for (int i = 0; i < linjer.Length && i < telefonbog.Length; i++)
                {
                    telefonbog[i] = linjer[i];
                    kontaktTæller++;
                }
            }
        }

        static void GemData()
        {
            File.WriteAllLines(filePath, telefonbog[..kontaktTæller]);
        }
    }

}