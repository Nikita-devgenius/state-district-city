using System.ComponentModel.DataAnnotations;
using System;
namespace state_district_city.Models
{
    public class MasterState
    {
        [Key]
        public int mStateId { get; set; }

        [Required(ErrorMessage = "please enter state name")]
        public string? mStateName { get; set; }

        public int? mStateOrderNo { get; set; }

        public int mStateIsActive { get; set; } = 1;

        public DateTime mStateInsDate { get; set; } = DateTime.Now;
    }
}



