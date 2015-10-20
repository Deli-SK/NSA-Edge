using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace NSA.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            //Create the CompositionContainer with the parts in the catalog
            var container = new CompositionContainer(new AssemblyCatalog(typeof(App).Assembly));

            //Fill the imports of this object
            try
            {
                container.ComposeParts();
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
