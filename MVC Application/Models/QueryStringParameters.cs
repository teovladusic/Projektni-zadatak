﻿namespace Projektni_Zadatak_Project_Service.Models
{
    public abstract class QueryStringParameters
    {
        //all params that should exist in every model
        const int maxPageSize = 50;

        private int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = (value < 1) ? 1 : value;
            }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > maxPageSize)
                {
                    _pageSize = maxPageSize;
                }
                else if (value < 1)
                {
                    _pageSize = 1;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
        public string OrderBy { get; set; }
    }
}
