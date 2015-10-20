using System.Windows;

namespace NSA.WPF.Rendering.Engine.Entities
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
