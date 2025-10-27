using CapybaraPetApp.Application.Abstractions;

namespace CapybaraPetApp.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CapybaraPetAppDbContext _dbContext;

    public UnitOfWork(CapybaraPetAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}