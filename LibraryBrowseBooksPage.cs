namespace MauiBugReproducer;

public class LibraryBrowseBooksPage : ContentPage
{
    private readonly LibraryFilter _libraryFilter;
    private readonly CollectionView _listView;

    public LibraryBrowseBooksPage(LibraryFilter libraryFilter, bool isAllLibraries, bool shouldShowBack)
    {
        _libraryFilter = libraryFilter;

        IList<Book>? books = isAllLibraries ? _libraryFilter.AllBooks : _libraryFilter.SelectedLibrary?.Books;

        _listView = new CollectionView
        {
            ItemsSource = books,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label();
                label.SetBinding(Label.TextProperty, "Name");
                return new StackLayout
                {
                    Children = { label },
                    Padding = new Thickness(10),
                    Margin = new Thickness(10),
                };
            }),
            SelectionMode = SelectionMode.Single
        };

        _listView.SelectionChanged += (s, e) =>
        {
            if (e.CurrentSelection.FirstOrDefault() is Book selectedFacility)
            {
                _libraryFilter.SelectedBook = selectedFacility;
                _libraryFilter.CloseBrowsing();
            }
            _listView.SelectedItem = null;
        };

        var closeButton = new Button { Text = "Close" };
        closeButton.Clicked += (s, e) => _libraryFilter.CloseBrowsing();

        var stack = new StackLayout
        {
            Children = {
                new Label {
                    Text = "Browse Books",
                    FontSize = 24,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Aqua,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 20, 0, 10)
                }
            }
        };

        if (shouldShowBack)
        {
            var backButton = new Button { Text = "Back" };
            backButton.Clicked += (s, e) => _libraryFilter.GoBack();
            stack.Children.Add(backButton);
        }

        stack.Children.Add(_listView);
        stack.Children.Add(closeButton);

        Content = stack;
    }
}
