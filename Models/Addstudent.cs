using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biohacker_Adminpanel.Models
{
    public class Addstudent
    {
            public int mspId { get; set; }
            public int mspinstituteId { get; set; }
            public int mspCourseId { get; set; }
            public int mspSessionId { get; set; }
            public long? mspStudentcode { get; set; } = 6001;
            public string? mspCourseDuration { get; set; }
            public DateTime mspSessionStartDate { get; set; }
            public DateTime mspSessionEndDate { get; set; }
            public int mspstatus { get; set; }
            public DateTime mspApproveDate { get; set; }
            public string? mspUniqueId { get; set; }
            public string? mspRegseries { get; set; }
          
        /// <summary>
        /// /registration
        /// </summary>
            public int msrId { get; set; }
            public int msrProfileId { get; set; }
            public string? msrName { get; set; }
            public string? msrfatherName { get; set; }
            public string? msrMotherName { get; set; }
            public string? msrEmail { get; set; }
            public long? msrMobileNo { get; set; }
            public DateTime msrDOB { get; set; } = DateTime.Now;
            public long? msrAadharNo { get; set; }
            public string? msrAddress { get; set; }
            public int msrStateId { get; set; }
            public int msrDistrictId { get; set; }
            public int msrCityId { get; set; }
            public int msrCategoryId { get; set; }
            public int msrGender { get; set; }
            public string? msrPhoto { get; set; }
            public IFormFile? StudentImage { get; set; }
            public string? msrDocumentImage { get; set; }
            public IFormFile? DocumentImage { get; set; }
            public int msrIActive { get; set; }
            public DateTime msrInsDate { get; set; } = DateTime.Now;
            public int msrInsBy { get; set; }

        [NotMapped] public string? InstituteName { get; set; }
        [NotMapped] public string? CourseName { get; set; }
        [NotMapped] public string? SessionName { get; set; }
        [NotMapped] public string? StateName { get; set; }
        [NotMapped] public string? DistrictName { get; set; }
        [NotMapped] public string? CityName { get; set; }
        [NotMapped] public string? CategoryName { get; set; }
        [NotMapped] public string? GenderName { get; set; }

    }
}

