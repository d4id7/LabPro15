using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions options = new() {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

Console.Write("Имя: ");
string playerName = Console.ReadLine()!;

Console.Write("Громкость: ");
int volume = int.Parse(Console.ReadLine()!);

Console.Write("Полный экран (да/нет): ");
bool isFullScreen = Console.ReadLine()! == "да";

Console.Write("Сложность (Easy/Normal/Hard): ");
Difficulty difficulty = Enum.Parse<Difficulty>(Console.ReadLine()!);

GameSettings settings = new() {
    PlayerName = playerName,
    Volume = volume,
    FullScreen = isFullScreen,
    Difficulty = difficulty
};

string settingsPath = "settings.json";

string jsonContent = JsonSerializer.Serialize(settings, options);
File.WriteAllText(settingsPath, jsonContent);
Console.WriteLine($"\nНастройки сохранены в {settingsPath}\n");

Console.WriteLine("Загрузка настроек...\n");

if (File.Exists(settingsPath)) {
    string loadedJson = File.ReadAllText(settingsPath);
    
    GameSettings? loadedSettings = JsonSerializer.Deserialize<GameSettings>(loadedJson);

    if (loadedSettings != null) {
        Console.WriteLine($"Игрок: {loadedSettings.PlayerName}");
        Console.WriteLine($"Громкость: {loadedSettings.Volume}");
        
        string fullScreenText = loadedSettings.FullScreen ? "да" : "нет";
        Console.WriteLine($"Полный экран: {fullScreenText}");
        
        // enum сериализуется как число (0, 1, 2), но при выводе через ToString() автоматически преобразуется в строковое представление (Easy, Normal, Hard).
        // Это стандартное поведение System.Text.Json для enum.
        Console.WriteLine($"Сложность: {loadedSettings.Difficulty}");
    }
} else {
    Console.WriteLine("Ошибка! Не удалось загрузить файл.");
}

class GameSettings {
    public string PlayerName { get; set; } = "";
    public int Volume { get; set; }
    public bool FullScreen { get; set; }
    public Difficulty Difficulty { get; set; }
}

enum Difficulty {
    Easy,
    Normal,
    Hard
}