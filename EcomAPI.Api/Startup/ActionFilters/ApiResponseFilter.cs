using EcomAPI.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcomAPI.Api.Startup.ActionFilters
{
    internal class ApiResponseFilter : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Result is ObjectResult objectResult)
            {
                var response = objectResult.Value;

                // You can modify the response object as needed, here we wrap it into ApiResponse
                if (response != null)
                {
                    var apiResponse = new ApiResponse<object>(!(response is ValidationFailureResponse), "", response);
                    objectResult.Value = apiResponse;
                }
                else
                {
                    // Handle the case when the response is null
                    var apiResponse = new ApiResponse<object>(false, "No data found", null!);
                    objectResult.Value = apiResponse;
                }
            }
        }


        public class ApiResponse<T>(bool success, string message, T data)
        {
            public bool Success { get; set; } = success;
            public string Message { get; set; } = message;
            public T Data { get; set; } = data;
        }
    }
}
