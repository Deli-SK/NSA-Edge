using System.Windows;

namespace NSA.WPF.Views.Rendering.Engine.Entities.Impl
{
    public class Anchor: IParticle
    {
        public Point Center { get; set; }

        public Anchor(Point center)
        {
            this.Center = center;
        }

        public void AddForce(Vector force)
        {
        }
    }
}
