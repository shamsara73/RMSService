using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace RMSServices.Tools
{
    public class WebApiGenerator
    {
        public WebApiGenerator()
        {
        }

        private static CSharpCompilation GenerateCode(string sourceCode, string className)
        {
            var codeString = SourceText.From(sourceCode);
            //var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.);

            var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString);

            var references = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(RMSModel.Models.RMSContext).Assembly.Location),

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

            string outputDll = className + ".dll";
            return CSharpCompilation.Create(outputDll,
                new[] { parsedSyntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
        }

        public bool Exists(string className)
        {
            string outputDll = className + ".dll";
            return File.Exists(outputDll);
        }

        public Assembly GetAssembly(string className)
        {
            string outputDll = className + ".dll";
            return Assembly.LoadFrom(outputDll);
        }

        public Assembly CreateDll(string className)
        {
            className = className.Replace(" ", string.Empty);

            string outputDll = className + ".dll";
            if (File.Exists(outputDll)) return Assembly.LoadFrom(outputDll);

            string code = File.ReadAllText(@"D:\Agung\Personal\Learn\RMSMiddlewareVersion\RMSServices\RMSServices\Tools\TextFile.txt");

            code = code.Replace("{classname}", className);
            
            File.WriteAllText("code.txt", code.ToString());

            var result = GenerateCode(code.ToString(), className).Emit(outputDll);
            //CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, code.ToString());
            if (!result.Success)
            {
                Console.WriteLine("Compilation done with error.");
                var failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures)
                {
                    Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                }
            }
            else
            {
                Console.WriteLine("Build Succeeded");
                return Assembly.LoadFrom(outputDll);
            }

            return null;
        }
    }
}
