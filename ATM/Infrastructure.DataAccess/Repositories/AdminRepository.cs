using Application.Abstractions.Repositories;
using Application.Models.Admins;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Admin?> FindAdminByUserName(string userName)
    {
        const string sql = """
                           select admin_id, admin_name, admin_password
                           from admins
                           where admin_name = :name;
                           """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("name", userName);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (await reader.ReadAsync().ConfigureAwait(false) is false)
        {
            return null;
        }

        return new Admin(
            Id: reader.GetInt64(0),
            Username: reader.GetString(1),
            Password: reader.GetString(2));
    }

    public async Task RegisterNewAdmin(string username, string password)
    {
        const string sql = """
                           insert into admins (admin_name, admin_password)
                           values (:name, :password);
                           """;
        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("name", username);
        command.AddParameter("password", password);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}