using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions options = new() {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

// === Часть 1 ===
Player me = new() {
    Name = "Илья",
    Level = 2
};

// meJson имеет тип string, потому что объект класса преобразуется в строку
string meJson = JsonSerializer.Serialize(me);

System.Console.WriteLine("--- Компактный вариант ---");
System.Console.WriteLine(meJson);

meJson = JsonSerializer.Serialize(me, options);

System.Console.WriteLine();
System.Console.WriteLine("--- Форматированный вариант ---");
System.Console.WriteLine(meJson);

// === Часть 2 ===
Player? meRestored = JsonSerializer.Deserialize<Player>(meJson);

System.Console.WriteLine();
if (meRestored != null) {
    System.Console.WriteLine($"{meRestored.Name}, уровень {meRestored.Level}");
}

string manualJson = """
{
    "Name": "Александр",
    "Level": 35
}
""";

Player? manualDeserialized = JsonSerializer.Deserialize<Player>(manualJson);

System.Console.WriteLine();
if (manualDeserialized != null) {
    System.Console.WriteLine($"{manualDeserialized.Name}, уровень {manualDeserialized.Level}");
}

// === Часть 3 ===
Player hero = new() {
    Name = "Ракета",
    Level = 12
};

string heroJson = JsonSerializer.Serialize(hero, options);
File.WriteAllText("player.json", heroJson);

string heroFromFile = File.ReadAllText("player.json");

Player? heroRestored = JsonSerializer.Deserialize<Player>(heroFromFile);

System.Console.WriteLine();
if (heroRestored != null) {
    System.Console.WriteLine($"{heroRestored.Name}, уровень {heroRestored.Level}");
}

// === Часть 4 ===
Player villain = new() {
    Name = "Лорд Злыдень",
    Level = 26
};

List<Player> players = [me, hero, villain];

string playersJson = JsonSerializer.Serialize(players, options);
File.WriteAllText("players.json", playersJson);

string playersFromFile = File.ReadAllText("players.json");

System.Console.WriteLine();
try {
    // В данном случае возникает общее исключение JsonException, которое связано с ошибкой при работе с JSON: синтаксические ошибки, незакрытая скобка и прочие
    List<Player>? playersRestored = JsonSerializer.Deserialize<List<Player>>(playersFromFile, options);

    if (playersRestored != null) {
        System.Console.WriteLine($"Количество игроков: {playersRestored.Count}");
        System.Console.WriteLine($"Второй игрок: {playersRestored[1].Name}");
        System.Console.WriteLine($"Уровень последнего игрока: {playersRestored[2].Level}");
    }
}
catch (JsonException ex) {
    System.Console.WriteLine($"Ошибка: {ex.Message}");
}

class Player {
    public string Name { get; set; } = "";
    public int Level { get; set; }
}