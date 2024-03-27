namespace Domain
{
    public interface IInsurancePolicyRepository
    {        
        void Save(InsurancePolicy policy);
        void RemovePolicy(int policyNumber);        
        InsurancePolicy GetPolicy(int policyNumber);

    }
}
