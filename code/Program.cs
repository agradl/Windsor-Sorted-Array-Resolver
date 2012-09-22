using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SortedArray.sample;

namespace SortedArray.code
{
    internal class Program
    {
        private static void Main()
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new SortedArrayResolver(container.Kernel, true, Utility.SortFunction));
            container.Register(AllTypes.FromAssemblyContaining<ILetter>()
                                   .BasedOn<ILetter>()
                                   .WithServiceFirstInterface())
                .Register(Component.For<LetterPrinter>());

            var printer = container.Resolve<LetterPrinter>();
            printer.Print();

            //var array = container.ResolveAll<ILetter>(); 

            // ResolveAll does not use the SortedArrayResolver
            //  since it is not actually resolving an array object
            //  it resolves all components matching the service and 
            //  builds an array from the results
        }

    }
}