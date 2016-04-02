using System.Windows;

namespace NSA.Visualization.Elements
{
    public interface IRigidBody
    {
        Point Position { get; }

        void AddForce(Vector force);
    }
}