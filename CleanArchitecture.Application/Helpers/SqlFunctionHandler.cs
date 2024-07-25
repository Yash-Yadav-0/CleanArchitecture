using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace CleanArchitecture.Application.Helpers
{
    public abstract class SqlFunctionHandler<TRequest, TResponse>
                where TRequest : IRequest<IList<TResponse>>
    {
        protected readonly IConfiguration configuration;
        protected SqlFunctionHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<IList<TResponse>> HandleAsync(
            TRequest request,
            string functionName,
            Func<IDataReader, TResponse> mapRow,
            CancellationToken cancellationToken,
            Dictionary<string, object> parameters = null)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var results = new List<TResponse>();

            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);

            // Build the function call string
            var commandText = BuildFunctionCall(functionName, parameters);

            await using var command = new NpgsqlCommand(commandText, connection)
            {
                CommandType = CommandType.Text
            };

            // Add parameters if any
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                results.Add(mapRow(reader));
            }
            await connection.CloseAsync();
            return results;
        }

        private string BuildFunctionCall(string functionName, Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return $"SELECT * FROM {functionName}()";
            }

            var parameterString = string.Join(", ", parameters.Select(p => $"{p.Key} := @{p.Key}"));
            return $"SELECT * FROM {functionName}({parameterString})";
        }
    }

}
