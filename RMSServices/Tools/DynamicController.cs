using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using RMSModel.Models;
using RMSServices.Controllers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace RMSServices.Tools
{
    public class RemoteControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
            sb.AppendLine("using RMSModel.Models;");
            sb.AppendLine("namespace RMSServices.Controllers;");
            //sb.AppendLine("[ApiController]");
            sb.AppendLine("[Route(\"[controller]/[action]\")]");
            sb.AppendLine("public class {classname}Controller : CrudController<{classname}>");
            sb.AppendLine("{");
            sb.AppendLine("public {classname}Controller() : base(db => db.{classname}){ }");
            sb.AppendLine("}");


            string codeTemplate = sb.ToString();
            //var remoteCode = File.ReadAllText(@"D:\Agung\Personal\Learn\RMSMiddlewareVersion\RMSServices\RMSServices\Tools\TextFile.txt");
            RMSContext db = new RMSContext();
            var entityTypes = db.Model.GetEntityTypes().Select(t => t.ClrType).ToList();
            //var remoteCode = new HttpClient().GetStringAsync("https://gist.githubusercontent.com/filipw/9311ce866edafde74cf539cbd01235c9/raw/6a500659a1c5d23d9cfce95d6b09da28e06c62da/types.txt").GetAwaiter().GetResult();
            var references = new List<MetadataReference>
                    {
                        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                        MetadataReference.CreateFromFile(typeof(RemoteControllerFeatureProvider).Assembly.Location),
                        MetadataReference.CreateFromFile(typeof(ControllerFeature).Assembly.Location),
                        //MetadataReference.CreateFromFile(typeof(RMSServices.Controllers.MainController).Assembly.Location),
                        MetadataReference.CreateFromFile(typeof(RMSModel.Models.RMSContext).Assembly.Location)

                    };

            var referencedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var referenced in referencedAssemblies)
            {
                string location = null;
                try
                {
                    location = referenced.Location;
                }
                catch
                {
                }
                if (location != null)
                {
                    references.Add(MetadataReference.CreateFromFile(location));
                }
            }


            foreach (var item in entityTypes)
            {
                string code = codeTemplate.Replace("{classname}", item.Name);

                var compilation = CSharpCompilation.Create("DynamicAssembly",
                    new[] { CSharpSyntaxTree.ParseText(code) },
                    references,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                using (var ms = new MemoryStream())
                {
                    var emitResult = compilation.Emit(ms);

                    if (!emitResult.Success)
                    {
                        // handle, log errors etc
                        Debug.WriteLine("Compilation failed!");
                        return;
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                    //var assembly = Assembly.Load(ms.ToArray());
                    var assembly = Assembly.Load(ms.ToArray());

                    var candidates = assembly.GetExportedTypes();

                    parts.Append(new AssemblyPart(assembly));
                    foreach (var candidate in candidates)
                    {
                        feature.Controllers.Add(IntrospectionExtensions.GetTypeInfo(candidate)
                        );
                    }

                    //feature.Controllers.
                    //foreach (var candidate in candidates)
                    //{
                    //    feature.Controllers.Add(
                    //        typeof(CrudController<candidate>).MakeGenericType(candidate).GetTypeInfo()
                    //    );
                    //}

                }
            }

        }
    }
}
