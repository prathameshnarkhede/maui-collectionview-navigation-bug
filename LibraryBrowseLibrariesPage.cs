namespace MauiBugReproducer;

public class LibraryBrowseLibrariesPage : ContentPage
{
    private readonly LibraryFilter _libraryFilter;
    private readonly CollectionView _listView;

    public LibraryBrowseLibrariesPage(LibraryFilter libraryFilter)
    {
        _libraryFilter = libraryFilter;

        _listView = new CollectionView
        {
            ItemsSource = _libraryFilter.AllLibraries,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label();
                label.SetBinding(Label.TextProperty, static (Library lib) => lib.Name);
                return new StackLayout
                {
                    Children = { label },
                    Padding = new Thickness(10),
                    Margin = new Thickness(10),
                };
            }),
            SelectionMode = SelectionMode.Single,
        };

        _listView.SelectionChanged += (s, e) =>
        {
            if (e.CurrentSelection.FirstOrDefault() is Library selectedSite)
            {
                _libraryFilter.SelectedLibrary = selectedSite;
                if (selectedSite.Books.Count > 1)
                    _libraryFilter.NavigateForward(new LibraryBrowseBooksPage(_libraryFilter, false, true));
                else
                    _libraryFilter.CloseBrowsing();
            }
            _listView.SelectedItem = null;
        };

        var closeButton = new Button { Text = "Close", VerticalOptions = LayoutOptions.End };
        closeButton.Clicked += (s, e) => _libraryFilter.CloseBrowsing();

        Content = new StackLayout
        {
            Children = {
                new Label {
                    Text = "Browse Libraries",
                    FontSize = 24,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Aqua,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 20, 0, 10)
                },
                _listView,
                closeButton
            }
        };
    }
}
