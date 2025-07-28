// ReSharper disable once CheckNamespace
namespace TriPower;

public static class InvalidOperationExceptionExtensions
{
    private const string ProblemDetailsDataKey = "ProblemDetailsData";
    
    public static ProblemDetailsData GetProblemDetailsData(this InvalidOperationException exception)
    {
        return 
            exception.Data[ProblemDetailsDataKey] as ProblemDetailsData ?? 
            new ProblemDetailsData("0", "Erro inesperado", 0, "Ocorreu algo inesperado na verificação de erros", []);
    }

    public static void ChangeProblemDetailsData(this InvalidOperationException exception, ProblemDetailsData problemDetailsData)
    {
        if (exception.Data.Contains(ProblemDetailsDataKey))
        {
            exception.Data[ProblemDetailsDataKey] = problemDetailsData;
        }
        else
        {
            exception.Data.Add(ProblemDetailsDataKey, problemDetailsData);
        }
    }

    public static bool ContainsProblemDetails(this InvalidOperationException exception)
    {
        return exception.Data.Contains(ProblemDetailsDataKey);
    }
}