using System.Data;

namespace Sdk.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
