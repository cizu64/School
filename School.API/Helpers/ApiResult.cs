namespace School.API.Helpers
{
    public class ApiResult
    {
        public dynamic Result { get; set; }
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; }
    }
}
