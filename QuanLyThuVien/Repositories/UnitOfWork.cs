using QuanLyThuVien.Data;

namespace QuanLyThuVien.Repositories;

public class UnitOfWork : IDisposable
{
    private BookRepository bookRepository;
    private AuthorRepository authorRepository;
    private GenreRepository genreRepository;
    private BorrowingRepository borrowingRepository;
    private BorrowedBookRepository borrowedBookRepository;
    private StatisticsBorrowedBookRepository statisticsBorrowedBookRepository;
    private readonly QuanLyThuVienContext context;
    public UnitOfWork(QuanLyThuVienContext context)
    {
        this.context = context;
    }
    public BookRepository BookRepository
    {
        get
        {
            if (this.bookRepository == null)
            {
                this.bookRepository = new BookRepository(context);
            }
            return bookRepository;
        }
    }
    public AuthorRepository AuthorRepository    
    {
        get
        {
            if (this.authorRepository == null)
            {
                this.authorRepository = new AuthorRepository(context);
            }
            return authorRepository;
        }
    }   
    public GenreRepository GenreRepository
    {
        get
        {
            if (this.genreRepository == null)
            {
                this.genreRepository = new GenreRepository(context);
            }
            return genreRepository;
        }
    }
    public BorrowingRepository BorrowingRepository
    {
        get
        {
            if (this.borrowingRepository == null)
            {
                this.borrowingRepository = new BorrowingRepository(context);
            }
            return borrowingRepository;
        }
    }
    public BorrowedBookRepository BorrowedBookRepository
    {
        get
        {
            if (this.borrowedBookRepository == null)
            {
                this.borrowedBookRepository = new BorrowedBookRepository(context);
            }
            return borrowedBookRepository;
        }
    }
    public StatisticsBorrowedBookRepository StatisticsBorrowedBookRepository
    {
        get
        {
            if (this.statisticsBorrowedBookRepository == null)
            {
                this.statisticsBorrowedBookRepository = new StatisticsBorrowedBookRepository(context);
            }
            return statisticsBorrowedBookRepository;
        }
    }
    
    public void Save()
    {
        context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
}