using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyRestaurant.Endpoint.WebApi.Models
{
    public sealed record ResponseModel<T>
    {
        public required HttpStatusCode StatusCode { get; init; }

        public bool Success => (int)StatusCode is >= 200 and < 300;

        public T? Data { get; init; }

        public ProblemDetails? Problem { get; init; }

        public static ResponseModel<T> Ok(T data) => new()
        {
            StatusCode = HttpStatusCode.OK,
            Data = data
        };

        public static ResponseModel<T> Created(T data) => new()
        {
            StatusCode = HttpStatusCode.Created,
            Data = data
        };

        public static ResponseModel<T> Accepted(T data) => new()
        {
            StatusCode = HttpStatusCode.Accepted,
            Data = data
        };

        public static ResponseModel<T> Fail(ProblemDetails problem) => new()
        {
            StatusCode = (HttpStatusCode)problem.Status!.Value,
            Problem = problem
        };
    }

    public sealed record ResponseModel
    {
        public required HttpStatusCode StatusCode { get; init; }

        public bool Success => (int)StatusCode is >= 200 and < 300;

        public ProblemDetails? Problem { get; init; }

        public static ResponseModel NoContent() => new()
        {
            StatusCode = HttpStatusCode.NoContent
        };

        public static ResponseModel Ok() => new()
        {
            StatusCode = HttpStatusCode.OK
        };

        public static ResponseModel Fail(ProblemDetails problem) => new()
        {
            StatusCode = (HttpStatusCode)problem.Status!.Value,
            Problem = problem
        };
    }
}
