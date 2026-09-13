using System.Text.Json;

var dataFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "tasks.json");

List<TaskItem> LoadTasks()
{
    if (!File.Exists(dataFile)) return new List<TaskItem>();
    var json = File.ReadAllText(dataFile);
    return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
}

void SaveTasks(List<TaskItem> tasks)
{
    var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(dataFile, json);
}

void AddTask(List<TaskItem> tasks, string title)
{
    tasks.Add(new TaskItem { Title = title, Done = false });
    SaveTasks(tasks);
    Console.WriteLine($"Eklendi: {title}");
}

void ListTasks(List<TaskItem> tasks)
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("Görev yok.");
        return;
    }
    for (int i = 0; i < tasks.Count; i++)
    {
        var mark = tasks[i].Done ? "x" : " ";
        Console.WriteLine($"[{mark}] {i + 1}. {tasks[i].Title}");
    }
}

void CompleteTask(List<TaskItem> tasks, int index)
{
    if (index < 1 || index > tasks.Count)
    {
        Console.WriteLine("Geçersiz görev numarası.");
        return;
    }
    tasks[index - 1].Done = true;
    SaveTasks(tasks);
    Console.WriteLine($"Tamamlandı: {tasks[index - 1].Title}");
}

var tasks = LoadTasks();

if (args.Length == 0)
{
    Console.WriteLine("Kullanım: dotnet run [add|list|done] ...");
    return;
}

switch (args[0])
{
    case "add" when args.Length > 1:
        AddTask(tasks, string.Join(" ", args.Skip(1)));
        break;
    case "list":
        ListTasks(tasks);
        break;
    case "done" when args.Length > 1:
        CompleteTask(tasks, int.Parse(args[1]));
        break;
    default:
        Console.WriteLine("Kullanım: dotnet run [add|list|done] ...");
        break;
}

class TaskItem
{
    public string Title { get; set; } = "";
    public bool Done { get; set; }
}
