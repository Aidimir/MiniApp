namespace Domain.Models;

public class StockModel
{
    public Guid IDStock { get; set; }
    public required string Stock { get; set; }
    public required string StockName { get; set; }
    public string? Address { get; set; }
    public string? Schedule { get; set; }
    public Guid? IDDivision { get; set; }
    public bool? CashPayment { get; set; }
    public bool? CardPayment { get; set; }
    public string? FIASId { get; set; }
    public string? OwnerInn { get; set; }
    public string? OwnerKpp { get; set; }
    public string? OwnerFullName { get; set; }
    public string? OwnerShortName { get; set; }
    public string? RailwayStation { get; set; }
    public string? ConsigneeCode { get; set; }
}