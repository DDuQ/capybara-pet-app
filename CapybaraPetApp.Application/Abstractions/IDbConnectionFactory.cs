using System.Data;

namespace CapybaraPetApp.Application.Abstractions;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnection(CancellationToken cancellationToken = default);
}