using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BsketDTO
{
    public record BasketItemDTO
    {

        [Required]
        public int Id { get; init; }

        public string ProductName { get; init; }

        public string PicureUrl { get; init; }

        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }

        public string Category { get; init; }
        public string Brand { get; init; }

        [Range(1, 99)]
        public int Quntity { get; init; } // دة تحديدا product عايز كام قطعه من ال  
    }
}
