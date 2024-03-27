using Domain;

namespace Application
{
    public interface IDataPersistence 
    {         
           void Store(InsurancePolicy policy);
           void Delete(int policyNumber);
           InsurancePolicy Read(int policyNumber);
    }


    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly IDataPersistence _dataPersistence;

        public InsurancePolicyRepository(IDataPersistence dataPersistence)
        {
            _dataPersistence = dataPersistence;
        }

        public InsurancePolicy GetPolicy(int policyNumber)
        {
            return _dataPersistence.Read(policyNumber);
        }

        public void RemovePolicy(int policyNumber)
        {
            _dataPersistence.Delete(policyNumber);
        }

        public void Save(InsurancePolicy policy)
        {
           _dataPersistence.Store(policy);
        }
    }
}
