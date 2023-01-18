using MongoDB.Driver;
using MongoDB.Bson;

MongoClient dbClient = new MongoClient("mongodb+srv://Ralle00:Vinter2023_@cluster0.mkqttnl.mongodb.net/?retryWrites=true&w=majority");

var database = dbClient.GetDatabase("Spelbutiken");
var spel = database.GetCollection<BsonDocument>("Spel");
var personal = database.GetCollection<BsonDocument>("Personal");

void Undermeny()
{
    Console.WriteLine($"1. Lägg till\n" +
    $"2. Visa alla\n" +
    $"3. Uppdatera\n" +
    $"4. Ta bort\n" +
    $"5. Gå tillbaka");
}

bool meny = true;

while (meny)
{

MainMenu:
    Console.Clear();
    Console.WriteLine($"==Spelbutiken==\n" +
        $"1. Spel\n" +
        $"2. Personal\n" +
        $"3. Avsluta");

    int.TryParse(Console.ReadLine(), out int input);

    if (input == 1)
    {
        Console.Clear();
        Undermeny();

        int.TryParse(Console.ReadLine(), out int choice);

        switch (choice)
        {
            case 1:
                Console.WriteLine("Lägg till spel");
                Console.WriteLine("Genre:");
                string genre = Console.ReadLine();
                Console.WriteLine("Title:");
                string title = Console.ReadLine();
                Console.WriteLine("Price:");
                int.TryParse(Console.ReadLine(), out int price);
                Console.WriteLine($"Du har lagt till {title} i listan över spel");
                Console.ReadKey();

                var document = new BsonDocument
                {
                    {"Genre", genre},
                    {"Title", title},
                    {"Price", price}
                };
                spel.InsertOne(document);
                break;

            case 2:
                Console.Clear();
                Console.WriteLine("==Tillgängliga spel i butiken==\n");
                var documents = spel.Find(new BsonDocument()).ToList();

                foreach (var item in documents)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("\nTryck på valfri tangent för att återgå till startmenyn");
                Console.ReadKey();
                break;

            case 3:
                Console.Clear();
                Console.WriteLine("==Uppdatera ett spel==");
                Console.WriteLine("Ange title:");
                string gameTitle = Console.ReadLine();
                Console.WriteLine("Vad vill du uppdatera?\n" + "1. Genre\n" + "2. Title\n" + "3. Price" + "");

                int.TryParse(Console.ReadLine(), out int val);


                if (val == 1)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("Title", gameTitle);
                    var dbDocument = spel.Find(filter).FirstOrDefault();
                    if (dbDocument != null)
                    {
                        var currentGenre = dbDocument["Genre"].AsString;
                        Console.WriteLine("Nuvarande genre: " + currentGenre);
                        Console.Write("Ny genre: ");
                        string updatedGenre = Console.ReadLine();
                        filter = Builders<BsonDocument>.Filter.And(
                                Builders<BsonDocument>.Filter.Eq("Title", gameTitle),
                                Builders<BsonDocument>.Filter.Eq("Genre", currentGenre));
                        var update = Builders<BsonDocument>
                            .Update.Set("Genre", updatedGenre);
                        spel.UpdateOne(filter, update);
                        Console.WriteLine("Uppdatering genomförd");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ingen matchning, försök igen!");
                        Console.ReadKey();
                    }

                }
                else if (val == 2)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("Title", gameTitle);
                    var dbDocument = spel.Find(filter).FirstOrDefault();
                    if (dbDocument != null)
                    {
                        var currentTitle = dbDocument["Title"].AsString;
                        Console.WriteLine("Nuvarande Title: " + currentTitle);
                        Console.Write("Ny Title: ");
                        string updatedTitle = Console.ReadLine();
                        filter = Builders<BsonDocument>.Filter.And(
                                Builders<BsonDocument>.Filter.Eq("Title", gameTitle),
                                Builders<BsonDocument>.Filter.Eq("Title", currentTitle));
                        var update = Builders<BsonDocument>
                            .Update.Set("Title", updatedTitle);
                        spel.UpdateOne(filter, update);
                        Console.WriteLine("Uppdatering genomförd");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ingen matchning, försök igen!");
                        Console.ReadKey();
                    }
                }
                else if (val == 3)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("Title", gameTitle);
                    var dbDocument = spel.Find(filter).FirstOrDefault();
                    if (dbDocument != null)
                    {
                        var currentPrice = dbDocument["Price"].AsInt32;
                        Console.WriteLine("Nuvarande pris: " + currentPrice);
                        Console.Write("Nytt pris: ");
                        int.TryParse(Console.ReadLine(), out int updatedPrice);
                        filter = Builders<BsonDocument>.Filter.And(
                                Builders<BsonDocument>.Filter.Eq("Title", gameTitle),
                                Builders<BsonDocument>.Filter.Eq("Price", currentPrice));
                        var update = Builders<BsonDocument>
                            .Update.Set("Price", updatedPrice);
                        spel.UpdateOne(filter, update);
                        Console.WriteLine("Uppdatering genomförd");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ingen matchning, försök igen!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Felaktigt val!");
                    Console.ReadKey();
                }
                break;

            case 4:
                var allDocuments = spel.Find(new BsonDocument()).ToList();

                foreach (var item in allDocuments)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Ange id för det spel du vill ta bort: ");
                var id = ObjectId.Parse(Console.ReadLine());
                var query = Builders<BsonDocument>.Filter.Eq("_id", id);
                spel.DeleteOne(query);
                Console.WriteLine($"Spel med id {id} togs bort");
                Console.ReadLine();
                break;
            case 5:
                Console.Clear();
                goto MainMenu;

            default:
                Console.WriteLine("Felaktig inmatning, tryck på valfri tangent för att återgå");
                Console.ReadKey();
                break;
        }
    }
    else if (input == 2)
    {
        Console.Clear();
        Undermeny();

        int.TryParse(Console.ReadLine(), out int choice);

        switch (choice)
        {
            case 1:
                Console.WriteLine("Lägg till ny anställd");
                Console.WriteLine("Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Age:");
                string age = Console.ReadLine();
                Console.WriteLine("Salary:");
                int.TryParse(Console.ReadLine(), out int salary);
                Console.WriteLine($"{name} har lagts till i listan över anställda");
                Console.ReadKey();

                var document = new BsonDocument
                {
                    {"Name", name},
                    {"Age", age},
                    {"Salary", salary}
                };
                personal.InsertOne(document);
                break;

            case 2:
                var documents = personal.Find(new BsonDocument()).ToList();

                foreach (var item in documents)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("\nTryck på valfri tangent för att återgå till startmenyn");
                Console.ReadKey();
                break;

            case 3:
                Console.Clear();
                Console.WriteLine("==Uppdatera en anställd==");
                Console.WriteLine("Name:");
                string employeeName = Console.ReadLine();
                Console.WriteLine("Vad vill du uppdatera?\n" + "1. Name\n" + "2. Age\n" + "3. Salary" + "");

                int.TryParse(Console.ReadLine(), out int val);


                if (val == 1)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("Name", employeeName);
                    var dbDocument = personal.Find(filter).FirstOrDefault();
                    if (dbDocument != null)
                    {
                        var currentName = dbDocument["Name"].AsString;
                        Console.WriteLine("Nuvarande namn: " + currentName);
                        Console.Write("Nytt namn: ");
                        string updatedName = Console.ReadLine();
                        filter = Builders<BsonDocument>.Filter.And(
                                Builders<BsonDocument>.Filter.Eq("Name", employeeName),
                                Builders<BsonDocument>.Filter.Eq("Name", currentName));
                        var update = Builders<BsonDocument>
                            .Update.Set("Name", updatedName);
                        personal.UpdateOne(filter, update);
                        Console.WriteLine("Uppdatering genomförd");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ingen matchning, försök igen!");
                        Console.ReadKey();
                    }

                }
                else if (val == 2)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("Name", employeeName);
                    var dbDocument = personal.Find(filter).FirstOrDefault();
                    if (dbDocument != null)
                    {
                        var currentAge = dbDocument["Age"].AsInt32;
                        Console.WriteLine("Nuvarande ålder: " + currentAge);
                        Console.Write("Ny ålder: ");
                        int.TryParse(Console.ReadLine(), out int updatedAge);
                        filter = Builders<BsonDocument>.Filter.And(
                                Builders<BsonDocument>.Filter.Eq("Name", employeeName),
                                Builders<BsonDocument>.Filter.Eq("Age", currentAge));
                        var update = Builders<BsonDocument>
                            .Update.Set("Age", updatedAge);
                        personal.UpdateOne(filter, update);
                        Console.WriteLine("Uppdatering genomförd");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ingen matchning, försök igen!");
                        Console.ReadKey();
                    }
                }
                else if (val == 3)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("Name", employeeName);
                    var dbDocument = personal.Find(filter).FirstOrDefault();
                    if (dbDocument != null)
                    {
                        var currentSalary = dbDocument["Salary"].AsInt32;
                        Console.WriteLine("Nuvarande lön: " + currentSalary);
                        Console.Write("Ny lön: ");
                        int.TryParse(Console.ReadLine(), out int updatedSalary);
                        filter = Builders<BsonDocument>.Filter.And(
                                Builders<BsonDocument>.Filter.Eq("Name", employeeName),
                                Builders<BsonDocument>.Filter.Eq("Salary", currentSalary));
                        var update = Builders<BsonDocument>
                            .Update.Set("Salary", updatedSalary);
                        personal.UpdateOne(filter, update);
                        Console.WriteLine("Uppdatering genomförd");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ingen matchning, försök igen!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Felaktigt val!");
                    Console.ReadKey();
                }
                break;

            case 4:
                var allDocuments = personal.Find(new BsonDocument()).ToList();

                foreach (var item in allDocuments)
                {
                    Console.WriteLine(item);
                }

                try
                {
                    Console.WriteLine("Ange id för den anställde du vill ta bort: ");
                    var id = ObjectId.Parse(Console.ReadLine());
                    var query = Builders<BsonDocument>.Filter.Eq("_id", id);
                    personal.DeleteOne(query);
                    Console.WriteLine($"anställd med id {id} togs bort");
                    Console.ReadKey();

                }
                catch (Exception)
                {
                    Console.WriteLine("Ett fel uppstod, vänligen fyll i id (endast siffror och bokstäver)");
                    Console.ReadKey();
                }
                break;

            case 5:
                Console.Clear();
                goto MainMenu;

            default:
                Console.WriteLine("Felaktig inmatning, tryck på valfri tangent för att återgå");
                Console.ReadKey();
                break;
        }


    }
    else if (input == 3)
    {
        Console.WriteLine("Avslutar program");
        meny = false;
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Vänligen gör ett giltigt val.. Tryck för att återgå till startmeny");
        Console.ReadKey();
    }


}
