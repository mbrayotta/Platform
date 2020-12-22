using System.Threading.Tasks;
using Domain;
using Persistence.DataAccess;

namespace Persistence.Querys
{
    public class OperationQuery
    {
        private readonly DataAccessOperation _dataAccessOperation;

        public OperationQuery(DataAccessOperation dataAccessOperation)
        {
            _dataAccessOperation = dataAccessOperation;
        }

        public async Task<int> Insert(Operation op)
        {
            int id = await _dataAccessOperation.DataAccess.InsertAsync<Operation>(op);

            return id;
        }

        public async Task Update(Operation op) 
        {
            await _dataAccessOperation.DataAccess.UpdateAsync<Operation>(op);
        }

        public async Task<Operation> Select(string id)
        {
            return await _dataAccessOperation.DataAccess.GetAsync<Operation>(id);
        }
    }
}
