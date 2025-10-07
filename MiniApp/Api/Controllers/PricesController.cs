using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using Api.DTO.Request;
using Contracts;
using Domain.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IRequestClient<UpsertCommand<PriceModel>> _upsertRequestClient;
        private readonly IRequestClient<BatchUpsertCommand<PriceModel>> _batchUpsertRequestClient;
        private readonly IRequestClient<DeleteCommand<PriceModel>> _deleteRequestClient;
        private readonly IRequestClient<BatchDeleteCommand<PriceModel>> _batchDeleteRequestClient;

        public PricesController(
            IRequestClient<UpsertCommand<PriceModel>> upsertRequestClient,
            IRequestClient<BatchUpsertCommand<PriceModel>> batchUpsertRequestClient,
            IRequestClient<DeleteCommand<PriceModel>> deleteRequestClient,
            IRequestClient<BatchDeleteCommand<PriceModel>> batchDeleteRequestClient)
        {
            _upsertRequestClient = upsertRequestClient;
            _batchUpsertRequestClient = batchUpsertRequestClient;
            _deleteRequestClient = deleteRequestClient;
            _batchDeleteRequestClient = batchDeleteRequestClient;
        }

        private Guid UserId
        {
            get
            {
                if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value,
                        out var id))
                    throw new AuthenticationException("No user id was provided");
                return id;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpsertPrice([FromBody] BaseUpsertRequest<PriceModel> request)
        {
            var command = new UpsertCommand<PriceModel> { Data = request.Data };
            var response = await _upsertRequestClient.GetResponse<PriceModel>(command);
            return Ok(new BaseResponse<PriceModel> { Data = response.Message, Success = true, Message = "Price upserted successfully" });
        }

        [HttpPost("batch")]
        public async Task<IActionResult> BatchUpsertPrices([FromBody] BaseBatchRequest<PriceModel> request)
        {
            var command = new BatchUpsertCommand<PriceModel> { Data = request.Data };
            var response = await _batchUpsertRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(Guid id)
        {
            var command = new DeleteCommand<PriceModel> { Id = id };
            var response = await _deleteRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeletePrices([FromBody] BaseBatchIdsRequest request)
        {
            var command = new BatchDeleteCommand<PriceModel> { Ids = request.Ids };
            var response = await _batchDeleteRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }
    }
