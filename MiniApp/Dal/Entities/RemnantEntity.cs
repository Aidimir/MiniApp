using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dal.Entities;

public class RemnantEntity
{
    [Key]
    [JsonPropertyName("ID")]
    public required Guid ID { get; init; }
    
    // Наличие в текущий момент (тонны и кг)
    [JsonPropertyName("InStockT")]
    public required decimal InStockT { get; set; }

    [JsonPropertyName("InStockM")]
    public required decimal InStockM { get; set; }

    // Скоро прибудет
    [JsonPropertyName("SoonArriveT")]
    public decimal? SoonArriveT { get; set; }

    [JsonPropertyName("SoonArriveM")]
    public decimal? SoonArriveM { get; set; }

    // Занято/зарезервировано
    [JsonPropertyName("ReservedT")]
    public decimal? ReservedT { get; set; }

    [JsonPropertyName("ReservedM")]
    public decimal? ReservedM { get; set; }

    // Заказано под заказ (доп. поставка)
    [JsonPropertyName("UnderTheOrder")]
    public bool? UnderTheOrder { get; set; }

    // Средняя длина и вес труб
    [JsonPropertyName("AvgTubeLength")]
    public required decimal AvgTubeLength { get; set; }

    [JsonPropertyName("AvgTubeWeight")]
    public required decimal AvgTubeWeight { get; set; }
    
    [ForeignKey("IDStock")]
    public virtual StockEntity Stock { get; set; }
}