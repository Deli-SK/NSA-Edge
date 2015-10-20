using System.Windows;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public interface IParticle
    {
        Point Center { get; }

        void AddForce(Vector force);
    }
}
