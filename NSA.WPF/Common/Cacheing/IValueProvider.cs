namespace NSA.WPF.Common.Cacheing
{
    public interface IValueProvider<out T>
    {
        T Value { get; }
    }
}