using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A vector field with two real number components.
    /// </summary>
    public sealed partial class Vector2Field : FloatVectorField<Vector2>
    {
        /* Public properties. */
        public override Vector2 Value
        {
            get => new Vector2(X.Value, Y.Value);
            set
            {
                X.Value = value.X;
                Y.Value = value.Y;
            }
        }

        /* Private properties. */
        private FloatField X { get; set; }
        private FloatField Y { get; set; }

        /* Constructors. */
        public Vector2Field() : base() { }

        public Vector2Field(float height, float labelWidth, string labelText, Vector2 value)
            : base(height, labelWidth, labelText, value) { }

        public Vector2Field(Vector2Field other) : base(other) { }

        /* Public methods. */
        public override Vector2Field Duplicate()
        {
            return new Vector2Field(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Vector field init.
            base.Init();

            // Set name.
            Name = "Vector2Field";

            // Create float fields.
            X = CreateField(0f, "X", 0f);
            Y = CreateField(0f, "Y", 0f);
        }
    }
}