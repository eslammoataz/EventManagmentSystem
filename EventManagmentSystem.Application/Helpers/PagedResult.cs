﻿namespace EventManagmentSystem.Application.Helpers
{
    public class PagedResult<T>
    {

        public List<T> Data { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }

}