using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dal.Entities;

public class PriceEntity
{
    [Key]
    [JsonPropertyName("ID")]
    public required Guid ID { get; init; }

    // Цены (в примере – целые числа, но decimal обеспечивает точность)
    [JsonPropertyName("PriceT")]
    public required decimal PriceT { get; set; }

    [JsonPropertyName("PriceLimitT1")]
    public decimal? PriceLimitT1 { get; set; }

    [JsonPropertyName("PriceT1")]
    public decimal? PriceT1 { get; set; }

    [JsonPropertyName("PriceLimitT2")]
    public decimal? PriceLimitT2 { get; set; }

    [JsonPropertyName("PriceT2")]
    public decimal? PriceT2 { get; set; }

    [JsonPropertyName("PriceM")]
    public required decimal PriceM { get; set; }

    [JsonPropertyName("PriceLimitM1")]
    public decimal? PriceLimitM1 { get; set; }

    [JsonPropertyName("PriceM1")]
    public decimal? PriceM1 { get; set; }

    [JsonPropertyName("PriceLimitM2")]
    public decimal? PriceLimitM2 { get; set; }

    [JsonPropertyName("PriceM2")]
    public decimal? PriceM2 { get; set; }

    // НДС – в примере 20 (целое число). Если хотите хранить как процент,
    // можно использовать int или decimal.
    [JsonPropertyName("NDS")]
    public required int NDS { get; set; }
    
    [ForeignKey("Stock")]
    [JsonPropertyName("IDStock")]
    public required Guid IDStock { get; set; }  // Добавлено поле для внешнего ключа
    
    public virtual StockEntity Stock { get; set; }
}

