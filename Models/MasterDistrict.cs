using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace state_district_city.Models
{
    public class MasterDistrict
    {
        [Key]
        public int mDistrictId { get; set; }

        [Required(ErrorMessage = "please enter district name")]
        public string? mDistrictName { get; set; }

        public int? mDistrictOrderNo { get; set; }

        public int mDistrictIsActive { get; set; } = 1;

        public DateTime mDistrictInsDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "please select state")]
        [ForeignKey("MasterState")]
        public int mDistrictStateId { get; set; }

        public MasterState? MasterState { get; set; }
    }
}