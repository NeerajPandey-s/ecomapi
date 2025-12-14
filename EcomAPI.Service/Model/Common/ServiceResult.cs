namespace EcomAPI.Service.Model.Common
{
    public class ServiceResult
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public dynamic? ResultData { get; set; }
        public int StatusCode { get; set; }
    }
}