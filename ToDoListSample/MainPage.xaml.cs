namespace ToDoListSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        BindingContext = new MainViewModel();
    }

    private void SwipeItem_OnInvoked(object sender, EventArgs e)
    {
    }
}