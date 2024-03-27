using System.Data.Common;
using Application;
using Domain;

namespace PresentationAndInfrastructure.Infrastructure
{
    public class DataPersistenceException(string message) : DbException(message) {    }

    public class SaveToMemory : IDataPersistence
    {
        private static readonly List<InsurancePolicy> _data = [];

        public void Delete(int policyNumber)
        {
            _data.RemoveAll(p => p.PolicyNumber == policyNumber);
        }

        public InsurancePolicy Read(int policyNumber)
        {
            return _data.FirstOrDefault(p => p.PolicyNumber == policyNumber) ?? throw new DataPersistenceException("Policy not found");
        }

        public void Store(InsurancePolicy policy)
        {            
            policy.PolicyNumber = _data.Count+1;
            _data.Add(policy);
        }
    }
}
