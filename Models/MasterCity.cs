using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace state_district_city.Models
{
    public class MasterCity
    {

        [Key]
        public int mCityId { get; set; }

        [Required(ErrorMessage = "please enter city name")]
        public string? mCityName { get; set; }

        [Required(ErrorMessage = "please enter order number")]
        public int? mCityOrderNo { get; set; }

        [Required(ErrorMessage = "please select district")]
        [ForeignKey("MasterDistrict")]
        public int mCityDistrictId { get; set; }

        public int mCityIsActive { get; set; } = 1;
        public DateTime mCityInsDate { get; set; } = DateTime.Now;

        public MasterDistrict? MasterDistrict { get; set; }
    }
}