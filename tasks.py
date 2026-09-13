import json
import sys
from pathlib import Path

DATA_FILE = Path(__file__).parent / "tasks.json"


def load_tasks():
    if not DATA_FILE.exists():
        return []
    with open(DATA_FILE, "r", encoding="utf-8") as f:
        return json.load(f)


def save_tasks(tasks):
    with open(DATA_FILE, "w", encoding="utf-8") as f:
        json.dump(tasks, f, ensure_ascii=False, indent=2)


def add_task(tasks, title):
    tasks.append({"title": title, "done": False})
    save_tasks(tasks)
    print(f"Eklendi: {title}")


def list_tasks(tasks):
    if not tasks:
        print("Görev yok.")
        return
    for i, task in enumerate(tasks, start=1):
        mark = "x" if task["done"] else " "
        print(f"[{mark}] {i}. {task['title']}")


def complete_task(tasks, index):
    if index < 1 or index > len(tasks):
        print("Geçersiz görev numarası.")
        return
    tasks[index - 1]["done"] = True
    save_tasks(tasks)
    print(f"Tamamlandı: {tasks[index - 1]['title']}")


def main():
    tasks = load_tasks()
    if len(sys.argv) < 2:
        print("Kullanım: tasks.py [add|list|done] ...")
        return

    command = sys.argv[1]
    if command == "add" and len(sys.argv) > 2:
        add_task(tasks, " ".join(sys.argv[2:]))
    elif command == "list":
        list_tasks(tasks)
    elif command == "done" and len(sys.argv) > 2:
        complete_task(tasks, int(sys.argv[2]))
    else:
        print("Kullanım: tasks.py [add|list|done] ...")


if __name__ == "__main__":
    main()
