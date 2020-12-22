using Infra.Data;

namespace Persistence.DataAccess
{
    public class DataAccessOperation
    {
        private readonly IDataAccessRegistry _dataAccessRegistry;
        public IDataAccess DataAccess => _dataAccessRegistry.GetDataAccess();

        public DataAccessOperation(IDataAccessRegistry dataAccessRegistry)
        {

            _dataAccessRegistry = dataAccessRegistry;
        }
    }
}
