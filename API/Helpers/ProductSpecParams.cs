namespace API.Helpers;
public sealed class ProductSpecParams
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Sort { get; set; }


    public int MaxPageSize { get; set; } = 20;
	public int PageIndex { get; set; } = 1;

	private int _pageSize = 6;
	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
	}


	private string _search;
	public string? Search
	{
		get => _search;
		set => _search = value.ToLower();
	}

}
