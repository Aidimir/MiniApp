using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dal.Entities;

public class NomenclatureEntity
{
    [Key]
    [JsonPropertyName("ID")]
    public required string ID { get; init; }

    [JsonPropertyName("IDCat")]
    public required string IDCat { get; set; }

    [JsonPropertyName("IDTypeNew")]
    public required string IDTypeNew { get; set; }

    [JsonPropertyName("ProductionType")]
    public required string ProductionType { get; set; }

    [JsonPropertyName("IDFunctionType")]
    public string? IDFunctionType { get; set; }

    [JsonPropertyName("Name")]
    public required string Name { get; set; }

    [JsonPropertyName("Gost")]
    public required string Gost { get; set; }

    [JsonPropertyName("FormOfLength")]
    public required string FormOfLength { get; set; }

    [JsonPropertyName("Manufacturer")]
    public required string Manufacturer { get; set; }

    [JsonPropertyName("SteelGrade")]
    public required string SteelGrade { get; set; }

    [JsonPropertyName("Diameter")]
    public required decimal Diameter { get; set; }

    [JsonPropertyName("ProfileSize2")]
    public decimal? ProfileSize2 { get; set; }

    [JsonPropertyName("PipeWallThickness")]
    public required decimal PipeWallThickness { get; set; }

    [JsonPropertyName("Status")]
    public required int Status { get; set; }

    [JsonPropertyName("Koef")]
    public required decimal Koef { get; set; }

    // Добавляем поле для связи с TypeEntity
    [ForeignKey("Type")]
    public Guid IDType { get; set; }  // <-- должно быть такого же типа, как IDType в TypeEntity
    
    // Связь с TypeEntity
    public virtual TypeEntity Type { get; set; }
}