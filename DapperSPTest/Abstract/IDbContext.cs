using System.Data;

namespace DapperSPTest.Abstract
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
        

        
    }
}
