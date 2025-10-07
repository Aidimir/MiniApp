using Api.DTO.Request;
using Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IRequestClient<UpsertCommand<TypeModel>> _upsertRequestClient;
        private readonly IRequestClient<BatchUpsertCommand<TypeModel>> _batchUpsertRequestClient;
        private readonly IRequestClient<DeleteCommand<TypeModel>> _deleteRequestClient;
        private readonly IRequestClient<BatchDeleteCommand<TypeModel>> _batchDeleteRequestClient;

        public TypesController(
            IRequestClient<UpsertCommand<TypeModel>> upsertRequestClient,
            IRequestClient<BatchUpsertCommand<TypeModel>> batchUpsertRequestClient,
            IRequestClient<DeleteCommand<TypeModel>> deleteRequestClient,
            IRequestClient<BatchDeleteCommand<TypeModel>> batchDeleteRequestClient)
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
        public async Task<IActionResult> UpsertType([FromBody] BaseUpsertRequest<TypeModel> request)
        {
            var command = new UpsertCommand<TypeModel> { Data = request.Data };
            var response = await _upsertRequestClient.GetResponse<TypeModel>(command);
            return Ok(new BaseResponse<TypeModel> { Data = response.Message, Success = true, Message = "Type upserted successfully" });
        }

        [HttpPost("batch")]
        public async Task<IActionResult> BatchUpsertTypes([FromBody] BaseBatchRequest<TypeModel> request)
        {
            var command = new BatchUpsertCommand<TypeModel> { Data = request.Data };
            var response = await _batchUpsertRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            var command = new DeleteCommand<TypeModel> { Id = id };
            var response = await _deleteRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteTypes([FromBody] BaseBatchIdsRequest request)
        {
            var command = new BatchDeleteCommand<TypeModel> { Ids = request.Ids };
            var response = await _batchDeleteRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }
    }
}