using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using School.Web.Helpers;
using School.Web.Services;

namespace School.Web.ViewModel
{
    public class CourseVM
    {
        public Result result { get; set; }
        public bool succeeded { get; set; }
        public string message { get; set; }
    }

    public class Result
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public CourseData[] data { get; set; }
    }

    public class CourseData
    {
        [JsonIgnore]
        public int departmentId { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public int id { get; set; }
        public DepartmentData department { get; set; }
        public bool hasEnrolled { get; set; }
         
    }
    public class DepartmentData
    {
        public string name { get; set; }
        public bool isActive { get; set; }
        public int id { get; set; }

    }
}
