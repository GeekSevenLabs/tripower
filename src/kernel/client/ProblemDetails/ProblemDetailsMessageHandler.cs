using System.Net;
using System.Net.Http.Json;

// ReSharper disable once CheckNamespace
namespace TriPower;

public class ProblemDetailsMessageHandler(NavigationManager navigationManager) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpResponseMessage = await base.SendAsync(request, cancellationToken);
        
        if (httpResponseMessage.StatusCode == HttpStatusCode.OK) return httpResponseMessage;
        
        ProblemDetailsData? problemDetailsData;
        if (httpResponseMessage.Content.Headers.ContentType?.MediaType == "application/problem+json")
        {
            problemDetailsData = await httpResponseMessage
                .Content
                .ReadFromJsonAsync<ProblemDetailsData>(cancellationToken: cancellationToken);
            
            var ex = new InvalidOperationException(problemDetailsData!.Message);
            ex.ChangeProblemDetailsData(problemDetailsData);
        }
        
        var title = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Unauthorized => "É necessário autenticar para realizar esta operação",
            HttpStatusCode.BadRequest => "Operação inválida",
            HttpStatusCode.Forbidden => "Sem permissão para realizar esta operação",
            HttpStatusCode.NotFound => "A operação solicitada não existe",
            _ => "Ocorreu um erro inesperado"
        };
        
        var detail = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        detail = string.IsNullOrEmpty(detail) ? "Nenhum detalhe adicional." : detail;
        
        // Se chamou uma API sem ter autorização, redireciona para a tela de login
        if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (navigationManager.Uri.Contains("account/login")) { return httpResponseMessage; }
            navigationManager.NavigateTo("/account/login", forceLoad: true);
            return httpResponseMessage;
        }
        
        problemDetailsData = new ProblemDetailsData("0", title, httpResponseMessage.StatusCode, detail, null);
        throw new InvalidOperationException(problemDetailsData.Message)
        {
            Data = { ["ProblemDetailsData"] = problemDetailsData }
        };
    }
}