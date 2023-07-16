using Newtonsoft.Json;

namespace School.Web.ViewModel
{

    public class TodoVM
    {
        [JsonProperty("Result")]
        public TodoResult result { get; set; }
        public bool succeeded { get; set; }
        public string message { get; set; }
       
    }
    public class TodoResult
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public TodoData[] data { get; set; }
    }
    public class TodoData
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public bool isCompleted { get; set; }
        public DateTime? dateCompleted { get; set; }
    }

}
