using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ToDoListSample;

public class MainViewModel : BindableObject
{
    public MainViewModel()
    {
        AddItemCommand = new Command<string>(title =>
        {
            var item = new ToDoItem(title);
            item.Category = selectCategory;
            if (ToDoItems.Contains(item))
                return;
            ToDoItems.Add(item);
        }, title => string.IsNullOrWhiteSpace(title) == false);

        ChangeDoneCommand = new Command<ToDoItem>(x => x.Done = !x.Done);

        DeleteItemCommand = new Command<ToDoItem>(x => ToDoItems.Remove(x));

        EdditItemCommand = new Command<ToDoItem>(item =>
        {
            item.ChangeTask(chooseName, selectCategory);
            OnPropertyChanged(nameof(item));
        });
    }
    //Двусторонняя привязка к конкретному свойству из разметки
    private string newName;

    public string chooseName
    {
        get
        {
            return newName;
        }
        set
        {
            newName = value;
            OnPropertyChanged(nameof(chooseName));
        }
    }
    private string category;
    public string selectCategory {
        get
        {
            return category;
        }
        set
        {
            category = value;
            OnPropertyChanged(nameof(selectCategory));
        }
    }

    public ICommand AddItemCommand { get; }
    public ICommand ChangeDoneCommand { get; }
    public ICommand DeleteItemCommand { get; }
    public ICommand EdditItemCommand { get; }

    public ObservableCollection<ToDoItem> ToDoItems { get; } = new ObservableCollection<ToDoItem>();
}

public class ToDoItem : BindableObject
{
    public ToDoItem(string title, string category = "Без категории")
    {
        _title = title;
        _category = category;
        DeleteItemCommand = new Command<ToDoItem>(x =>
        {
            Debug.WriteLine("Hello " + x.Title);
        });
    }

    public void ChangeTask(string newTask, string newCategory)
    {
        _title = newTask;
        _category = newCategory;
        OnPropertyChanged(nameof(Title));
        OnPropertyChanged(nameof(Category));
    }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (_done == value) return;
            _done = value;
            OnPropertyChanged(nameof(Done));
        }
    }

    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            if (_title == value) return;
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private string _category;
    public string Category
    {
        get => _category;
        set
        {
            _category = string.IsNullOrWhiteSpace(value) ? "Без категории" : value;
            OnPropertyChanged(nameof(Category));
        }
    }

    public ICommand DeleteItemCommand { get; }
    public ICommand EdditItemCommand { get; }

    public override string ToString()
    {
        return Title;
    }
}