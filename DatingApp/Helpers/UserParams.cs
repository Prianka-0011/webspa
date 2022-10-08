﻿namespace DatingApp.Helpers
{
    public class UserParams
    {
        public const int MAxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize=10;
        public int PageSize
        { 
            get=> _pageSize;
            set => _pageSize = (value >MAxPageSize)?MAxPageSize:value;
        }
    }
}
