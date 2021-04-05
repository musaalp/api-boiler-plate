using MySql.Data.MySqlClient;
using Sdk.Interfaces;
using System;
using System.Data;

namespace Data.Utils
{
    public interface IOrderDbConnection : IDbConnectionFactory
    {
    }

    public class OrderDbConnection : IOrderDbConnection
    {
        private readonly string _connectionString;

        public OrderDbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException("OrderDbConnection_string_has_not_found");

            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            if (_connectionString == null)
                throw new NullReferenceException($"{nameof(OrderDbConnection)}_{nameof(_connectionString)}");

            return new MySqlConnection(_connectionString);
        }
    }
}
