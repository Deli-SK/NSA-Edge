using System;
using System.Windows;

namespace NSA.WPF.Rendering.Engine.Entities
{
    public class Spring: IUpdatable
    {
        public IParticle A { get; }
        public IParticle B { get; }

        public double Length { get; set; }
        public double Strength { get; set; }

        public bool Retract { get; set; } = true;
        public bool Extend { get; set; } = true;


        public Spring(IParticle a, IParticle b, double length, double strength = 0.5f)
        {
            this.A = a;
            this.B = b;

            this.Length = length;
            this.Strength = strength;
        }

        public void Update(double delta)
        {
            var direction = this.B.Center - this.A.Center;

            if (Math.Abs(direction.Length) < double.Epsilon)
                direction = new Vector(double.Epsilon * Randomizer.Next() - 0.5d, double.Epsilon * Randomizer.Next() - 0.5d);

            var modifier = direction.Length - this.Length;

            direction.Normalize();

            if (modifier > 0 && this.Retract
                || modifier < 0 && this.Extend)
            {
                var force = direction * modifier * 0.5f * this.Strength;
                this.A.AddForce(force);
                this.B.AddForce(-force);
            }
        }
    }
}
