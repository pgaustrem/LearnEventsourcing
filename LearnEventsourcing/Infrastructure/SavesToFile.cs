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
            //delete InsurancePolicy from the static in-memory list called _data
            _data.RemoveAll(p => p.PolicyNumber == policyNumber);
        }

        public InsurancePolicy Read(int policyNumber)
        {
            //return InsurancePolicy from the static in-memory list called _data, if no policy is found, return a new InsurancePolicy
            return _data.FirstOrDefault(p => p.PolicyNumber == policyNumber) ?? throw new DataPersistenceException("Policy not found");
        }

        public void Store(InsurancePolicy policy)
        {
            //save InsurancePolicy to the static in-memory list called _data. If an existing policy is found, update it, otherwise add it to the list
            policy.PolicyNumber = _data.Count+1;
            _data.Add(policy);
        }
    }
}
