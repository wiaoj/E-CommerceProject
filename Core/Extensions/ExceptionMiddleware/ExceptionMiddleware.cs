using Core.Utilities.Messages;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions.ExceptionMiddleware {
	public class ExceptionMiddleware {
		private readonly RequestDelegate next;
		public ExceptionMiddleware(RequestDelegate next) => this.next = next;

		public async Task InvokeAsync(HttpContext httpContext) {
			try {
				await this.next(httpContext);
			} catch(Exception exception) {
				await HandleExceptionAsync(httpContext, exception);
			}
		}

		private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception) {
			httpContext.Response.ContentType = "application/json";
			//httpContext.Response.ContentType = "text/plain";
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			String message = ExceptionMessages.InternalServerError;

			//TODO: Code refactor exception middleware
			switch(exception) {
				//case SecurityException:
				//    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
				//    return httpContext.Response.WriteAsync(new SecurityErrorDetails {
				//        StatusCode = StatusCodes.Status401Unauthorized,
				//        Message = exception.Message
				//    }.ToJson());
				case ValidationException:
					httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
					return httpContext.Response.WriteAsync(new ValidationErrorDetails {
						StatusCode = StatusCodes.Status400BadRequest,
						Message = exception.Message,
						ValidationErrors = ((ValidationException)exception).Errors
					}.ToJson());
				case UnauthorizedAccessException:
					httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
					return httpContext.Response.WriteAsync("UnauthorizedAccessException ExceptionMiddleware.cs bak");
				case NotSupportedException:
					httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
					return httpContext.Response.WriteAsync("NotSupportedException ExceptionMiddleware.cs bak");
				case ApplicationException:
					httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
					return httpContext.Response.WriteAsync("ApplicationException ExceptionMiddleware.cs bak");
				default:
					return httpContext.Response.WriteAsync(new ErrorDetails() {
						StatusCode = httpContext.Response.StatusCode,
						//Message = message
						Message = exception.Message //=> sunucu hatalarını okumak için
					}.ToJson());
			};
		}
	}
}