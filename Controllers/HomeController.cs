using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using state_district_city.Models;

namespace state_district_city.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MasterState(int? Id)
        {
            MasterState data = new MasterState();

            if (Id.HasValue && Id.Value != 0)
            {
                var mst = _dbContext.MasterState.FirstOrDefault(r => r.mStateId == Id.Value);
                if (mst != null)
                    data = mst;
            }

            ViewBag.Allstate = _dbContext.MasterState.ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult addstate(MasterState datas)
        {
            if (datas.mStateId == 0)
            {
                _dbContext.MasterState.Add(datas);
                TempData["msg"] = "Successfully Inserted";
            }
            else
            {
                _dbContext.MasterState.Update(datas);
                TempData["msg"] = "Successfully Updated";
            }

            _dbContext.SaveChanges();
            return RedirectToAction("MasterState");
        }

        public IActionResult delstate(int Id)
        {
            var a = _dbContext.MasterState.FirstOrDefault(r => r.mStateId == Id);
            if (a != null)
            {
                _dbContext.MasterState.Remove(a);
                _dbContext.SaveChanges();

                TempData["msg"] = "Successfully Deleted";
            }

            return RedirectToAction("MasterState");
        }

        //-------- end --------------


        //---------MasterDistrict---------

        public IActionResult MasterDistrict(int? Id)
        {
            MasterDistrict data = new MasterDistrict();

            if (Id.HasValue && Id.Value != 0)
            {
                var mtd = _dbContext.MasterDistrict.FirstOrDefault(r => r.mDistrictId == Id.Value);
                if (mtd != null)
                    data = mtd;
            }

            ViewBag.Allstate = new SelectList(_dbContext.MasterState.ToList(), "mStateId", "mStateName");
            ViewBag.AllDistrict = _dbContext.MasterDistrict.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult addDistrict(MasterDistrict datas)
        {
            if (datas.mDistrictId == 0)
            {
                _dbContext.MasterDistrict.Add(datas);
                TempData["msg"] = "Successfully Inserted";
            }
            else
            {
                _dbContext.MasterDistrict.Update(datas);
                TempData["msg"] = "Successfully Updated";
            }

            _dbContext.SaveChanges();
            return RedirectToAction("MasterDistrict");
        }

        public IActionResult deldistrict(int Id)
        {
            var mtyped = _dbContext.MasterDistrict.FirstOrDefault(r => r.mDistrictId == Id);
            if (mtyped != null)
            {
                _dbContext.MasterDistrict.Remove(mtyped);
                _dbContext.SaveChanges();

                TempData["msg"] = "Successfully Deleted";
            }

            return RedirectToAction("MasterDistrict");
        }

        //-------- end --------------


        //---------MasterCity---------
        public IActionResult MasterCity(int? Id)
        {
            MasterCity data = new MasterCity();

            if (Id.HasValue && Id.Value != 0)
            {
                var mci = _dbContext.MasterCity.FirstOrDefault(r => r.mCityId == Id.Value);
                if (mci != null)
                    data = mci;
            }

            ViewBag.AllDistrict = new SelectList(_dbContext.MasterDistrict.ToList(), "mDistrictId", "mDistrictName");
            ViewBag.Allcity = _dbContext.MasterCity.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult addcity(MasterCity datas)
        {
            if (datas.mCityId == 0)
            {
                _dbContext.MasterCity.Add(datas);
                TempData["msg"] = "Successfully Inserted";
            }
            else
            {
                _dbContext.MasterCity.Update(datas);
                TempData["msg"] = "Successfully Updated";
            }

            _dbContext.SaveChanges();
            return RedirectToAction("MasterCity");
        }

        public IActionResult delcity(int Id)
        {
            var mtyped = _dbContext.MasterCity.FirstOrDefault(r => r.mCityId == Id);
            if (mtyped != null)
            {
                _dbContext.MasterCity.Remove(mtyped);
                _dbContext.SaveChanges();

                TempData["msg"] = "Successfully Deleted";
            }

            return RedirectToAction("MasterCity");
        }

        //-------- end --------------


        //---------MasterStudentProfile---------

        public IActionResult MasterStudentProfile(int? Id)
        {
            var model = new Addstudent();

            ViewBag.Allinstitute = new SelectList(_dbContext.MasterInstitute.Where(r => r.mInstituteId == 1), "mInstituteId", "mInstituteName");
            ViewBag.AllCourse = new SelectList(_dbContext.MasterCourse.ToList(), "mCourseId", "mCourseName");
            ViewBag.Allsession = new SelectList(_dbContext.MasterSession.ToList(), "mSessionId", "mSessionName");
            ViewBag.Allstate = new SelectList(_dbContext.MasterState.ToList(), "mStateId", "mStateName");
            ViewBag.AllDistrict = new SelectList(_dbContext.MasterDistrict.ToList(), "mDistrictId", "mDistrictName");
            ViewBag.Allcity = new SelectList(_dbContext.MasterCity.ToList(), "mCityId", "mCityName");
            ViewBag.CategoryList = new SelectList(_dbContext.MasterTypeDetail.Where(r => r.mTypeId == 2), "mtDetailId", "mtDetailName");
            ViewBag.GenderList = new SelectList(_dbContext.MasterTypeDetail.Where(r => r.mTypeId == 3), "mtDetailId", "mtDetailName");


            if (Id.HasValue && Id.Value != 0)
            {
                var existing = (from p in _dbContext.MasterStudentProfile
                                join r in _dbContext.MasterStudentRegistration on p.mspId equals r.msrProfileId
                                join i in _dbContext.MasterInstitute on p.mspinstituteId equals i.mInstituteId into inst
                                from i in inst.DefaultIfEmpty()
                                join c in _dbContext.MasterCourse on p.mspCourseId equals c.mCourseId into course
                                from c in course.DefaultIfEmpty()
                                join s in _dbContext.MasterSession on p.mspSessionId equals s.mSessionId into sess
                                from s in sess.DefaultIfEmpty()
                                join st in _dbContext.MasterState on r.msrStateId equals st.mStateId into state
                                from st in state.DefaultIfEmpty()
                                join d in _dbContext.MasterDistrict on r.msrDistrictId equals d.mDistrictId into dist
                                from d in dist.DefaultIfEmpty()
                                join ci in _dbContext.MasterCity on r.msrCityId equals ci.mCityId into city
                                from ci in city.DefaultIfEmpty()
                                join cat in _dbContext.MasterTypeDetail on r.msrCategoryId equals cat.mtDetailId into catg
                                from cat in catg.DefaultIfEmpty()
                                join g in _dbContext.MasterTypeDetail on r.msrGender equals g.mtDetailId into gen
                                from g in gen.DefaultIfEmpty()
                                where p.mspId == Id.Value
                                select new Addstudent
                                {
                                    mspId = p.mspId,
                                    mspinstituteId = p.mspinstituteId,
                                    mspCourseId = p.mspCourseId,
                                    mspSessionId = p.mspSessionId,
                                    mspStudentcode = p.mspStudentcode,
                                    mspCourseDuration = p.mspCourseDuration,
                                    mspSessionStartDate = p.mspSessionStartDate,
                                    mspSessionEndDate = p.mspSessionEndDate,
                                    mspstatus = p.mspstatus,
                                    mspApproveDate = p.mspApproveDate,
                                    mspUniqueId = p.mspUniqueId,
                                    mspRegseries = p.mspRegseries,
                                    msrId = r.msrId,
                                    msrProfileId = r.msrProfileId,
                                    msrName = r.msrName,
                                    msrfatherName = r.msrfatherName,
                                    msrMotherName = r.msrMotherName,
                                    msrEmail = r.msrEmail,
                                    msrMobileNo = r.msrMobileNo,
                                    msrDOB = r.msrDOB,
                                    msrAadharNo = r.msrAadharNo,
                                    msrAddress = r.msrAddress,
                                    msrStateId = r.msrStateId,
                                    msrDistrictId = r.msrDistrictId,
                                    msrCityId = r.msrCityId,
                                    msrCategoryId = r.msrCategoryId,
                                    msrGender = r.msrGender,
                                    msrPhoto = r.msrPhoto,
                                    msrDocumentImage = r.msrDocumentImage,
                                    InstituteName = i != null ? i.mInstituteName : null,
                                    CourseName = c != null ? c.mCourseName : null,
                                    SessionName = s != null ? s.mSessionName : null,
                                    StateName = st != null ? st.mStateName : null,
                                    DistrictName = d != null ? d.mDistrictName : null,
                                    CityName = ci != null ? ci.mCityName : null,
                                    CategoryName = cat != null ? cat.mtDetailName : null,
                                    GenderName = g != null ? g.mtDetailName : null
                                }).FirstOrDefault();

                if (existing != null)
                    model = existing;
            }

            ViewBag.allstu = new List<Addstudent>();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> insertstudata(Addstudent model)
        {
            if (model.mspId == 0)
            {
                // =============== INSERT SECTION ===============
                var stuCode = _dbContext.MasterStudentProfile.Any() &&
                              _dbContext.MasterStudentProfile.Max(x => x.mspStudentcode) != null
                    ? (int)_dbContext.MasterStudentProfile.Max(x => x.mspStudentcode).Value + 1
                    : 6001;

                DateTime sessionStartDate = FixSqlDate(model.mspSessionStartDate);
                int durationMonths = 0;
                if (!string.IsNullOrEmpty(model.mspCourseDuration))
                    int.TryParse(model.mspCourseDuration, out durationMonths);

                if (durationMonths <= 0)
                    durationMonths = 6;

                DateTime computedSessionEnd = FixSqlDate(sessionStartDate.AddMonths(durationMonths));

                var stumodel = new MasterStudentProfile
                {
                    mspinstituteId = model.mspinstituteId,
                    mspSessionId = model.mspSessionId,
                    mspCourseId = model.mspCourseId,
                    mspStudentcode = stuCode,
                    mspCourseDuration = model.mspCourseDuration,
                    mspSessionStartDate = sessionStartDate,
                    mspSessionEndDate = computedSessionEnd,
                    mspstatus = 1,
                    mspApproveDate = DateTime.Now,
                    mspUniqueId = stuCode.ToString(),
                    mspRegseries = model.mspRegseries,
                    mspIActive = 1,
                    mspInsDate = DateTime.Now,
                    mspInsBy = 1
                };

                _dbContext.MasterStudentProfile.Add(stumodel);
                await _dbContext.SaveChangesAsync();

                // === Upload Student Photo ===
                if (model.StudentImage != null && model.StudentImage.Length > 0)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.StudentImage.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.StudentImage.CopyToAsync(stream);
                    }

                    model.msrPhoto = "/uploads/" + uniqueFileName;
                }

                // === Upload Document Image ===
                if (model.DocumentImage != null && model.DocumentImage.Length > 0)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.DocumentImage.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.DocumentImage.CopyToAsync(stream);
                    }

                    model.msrDocumentImage = "/uploads/" + uniqueFileName;
                }

                // === Save Registration Data ===
                var register = new MasterStudentRegistration
                {
                    msrStateId = model.msrStateId,
                    msrDistrictId = model.msrDistrictId,
                    msrGender = model.msrGender,
                    msrCategoryId = model.msrCategoryId,
                    msrCityId = model.msrCityId,
                    msrProfileId = stumodel.mspId,
                    msrName = model.msrName,
                    msrfatherName = model.msrfatherName,
                    msrMotherName = model.msrMotherName,
                    msrEmail = model.msrEmail,
                    msrMobileNo = model.msrMobileNo,
                    msrAadharNo = model.msrAadharNo,
                    msrDOB = FixSqlDate(model.msrDOB),
                    msrAddress = model.msrAddress,
                    msrPhoto = model.msrPhoto,
                    msrDocumentImage = model.msrDocumentImage,
                    msrIActive = 1,
                    msrInsDate = DateTime.Now,
                    msrInsBy = 1
                };

                _dbContext.MasterStudentRegistration.Add(register);
                await _dbContext.SaveChangesAsync();

                TempData["msg"] = "Student added successfully";
            }
            else
            {
                // =============== UPDATE SECTION ===============
                var profile = await _dbContext.MasterStudentProfile.FindAsync(model.mspId);
                var registration = await _dbContext.MasterStudentRegistration
                    .FirstOrDefaultAsync(r => r.msrProfileId == model.mspId);

                if (profile == null || registration == null)
                    return NotFound();

                // === Handle Student Photo Update ===
                if (model.StudentImage != null && model.StudentImage.Length > 0)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.StudentImage.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.StudentImage.CopyToAsync(stream);
                    }

                    model.msrPhoto = "/uploads/" + uniqueFileName;
                }
                else
                {
                    // Keep existing photo path
                    model.msrPhoto = registration.msrPhoto;
                }

                // === Handle Document Image Update ===
                if (model.DocumentImage != null && model.DocumentImage.Length > 0)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.DocumentImage.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.DocumentImage.CopyToAsync(stream);
                    }

                    model.msrDocumentImage = "/uploads/" + uniqueFileName;
                }
                else
                {
                    // Keep existing document image path
                    model.msrDocumentImage = registration.msrDocumentImage;
                }

                // === Update Profile ===
                profile.mspCourseDuration = model.mspCourseDuration;
                profile.mspSessionStartDate = FixSqlDate(model.mspSessionStartDate);
                profile.mspSessionEndDate = FixSqlDate(model.mspSessionEndDate);
                profile.mspstatus = model.mspstatus;
                profile.mspApproveDate = DateTime.Now;
                profile.mspUniqueId = model.mspUniqueId;
                profile.mspRegseries = model.mspRegseries;
                profile.mspInsDate = DateTime.Now;
                _dbContext.MasterStudentProfile.Update(profile);

                // === Update Registration ===
                registration.msrName = model.msrName;
                registration.msrfatherName = model.msrfatherName;
                registration.msrMotherName = model.msrMotherName;
                registration.msrEmail = model.msrEmail;
                registration.msrMobileNo = model.msrMobileNo;
                registration.msrAadharNo = model.msrAadharNo;
                registration.msrDOB = FixSqlDate(model.msrDOB);
                registration.msrAddress = model.msrAddress;
                registration.msrGender = model.msrGender;
                registration.msrPhoto = model.msrPhoto;
                registration.msrDocumentImage = model.msrDocumentImage;
                registration.msrInsDate = DateTime.Now;

                _dbContext.MasterStudentRegistration.Update(registration);
                await _dbContext.SaveChangesAsync();

                TempData["msg"] = "Student updated successfully";
            }

            return RedirectToAction("MasterStudentProfile");
        }

        public async Task<IActionResult> delstu(int Id)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var profile = await _dbContext.MasterStudentProfile
                .FirstOrDefaultAsync(x => x.mspId == Id);

            var registration = await _dbContext.MasterStudentRegistration
                .FirstOrDefaultAsync(x => x.msrProfileId == Id);

            if (profile != null)
                _dbContext.MasterStudentProfile.Remove(profile);

            if (registration != null)
                _dbContext.MasterStudentRegistration.Remove(registration);



            TempData["msg"] = "Successfully Deleted";

            return RedirectToAction("studentlists");
        }



        // ? Universal date fixer
        private DateTime FixSqlDate(DateTime? date)
        {
            if (!date.HasValue ||
                date.Value < (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue ||
                date.Value > (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue)
            {
                return DateTime.Now;
            }

            return date.Value;
        }

        public async Task<IActionResult> studentlists(int id)
        {
            var list = await (from p in _dbContext.MasterStudentProfile
                              join r in _dbContext.MasterStudentRegistration on p.mspId equals r.msrProfileId
                              join i in _dbContext.MasterInstitute on p.mspinstituteId equals i.mInstituteId into inst
                              from i in inst.DefaultIfEmpty()
                              join c in _dbContext.MasterCourse on p.mspCourseId equals c.mCourseId into course
                              from c in course.DefaultIfEmpty()
                              join s in _dbContext.MasterSession on p.mspSessionId equals s.mSessionId into sess
                              from s in sess.DefaultIfEmpty()
                              join st in _dbContext.MasterState on r.msrStateId equals st.mStateId into state
                              from st in state.DefaultIfEmpty()
                              join d in _dbContext.MasterDistrict on r.msrDistrictId equals d.mDistrictId into dist
                              from d in dist.DefaultIfEmpty()
                              join ci in _dbContext.MasterCity on r.msrCityId equals ci.mCityId into city
                              from ci in city.DefaultIfEmpty()
                              join cat in _dbContext.MasterTypeDetail on r.msrCategoryId equals cat.mtDetailId into catg
                              from cat in catg.DefaultIfEmpty()
                              join g in _dbContext.MasterTypeDetail on r.msrGender equals g.mtDetailId into gen
                              from g in gen.DefaultIfEmpty()
                              select new Addstudent
                              {
                                  mspId = p.mspId,
                                  mspinstituteId = p.mspinstituteId,
                                  mspCourseId = p.mspCourseId,
                                  mspSessionId = p.mspSessionId,
                                  mspStudentcode = p.mspStudentcode,
                                  mspCourseDuration = p.mspCourseDuration,
                                  mspSessionStartDate = p.mspSessionStartDate,
                                  mspSessionEndDate = p.mspSessionEndDate,
                                  mspstatus = p.mspstatus,
                                  mspApproveDate = p.mspApproveDate,
                                  mspUniqueId = p.mspUniqueId,
                                  mspRegseries = p.mspRegseries,
                                  msrId = r.msrId,
                                  msrProfileId = r.msrProfileId,
                                  msrName = r.msrName,
                                  msrfatherName = r.msrfatherName,
                                  msrMotherName = r.msrMotherName,
                                  msrEmail = r.msrEmail,
                                  msrMobileNo = r.msrMobileNo,
                                  msrDOB = r.msrDOB,
                                  msrAadharNo = r.msrAadharNo,
                                  msrAddress = r.msrAddress,
                                  msrStateId = r.msrStateId,
                                  msrDistrictId = r.msrDistrictId,
                                  msrCityId = r.msrCityId,
                                  msrCategoryId = r.msrCategoryId,
                                  msrGender = r.msrGender,
                                  msrPhoto = r.msrPhoto,
                                  msrDocumentImage = r.msrDocumentImage,

                                  // Display Names
                                  InstituteName = i != null ? i.mInstituteName : null,
                                  CourseName = c != null ? c.mCourseName : null,
                                  SessionName = s != null ? s.mSessionName : null,
                                  StateName = st != null ? st.mStateName : null,
                                  DistrictName = d != null ? d.mDistrictName : null,
                                  CityName = ci != null ? ci.mCityName : null,
                                  CategoryName = cat != null ? cat.mtDetailName : null,
                                  GenderName = g != null ? g.mtDetailName : null
                              }).ToListAsync();

            return View(list);
        }

        //-------- end --------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
