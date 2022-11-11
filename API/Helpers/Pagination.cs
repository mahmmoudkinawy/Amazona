﻿namespace API.Helpers;
public class Pagination<T> where T : class
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int TotalItems { get; set; }
    public IReadOnlyList<T> Data { get; set; }

    public Pagination(int pageSize, int pageIndex, int totalItems, IReadOnlyList<T> data)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        TotalItems = totalItems;
        Data = data;
    }
}
