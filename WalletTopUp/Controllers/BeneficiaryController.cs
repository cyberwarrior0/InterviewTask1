using Microsoft.AspNetCore.Mvc;
using WalletTopUp.Models;
using WalletTopUp.Services;

namespace WalletTopUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IWalletService _walletService;

        public WalletController(IBeneficiaryService beneficiaryService, IWalletService walletService)
        {
            _beneficiaryService = beneficiaryService;
            _walletService = walletService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Beneficiary>>> GetBeneficiariesByUserId(int userId)
        {
            try
            {
                var beneficiaries = await _beneficiaryService.GetBeneficiariesByUserId(userId);

                if (beneficiaries == null)
                {
                    return NotFound(); 
                }

                return beneficiaries;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("topup")]
        public async Task<ActionResult<string>> TopUp([FromBody] TopUpRequest request)
        {
            try
            {
                if (!Enum.IsDefined(typeof(TopUpOption), request.topUpOption))
                {
                    return BadRequest("Invalid top-up amount.");
                }

                var message = await _walletService.TopUp(request);
                return message.ToString();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    }
}
