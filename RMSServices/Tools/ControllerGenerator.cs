using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RMSServices.Tools
{
    public class ControllerGenerator
    {
        private readonly ApplicationPartManager _partManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ControllerGenerator(
            ApplicationPartManager partManager,
            IHostingEnvironment env)
        {
            _partManager = partManager;
            _hostingEnvironment = env;
        }

        public bool AppendController(string className)
        {
            var generator = new WebApiGenerator();

            Assembly assembly = generator.Exists(className) ?
                generator.GetAssembly(className) : generator.CreateDll(className);

            if (assembly != null)
            {
                _partManager.ApplicationParts.Add(new AssemblyPart(assembly));
                // Notify change
                MyActionDescriptorChangeProvider.Instance.HasChanged = true;
                MyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
                return true;
            }
            return false;
        }
    }
}
