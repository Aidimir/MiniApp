using System.ComponentModel.DataAnnotations;

namespace Api.DTO.Request

{
    // Базовые модели запросов
    public class BaseUpsertRequest<T>
    {
        [Required]
        public T Data { get; set; }
    }

    public class BaseBatchRequest<T>
    {
        [Required]
        public List<T> Data { get; set; }
    }

    public class BaseIdRequest
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class BaseBatchIdsRequest
    {
        [Required]
        public List<Guid> Ids { get; set; }
    }

    // Модели ответов
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class BatchResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}