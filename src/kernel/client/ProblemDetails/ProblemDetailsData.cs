using System.Net;

// ReSharper disable once CheckNamespace
namespace TriPower;

public class ProblemDetailsData (
    string type,
    string title,
    HttpStatusCode status,
    string? detail,
    Dictionary<string, string[]>? errors)
{
    public string Type { get; private set; } = type;
    public string Title { get; private set; } = title;
    public string Message { get; private set; } = detail ?? title;
    public HttpStatusCode Status { get; private set; } = status;
    public string? Detail { get; private set; } = detail;
    public Dictionary<string, string[]>? Errors { get; private set; } = errors;
}