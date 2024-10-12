﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string PicureUrl { get; set; }
        public decimal Price { get; set; }

        public string Category { get; set; }
        public string Brand { get; set; }

        public int Quntity { get; set; } // دة تحديدا product عايز كام قطعه من ال 

    }
}
