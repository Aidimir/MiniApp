namespace Domain.Models;

public class NomenclatureModel
{
    public string ID { get; set; }
    public string IDCat { get; set; }
    public string IDType { get; set; }
    public required string IDTypeNew { get; set; }
    public required string ProductionType { get; set; }
    public int? IDFunctionType { get; set; }
    public required string Name { get; set; }
    public required string Gost { get; set; }
    public required string FormOfLength { get; set; }
    public required string Manufacturer { get; set; }
    public required string SteelGrade { get; set; }
    public decimal Diameter { get; set; }
    public decimal? ProfileSize2 { get; set; }
    public decimal PipeWallThickness { get; set; }
    public required int Status { get; set; }
    public decimal Koef { get; set; }
}