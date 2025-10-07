using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dal.Entities;

public class StockEntity
{
    [Key]
    [JsonPropertyName("IDStock")]
    public required Guid IDStock { get; init; }   // GUID‑строка

    [JsonPropertyName("IDDivision")]
    public required Guid IDDivision { get; set; }

    /* ------------- адрес и контакт ---------------- */

    [JsonPropertyName("Stock")]
    public required string Stock { get; set; }          // название города/склада

    [JsonPropertyName("StockName")]
    public required string StockName { get; set; }     // обычно совпадает с выше

    [JsonPropertyName("Address")]
    public string? Address { get; set; }              // может быть пустой строкой

    [JsonPropertyName("Schedule")]
    public required string Schedule { get; set; }

    /* ------------- платежи ------------------------ */

    [JsonPropertyName("CashPayment")]
    public required bool CashPayment { get; set; }

    [JsonPropertyName("CardPayment")]
    public required bool CardPayment { get; set; }

    /* ------------- FIAS и собственник ------------ */

    [JsonPropertyName("FIASId")]
    public required Guid FIASId { get; set; }

    [JsonPropertyName("OwnerInn")]
    public required string OwnerInn { get; set; }      // ИНН

    [JsonPropertyName("OwnerKpp")]
    public required string OwnerKpp { get; set; }     // КПП

    [JsonPropertyName("OwnerFullName")]
    public required string OwnerFullName { get; set; }

    [JsonPropertyName("OwnerShortName")]
    public required string OwnerShortName { get; set; }

    /* ------------- транспорт --------------------- */

    [JsonPropertyName("RailwayStation")]
    public required string RailwayStation { get; set; }   // код станции

    [JsonPropertyName("ConsigneeCode")]
    public required string ConsigneeCode { get; set; }
}
