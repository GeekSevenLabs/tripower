// ReSharper disable once CheckNamespace
namespace TriPower;

public static class ExceptionExtensions
{
    public static string GetTitle(this Exception exception)
    {
        const string title = "Algo deu errado";
        return exception switch
        {
            // Para acessar Handler anonimos é necessário registrar usando o HttpClient público
            // Senão pode cair aqui
            // case AccessTokenNotAvailableException: return "Não autorizado";
            // case ProblemDetailsException problemDetailsException:
            //     return problemDetailsException.Title switch
            //     {
            //         "One or more validation errors occurred." => "Verifique os dados",
            //         _ => problemDetailsException.Title ?? title,
            //     };
            HttpRequestException { Message: "TypeError: Failed to fetch" } => "Sem conexão",
            _ => title
        };
    }

    public static string GetMessage(this Exception exception)
    {
        switch (exception)
        {
            // Para acessar Handler anonimos é necessário registrar usando o HttpClient público
            // Senão pode cair aqui
            // case AccessTokenNotAvailableException: return "É necessário estar autenticado";
            // case ProblemDetailsException problemDetailsException:
            //     return problemDetailsException.Title switch
            //     {
            //         "One or more validation errors occurred."
            //             => string.Join("\n", problemDetailsException.Errors!.Values.SelectMany(message => message)),
            //         _ => exception.Message
            //     };
            case HttpRequestException { Message: "TypeError: Failed to fetch" }: return "Não foi possível conectar. Verifique sua conexão com a Internet.";
            default: // inesperado
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.GetType().Name);
                Console.WriteLine(exception.StackTrace);
                return exception.Message;
        }
    }
}