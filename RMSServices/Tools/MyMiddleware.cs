namespace RMSServices.Tools
{
    public class MyMiddleware
{
    public MyMiddleware(RequestDelegate requestDel, ControllerGenerator generator)
    {
        RequestDel = requestDel;
        Generator = generator;
    }

    public RequestDelegate RequestDel { get; }
    public ControllerGenerator Generator { get; }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.HasValue)
        {
            var queryParams = context.Request.Path.Value;
            var entityName = queryParams.Split("/").Where(s => !string.IsNullOrEmpty(s)).FirstOrDefault();
            var result = Generator.AppendController(entityName);
            Console.WriteLine(result + ", " + queryParams);
        }

        await RequestDel.Invoke(context);
    }
}
}
