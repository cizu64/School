using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using School.Web.Helpers;
using School.Web.Services;

namespace School.Web.ViewModel
{

    public class StudentVM
    {
        [JsonProperty("Result")]
        public StudentResult result { get; set; }
        public bool succeeded { get; set; }
        public string message { get; set; }
    }

    public class StudentResult
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public StudentAddress studentAddress { get; set; }
        public StudentCourse[] studentCourses { get; set; }
        public int id { get; set; }
    }

    public class StudentAddress
    {
        public string city { get; set; }
        public string street { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }

    public class StudentCourse
    {
        public int studentId { get; set; }
        public int courseId { get; set; }
        public int id { get; set; }
    }

}
