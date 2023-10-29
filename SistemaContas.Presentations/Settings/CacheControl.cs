namespace SistemaContas.Presentations.Settings
{
    //Classe utilizadada para executar limpeza do cache do navegador.
    public class CacheControl
    {
        //objeto RequestDelegate permite executar uma ação no sistema sempre que uma página ou requisição for aberta.
        private readonly RequestDelegate? _requestDelegate;

        /// <summary>
        /// Énecessário criar um construtor pasra que o ASP.NET inicialize esse atributo.
        /// DI
        /// </summary>
        /// <param name="requestDelegate"></param>
        public CacheControl(RequestDelegate? requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        /// <summary>
        /// Esse método é executado antes de que qualquer página do sistema seja aberta.
        /// </summary>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            //Limpar o cache do navegador
            httpContext.Response.OnStarting((state) =>
            {
                httpContext.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                httpContext.Response.Headers.Append("Pragma", "no-cache");
                httpContext.Response.Headers.Append("Expires", "0");

                return Task.FromResult(0);
            }, 0);

            await _requestDelegate.Invoke(httpContext);
        }

    }
}
