using ACommon;
using ACommon.Objects;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.Services
{
    internal class DataAccessService
    {
        public string ConnectionString => _ConnectionOptions.ConnectionString;

        private readonly ConnectionOptions _ConnectionOptions;

        public DataAccessService(ConnectionOptions connectionoptions)
        {
            _ConnectionOptions = connectionoptions;
        }
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
