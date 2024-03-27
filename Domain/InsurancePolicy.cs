namespace Domain
{
    public class InsurancePolicyException(string message) : Exception(message)
    {
    }

    public class InsurancePolicy 
    {        
        public int PolicyNumber { get; set; }
        public string PolicyHolderName { get; set; } = "";   
        public double Premium{ get; set; }                                 
        
        //list of coverages on policy
        public List<Coverage> Coverages { get; set; } = [];
        public PolicyState PolicyState { get; set; } = PolicyState.New;

        public InsurancePolicy(double premium = 0, string policyHolderName = "")
        {            
            Premium = premium;
            PolicyHolderName = policyHolderName;
        }

        public void RemoveCoverage(CoverageType coverage)
        {
            
            if (!Coverages.Any(c => c.CoverageType == coverage))
            {
                throw new InsurancePolicyException("Cannot remove coverage that is not on policy");
            }

            _ = IsCancelled() ? throw new InsurancePolicyException("Cannot remove coverage on policy that is in cancelled state") : Coverages.Remove(Coverages.First(c => c.CoverageType == coverage));
        }

        //method to add coverage to policy
        public void AddCoverage(CoverageType coverage)
        {
            if (IsCancelled())
            {
                throw new InsurancePolicyException("Cannot add coverage on policy that is in cancelled state");
            }
            Coverages.Add(new Coverage { CoverageType = coverage });
        }


        //method to set policy holder name
        public void SetPolicyHolderName(string name)
        {
            _ = IsCancelled() ? throw new InsurancePolicyException("Cannot change policy holder name on policy that is in cancelled state") : PolicyHolderName = name;
        }

        //create external method to check if policy is cancelled
        public bool IsCancelled()
        {
            return PolicyState == PolicyState.Cancelled;
        }

        //method to cancel policy if it is not already cancelled
        public void CancelPolicy()
        {
            _ = IsCancelled() ? throw new InsurancePolicyException("Cannot cancel policy that is already in cancelled state") : PolicyState = PolicyState.Cancelled;
        }

        //method to activate policy if it is is new state
        public void ActivatePolicy()
        {
            _ = PolicyState == PolicyState.New ? PolicyState = PolicyState.Active : throw new InsurancePolicyException("Cannot activate policy that is not in new state");
        }
    }
   
    public class Coverage
    {
        public CoverageType CoverageType { get; set; }
        public double InsuranceSum { get; set; }
    }
    public enum CoverageType
    {
        FleshWound,
        Decapitated,        
        DrawnAndQuartered
    }

    public enum PolicyState
    {
        New,
        Active,
        Cancelled
    }
}
