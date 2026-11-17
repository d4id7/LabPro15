using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

var jsonOptions = new JsonSerializerOptions {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

// Далее для заданий будут использоваться {}, которые создают новую область видимости. Это позволяет использовать одни и те же имена переменных в разных заданиях.

Console.WriteLine("=== ★ Задание 1: JSON объекта ===");
{
    var programmingCourse = new Course {
        Name = "Алгоритмизация и программирование",
        TimeInHours = 36,
        Assessment = AssessmentType.Pass
    };

    string courseJson = JsonSerializer.Serialize(programmingCourse, jsonOptions);
    Console.WriteLine("----- Сериализованный объект -----");
    Console.WriteLine(courseJson);

    var deserializedCourse = JsonSerializer.Deserialize<Course>(courseJson, jsonOptions);

    Console.WriteLine("\n----- Восстановленный объект -----");
    if (deserializedCourse != null) {
        Console.WriteLine($"Дисциплина: {deserializedCourse.Name}");
        Console.WriteLine($"Время в семестре: {deserializedCourse.TimeInHours} ч");
        Console.WriteLine($"Вид оценки: {deserializedCourse.Assessment}\n");
    }
}

Console.WriteLine("\n=== ★★ Задание 2: JSON коллекции ===");
{
    var semesterCourses = new List<Course>
    {
        new() { Name = "Элементы высшей математики", TimeInHours = 36, Assessment = AssessmentType.Exam },
        new() { Name = "Физическая культура", TimeInHours = 24, Assessment = AssessmentType.Pass },
        new() { Name = "Численные методы", TimeInHours = 24, Assessment = AssessmentType.Graded }
    };

    string coursesFilePath = "courses.json";

    string coursesJson = JsonSerializer.Serialize(semesterCourses, jsonOptions);
    File.WriteAllText(coursesFilePath, coursesJson);
    Console.WriteLine($"Список успешно сохранён в {coursesFilePath}\n");

    string fileContent = File.ReadAllText(coursesFilePath);
    var deserializedCourses = JsonSerializer.Deserialize<List<Course>>(fileContent, jsonOptions);

    if (deserializedCourses != null) {
        Console.WriteLine("----- Загруженная коллекция -----");
        Console.WriteLine($"Количество дисциплин: {deserializedCourses.Count}");

        foreach (var course in deserializedCourses) {
            Console.WriteLine($"- {course.Name} ({course.Assessment}, {course.TimeInHours} ч)");
        }
        Console.WriteLine();
    }
    else {
        Console.WriteLine("Ошибка: не удалось загрузить данные из файла.");
    }
}

Console.WriteLine("\n=== ★★★ Задание 3: JSON + существующая коллекция ===");
{
    var bookTitles = new List<string>
    {
        "Евгений Онегин",
        "Бесы",
        "Солярис",
        "Преступление и наказание",
        "Бойцовский клуб"
    };

    // Храним именно количество страниц, так как это полезная метрика для системы
    var bookPageCounts = new Dictionary<string, int> {
        [bookTitles[0]] = 352,
        [bookTitles[1]] = 768,
        [bookTitles[2]] = 288,
        [bookTitles[3]] = 672,
        [bookTitles[4]] = 256
    };

    string booksFilePath = "books.json";

    // Сериализуем Dictionary, так как он представляет собой законченную пару "ключ-значение", которая имеет самостоятельную ценность для системы учёта, в отличие от простого списка названий.
    string booksJson = JsonSerializer.Serialize(bookPageCounts, jsonOptions);
    File.WriteAllText(booksFilePath, booksJson);

    string booksFileContent = File.ReadAllText(booksFilePath);
    var deserializedBookPages = JsonSerializer.Deserialize<Dictionary<string, int>>(booksFileContent, jsonOptions);

    if (deserializedBookPages != null) {
        Console.WriteLine("----- Загруженный каталог книг -----");
        int bookIndex = 1;
        foreach (var book in deserializedBookPages) {
            Console.WriteLine($"{bookIndex++}. В книге \"{book.Key}\" (изд. \"Эксклюзивная классика\") содержится {book.Value} страниц.");
        }
    }
    else {
        Console.WriteLine("Ошибка: не удалось загрузить каталог книг.");
    }
}

// === Определения типов ===
public class Course {
    // string.Empty - это то же самое, что и = "", но его использование улучшает читаемость кода
    public string Name { get; set; } = string.Empty;
    public int TimeInHours { get; set; }
    public AssessmentType Assessment { get; set; }
}

public enum AssessmentType {
    Pass,       // Зачёт
    Graded,     // Оценка
    Exam        // Экзамен
}