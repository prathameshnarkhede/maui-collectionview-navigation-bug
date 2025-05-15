namespace MauiBugReproducer;

public class Library
{
    public required string Name { get; set; }
    public List<Book> Books { get; set; } = [];
}

public class Book
{
    public required string Title { get; set; }
    public required Library Library { get; set; }
}

public class LibraryFilter : ContentView
{
    public IList<Library> AllLibraries { get; set; }
    public IList<Book> AllBooks { get; set; }

    public Library? SelectedLibrary { get; set; }
    public Book? SelectedBook { get; set; }

    public LibraryFilter()
    {
        // Mock data
        var libraryA = new Library { Name = "Library A" };
        var libraryB = new Library { Name = "Library B" };
        var book1 = new Book { Title = "Book 1", Library = libraryA };
        var book2 = new Book { Title = "Book 2", Library = libraryA };
        var book3 = new Book { Title = "Book 3", Library = libraryB };
        libraryA.Books.AddRange([book1, book2]);
        libraryB.Books.Add(book3);

        AllLibraries = [libraryA, libraryB];
        AllBooks = [book1, book2, book3];

        var browseButton = new Button { Text = "Browse" };
        browseButton.Clicked += (s, e) =>
        {
            if (AllLibraries.Count > 1)
                NavigateForward(new LibraryBrowseLibrariesPage(this));
            else
                NavigateForward(new LibraryBrowseBooksPage(this, true, false));
        };

        Content = new StackLayout
        {
            Children = { browseButton }
        };
    }

    public async void NavigateForward(ContentPage page)
    {
        await Navigation.PushModalAsync(page);
    }

    public async void CloseBrowsing()
    {
        await Navigation.PopModalAsync();
    }

    public async void GoBack()
    {
        await Navigation.PopModalAsync();
    }
}
