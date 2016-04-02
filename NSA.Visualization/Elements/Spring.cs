namespace NSA.Visualization.Elements
{
    public class Spring: IUpdatable
    {
        public IRigidBody From { get; }
        public IRigidBody To { get; }

        public float Length { get; set; }
        public float Strength { get; set; }

        public Spring(IRigidBody @from, IRigidBody to, float length, float strength = 1)
        {
            this.From = @from;
            this.To = to;
            this.Length = length;
        }

        public void Tick(float delay)
        {
            var direction = this.To.Position - this.From.Position;
            var force = (direction / 2) * (this.Strength / 100);
            this.From.AddForce(force);
            this.To.AddForce(- force);
        }
    }
}
