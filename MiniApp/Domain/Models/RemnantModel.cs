namespace Domain.Models;

public class RemnantModel
{
    public Guid ID { get; set; }
    public Guid IDStock { get; set; }
    public decimal InStockT { get; set; }
    public decimal InStockM { get; set; }
    public decimal? SoonArriveT { get; set; }
    public decimal? SoonArriveM { get; set; }
    public decimal? ReservedT { get; set; }
    public decimal? ReservedM { get; set; }
    public bool? UnderTheOrder { get; set; }
    public decimal AvgTubeLength { get; set; }
    public decimal AvgTubeWeight { get; set; }
}