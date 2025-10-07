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
    public class NomenclaturesController : ControllerBase
    {
        private readonly IRequestClient<UpsertCommand<NomenclatureModel>> _upsertRequestClient;
        private readonly IRequestClient<BatchUpsertCommand<NomenclatureModel>> _batchUpsertRequestClient;
        private readonly IRequestClient<DeleteCommand<NomenclatureModel>> _deleteRequestClient;
        private readonly IRequestClient<BatchDeleteCommand<NomenclatureModel>> _batchDeleteRequestClient;

        public NomenclaturesController(
            IRequestClient<UpsertCommand<NomenclatureModel>> upsertRequestClient,
            IRequestClient<BatchUpsertCommand<NomenclatureModel>> batchUpsertRequestClient,
            IRequestClient<DeleteCommand<NomenclatureModel>> deleteRequestClient,
            IRequestClient<BatchDeleteCommand<NomenclatureModel>> batchDeleteRequestClient)
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
        public async Task<IActionResult> UpsertNomenclature([FromBody] BaseUpsertRequest<NomenclatureModel> request)
        {
            var command = new UpsertCommand<NomenclatureModel> { Data = request.Data };
            var response = await _upsertRequestClient.GetResponse<NomenclatureModel>(command);
            return Ok(new BaseResponse<NomenclatureModel> { Data = response.Message, Success = true, Message = "Nomenclature upserted successfully" });
        }

        [HttpPost("batch")]
        public async Task<IActionResult> BatchUpsertNomenclatures([FromBody] BaseBatchRequest<NomenclatureModel> request)
        {
            var command = new BatchUpsertCommand<NomenclatureModel> { Data = request.Data };
            var response = await _batchUpsertRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNomenclature(Guid id)
        {
            var command = new DeleteCommand<NomenclatureModel> { Id = id };
            var response = await _deleteRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteNomenclatures([FromBody] BaseBatchIdsRequest request)
        {
            var command = new BatchDeleteCommand<NomenclatureModel> { Ids = request.Ids };
            var response = await _batchDeleteRequestClient.GetResponse<BatchResponse>(command);
            return Ok(response.Message);
        }
    }
