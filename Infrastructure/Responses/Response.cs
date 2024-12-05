using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Application.Responses
{
    public class Response<T>
    {
        public Response(ResponseStatus status)
        {
            SetStatus(status);
        }

        public Response(ResponseStatus status, List<string> errors)
        {
            SetStatus(status);
            Errors = errors;
        }

        public Response(ResponseStatus status, T data)
        {
            SetStatus(status);
            Data = data;
        }

        public Response(ResponseStatus status, List<string> errors, T data)
        {
            SetStatus(status);
            Errors = errors;
            Data = data;
        }

        public Response(ResponseStatus status, string message)
        {
            SetStatus(status);
            Message = message;
        }

        public Response(T data)
        {
            SetStatus(ResponseStatus.Succeed);
            Data = data;
        }

        public static JsonResult Succeed()
        {
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        private ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }

        public int StatusCode;

        public Response<T> GetResponse()
        {
            return this;
        }

        public JsonResult ToJsonResult()
        {
            return new(this) { StatusCode = StatusCode };
        }

        public ActionResult<T> ToActionResult()
        {
            return new(Data);
        }

        private void SetStatus(ResponseStatus status)
        {
            Status = status;

            switch (status)
            {
                case ResponseStatus.Failed:
                    Message = "خطا در تکمیل درخواست";
                    StatusCode = StatusCodes.Status500InternalServerError;
                    break;
                case ResponseStatus.Succeed:
                    Message = "با موفقیت انجام شد";
                    StatusCode = StatusCodes.Status200OK;
                    break;
                case ResponseStatus.NotFound:
                    Message = "اطلاعات مورد نظر یافت نشد";
                    StatusCode = StatusCodes.Status404NotFound;
                    break;
                case ResponseStatus.BadRequest:
                    Message = "خطا در اطلاعات ارسالی";
                    StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case ResponseStatus.Unauthorized:
                    Message = "کاربر نامعتبر";
                    StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case ResponseStatus.Forbidden:
                    Message = "اجازه دسترسی نامعتبر";
                    StatusCode = StatusCodes.Status403Forbidden;
                    break;
                default:
                    break;
            }
        }
    }
}
