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

class Player {
    public string Name { get; set; } = "";
    public int Level { get; set; }
}