using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
	        var kernel = new StandardKernel();
			kernel.Bind<IOptions>().To<ConsoleOptions>();
			var options = kernel.Get<IOptions>();
			if (!CommandLine.Parser.Default.ParseArguments(args, options))
				return;
			
	        kernel.Bind<IAlgorithm>().To<AlphabeticTagCloudAlgorithm>()
				.WithConstructorArgument("options", options);
			kernel.Bind<IImageWriter>().To<ImageWriter>()
		        .WithConstructorArgument("path",options.GetOutputFile())
				.WithConstructorArgument("format", options.GetOutputFormat());
	        kernel.Bind<IWordsReader>().To<SimpleWordsReader>()
				.WithConstructorArgument("path",options.GetInputFile());
	        kernel.Bind<IWordsFilter>().To<PrepositionsFilter>();
	        kernel.Bind<ITagCloudBuilder>().To<TagCloudBuilder>();	
			kernel.Get<ITagCloudBuilder>().Build();


        }
    }										
}
