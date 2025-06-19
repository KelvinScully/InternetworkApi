using ACommon.Objects;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.Services
{
    internal class DataAccessService
    {
        private readonly string _ConnectionString;

        public DataAccessService(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        // Create or Post
        public async Task<ApiResult<bool>> ExecuteNonQueryAsync(string storeProcedure, List<SqlParameter> parameters)
        {
            try
            {
                using var connection = new SqlConnection(_ConnectionString);
                await connection.OpenAsync();
                using var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Name, param.Value ?? DBNull.Value);
                }

                await command.ExecuteNonQueryAsync();
                return new ApiResult<bool>
                {
                    IsSuccessful = true,
                    Value = true,
                    Message = "Object Inserted Successfully"
                };

            }
            catch (Exception ex)
            {
                return new ApiResult<bool>
                {
                    IsSuccessful = false,
                    Value = false,
                    Message = $"Object Failed to Insert Database: {ex}"
                };
            }
        }

        /// <summary>
        /// Currently not in use
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <param name="mapFunction"></param>
        /// <returns></returns>
        public async Task<List<T>?> ExecuteReaderListAsync<T>(string storeProcedure, List<SqlParameter> parameters, Func<SqlDataReader, T> mapFunction)
        {
            List<T> results = [];

            try
            {
                using var connection = new SqlConnection(_ConnectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Name, param.Value ?? DBNull.Value);
                }

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    results.Add(mapFunction(reader));
                }

                return results;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ApiResult<T?>> ExecuteReaderSingleAsync<T>(string storeProcedure, List<SqlParameter> parameters, Func<SqlDataReader, T> mapFunction)
        {
            try
            {
                using var connection = new SqlConnection(_ConnectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(storeProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Name, param.Value ?? DBNull.Value);
                }

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    return new ApiResult<T?>
                    {
                        IsSuccessful = true,
                        Value = mapFunction(reader),
                        Message = "Object Found"
                    };
                }

                return new ApiResult<T?>
                {
                    IsSuccessful = true,
                    Value = default,
                    Message = "Object not found with that ID"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<T?>
                {
                    IsSuccessful = false,
                    Value = default,
                    Message = $"Error in the Database: {ex}"
                };
            }

        }

        // Batch Support to be added later
    }

    internal static class DataReaderHelper
    {
        public static T Get<T>(SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            if (reader.IsDBNull(ordinal))
                return default!;

            object value = reader.GetValue(ordinal);

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }

    internal class SqlParameter
    {
        public string Name { get; set; } = string.Empty;
        public object Value { get; set; } = DBNull.Value;

        public SqlParameter(string name, object? value)
        {
            Name = name;
            Value = value ?? DBNull.Value;
        }
    }
}
