namespace MauiBugReproducer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var libraryFilter = new LibraryFilter();
            Content = libraryFilter;
        }
    }
}
