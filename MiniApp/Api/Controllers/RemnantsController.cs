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
public class RemnantsController : ControllerBase
{
    private readonly IRequestClient<UpsertCommand<RemnantModel>> _upsertRequestClient;
    private readonly IRequestClient<BatchUpsertCommand<RemnantModel>> _batchUpsertRequestClient;
    private readonly IRequestClient<DeleteCommand<RemnantModel>> _deleteRequestClient;
    private readonly IRequestClient<BatchDeleteCommand<RemnantModel>> _batchDeleteRequestClient;

    public RemnantsController(
        IRequestClient<UpsertCommand<RemnantModel>> upsertRequestClient,
        IRequestClient<BatchUpsertCommand<RemnantModel>> batchUpsertRequestClient,
        IRequestClient<DeleteCommand<RemnantModel>> deleteRequestClient,
        IRequestClient<BatchDeleteCommand<RemnantModel>> batchDeleteRequestClient)
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
    public async Task<IActionResult> UpsertRemnant([FromBody] BaseUpsertRequest<RemnantModel> request)
    {
        var command = new UpsertCommand<RemnantModel> { Data = request.Data };
        var response = await _upsertRequestClient.GetResponse<RemnantModel>(command);
        return Ok(new BaseResponse<RemnantModel>
            { Data = response.Message, Success = true, Message = "Remnant upserted successfully" });
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchUpsertRemnants([FromBody] BaseBatchRequest<RemnantModel> request)
    {
        var command = new BatchUpsertCommand<RemnantModel> { Data = request.Data };
        var response = await _batchUpsertRequestClient.GetResponse<BatchResponse>(command);
        return Ok(response.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRemnant(Guid id)
    {
        var command = new DeleteCommand<RemnantModel> { Id = id };
        var response = await _deleteRequestClient.GetResponse<BatchResponse>(command);
        return Ok(response.Message);
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> BatchDeleteRemnants([FromBody] BaseBatchIdsRequest request)
    {
        var command = new BatchDeleteCommand<RemnantModel> { Ids = request.Ids };
        var response = await _batchDeleteRequestClient.GetResponse<BatchResponse>(command);
        return Ok(response.Message);
    }
}
