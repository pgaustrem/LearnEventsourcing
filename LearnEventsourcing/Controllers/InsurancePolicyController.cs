using Domain;
using Microsoft.AspNetCore.Mvc;

namespace LearnEventsourcing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly ILogger<InsurancePolicyController> _logger;
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;

        public InsurancePolicyController(ILogger<InsurancePolicyController> logger, IInsurancePolicyRepository insurancePolicyRepository)
        {
            _logger = logger;
            _insurancePolicyRepository = insurancePolicyRepository;
        }

        [HttpGet(Name = "InsurancePolicy")]
        public InsurancePolicy Get(int policyNumber)
        {
            return _insurancePolicyRepository.GetPolicy(policyNumber);
        }

        [HttpPost(Name = "CreatePolicy")]
        public int Post(CoverageType coverage, double premium, string policyHolderName)
        {
            var policy = new InsurancePolicy(premium, policyHolderName);
            policy.AddCoverage(coverage);
            _insurancePolicyRepository.Save(policy);

            return policy.PolicyNumber;
        }

        [HttpPatch("policyholder", Name = "PolicyHolderName")]
        public void Patch(int policyNumber, string name)
        {
            var policy = _insurancePolicyRepository.GetPolicy(policyNumber);
            policy.SetPolicyHolderName(name);
            _insurancePolicyRepository.Save(policy);
        }

        [HttpDelete(Name = "RemovePolicy")]
        public void Delete(int policyNumber)
        {
            _insurancePolicyRepository.RemovePolicy(policyNumber);
        }

        [HttpPut(Name = "ActivatePolicy")]
        public void Activate(int policyNumber)
        {
            var policy = _insurancePolicyRepository.GetPolicy(policyNumber);
            policy.ActivatePolicy();
            _insurancePolicyRepository.Save(policy);
        }

        [HttpPost("coverages", Name = "AddCoverage")]
        public void Post(int policyNumber, CoverageType coverage)
        {
            var policy = _insurancePolicyRepository.GetPolicy(policyNumber);
            policy.AddCoverage(coverage);
            _insurancePolicyRepository.Save(policy);
        }

        [HttpDelete("coverages", Name = "RemoveCoverage")]
        public void Delete(int policyNumber, CoverageType coverage)
        {
            var policy = _insurancePolicyRepository.GetPolicy(policyNumber);
            policy.RemoveCoverage(coverage);
            _insurancePolicyRepository.Save(policy);
        }
    }
}
    