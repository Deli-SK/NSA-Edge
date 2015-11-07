using System.Windows.Input;

namespace NSA.WPF.Common.Extensions
{
    public static class CommandExtensions
    {
        public static void Execute(this ICommand cmd)
        {
            cmd.Execute(null);
        }
    }
}
