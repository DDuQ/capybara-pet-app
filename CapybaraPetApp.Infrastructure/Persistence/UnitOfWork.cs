using CapybaraPetApp.Application.Abstractions;

namespace CapybaraPetApp.Infrastructure.Persistence;

public class UnitOfWork(CapybaraPetAppDbContext dbContext) : IUnitOfWork
{
    public void Dispose()
    {
        dbContext.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}