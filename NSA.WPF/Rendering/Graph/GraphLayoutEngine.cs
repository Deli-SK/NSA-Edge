using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using NSA.WPF.Business.Common;
using NSA.WPF.Rendering.Engine;
using NSA.WPF.Rendering.Engine.Entities;

namespace NSA.WPF.Rendering.Graph
{
    public class GraphLayoutEngine
    {
        private readonly Engine2D _engine;

        private readonly Anchor _center = new Anchor(new Point(400, 300));

        private readonly Dictionary<string, Particle> _particles = new Dictionary<string, Particle>();
        private readonly List<DrawableSpring> _springs = new List<DrawableSpring>();

        private readonly ParticleDrawer _termDrawer = new ParticleDrawer(Brushes.Red, new Pen(Brushes.Black, 1), 20);
        private readonly ParticleDrawer _sentenceDrawer = new ParticleDrawer(Brushes.Gold, new Pen(Brushes.Black, 1), 20);
        private readonly EdgedSpringDrawer _containEdgeDrawer = new EdgedSpringDrawer(new Pen(Brushes.Blue, 12) { EndLineCap = PenLineCap.Triangle }, new Pen(Brushes.Blue, 2) { DashStyle = DashStyles.Dash}, 20);
        private readonly EdgedSpringDrawer _explainEdgeDrawer = new EdgedSpringDrawer(new Pen(Brushes.Green, 12) {EndLineCap = PenLineCap.Triangle}, new Pen(Brushes.Green, 2) { DashStyle = DashStyles.Solid}, 20);
        private readonly DummyDrawer _noneEdgeDrawer = new DummyDrawer();

        public GraphLayoutEngine(Engine2D engine)
        {
            this._engine = engine;
        }

        public void Clear()
        {
            foreach (var particle in this._particles)
            {
                this._engine.Remove(particle.Value);
            }
            foreach (var spring in this._springs)
            {
                this._engine.Remove(spring);
            }
            this._particles.Clear();
            this._springs.Clear();
        }

        public void AddTerm(string term)
        {
            var newParticle = new Particle(this._termDrawer, GetNodeCenter(), term);

            foreach (var particle in this._particles.Values)
            {
                var edge = this.SetEdge(particle, newParticle, ConnectionType.None);
                this._engine.AddEntity(edge);
            }

            this.AddParticle(term, newParticle);
        }

        public void AddSentence(string sentence)
        {
            var newParticle = new Particle(this._sentenceDrawer, GetNodeCenter(), sentence);

            foreach (var particle in this._particles.Values)
            {
                var edge = this.SetEdge(newParticle, particle, ConnectionType.None);
                this._engine.AddEntity(edge);
            }

            this.AddParticle(sentence, newParticle);
        }

        public void Remove(string node)
        {
            var particle = this._particles[node];

            this._springs.RemoveAll(spring =>
            {
                if (spring.A == particle || spring.B == particle)
                {
                    this._engine.Remove(spring);
                    return true;
                }
                return false;
            });

            this._particles.Remove(node);
            this._engine.Remove(particle);
        }

        public void Connect(string sentence, string term, ConnectionType type)
        {
            this.SetEdge(sentence, term, type);
        }

        public void Disconnect(string sentence, string term)
        {
            this.SetEdge(sentence, term, ConnectionType.None);
        }

        private void AddParticle(string label, Particle newParticle)
        {
            var constraint = new Spring(this._center, newParticle, 0, 0.01);

            this._particles.Add(label, newParticle);
            this._engine.Add(constraint);
            this._engine.AddEntity(newParticle);
        }

        private DrawableSpring SetEdge(string sentence, string term, ConnectionType type)
        {
            var sentenceParticle = this._particles[sentence];
            var termParticle = this._particles[term];

            return this.SetEdge(sentenceParticle, termParticle, type);
        }

        private DrawableSpring SetEdge(IParticle from, IParticle to, ConnectionType type)
        {
            var edge = this._springs.FirstOrDefault(spring => spring.A == from && spring.B == to);

            if (edge == null)
            {
                edge = new DrawableSpring(null, from, to, 100);
                this._springs.Add(edge);
            }

            switch (type)
            {
                case ConnectionType.None:
                    edge.Renderer = this._noneEdgeDrawer;
                    edge.Length = 500;
                    edge.Retract = false;
                    edge.Strength = 0.01;
                    break;
                case ConnectionType.Containing:
                    edge.Renderer = this._containEdgeDrawer;
                    edge.Length = 100;
                    edge.Retract = true;
                    edge.Strength = 0.5;
                    break;
                case ConnectionType.Explaining:
                    edge.Renderer = this._explainEdgeDrawer;
                    edge.Length = 100;
                    edge.Retract = true;
                    edge.Strength = 0.5;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return edge;
        }

        private static Point GetNodeCenter()
        {
            return new Point(Randomizer.Next(200, 600), Randomizer.Next(200, 400));
        }
    }
}
