using Microsoft.Extensions.DependencyInjection;

namespace _35multiplesum
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider provider = Configuration.ConfigureApplication();

            IMultiples multiples = provider.GetService<IMultiples>();

            if (args.Length > 0)
            {
                multiples.Run(args);
            }
            else
            {
                multiples.Run();
            }
        }
    }
}
