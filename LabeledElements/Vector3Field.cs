using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A vector field with three real number components.
    /// </summary>
    public sealed partial class Vector3Field : FloatVectorField<Vector3>
    {
        /* Public properties. */
        public override Vector3 Value
        {
            get => new Vector3(X.Value, Y.Value, Z.Value);
            set
            {
                X.Value = value.X;
                Y.Value = value.Y;
                Z.Value = value.Z;
            }
        }

        /* Private properties. */
        private FloatField X { get; set; }
        private FloatField Y { get; set; }
        private FloatField Z { get; set; }

        /* Constructors. */
        public Vector3Field() : base() { }

        public Vector3Field(float height, float labelWidth, string labelText, Vector3 value)
            : base(height, labelWidth, labelText, value) { }

        public Vector3Field(Vector3Field other) : base(other) { }

        /* Public methods. */
        public override Vector3Field Duplicate()
        {
            return new Vector3Field(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Vector field init.
            base.Init();

            // Set name.
            Name = "Vector3Field";

            // Create float fields.
            X = CreateField(0f, "X", 0f);
            Y = CreateField(0f, "Y", 0f);
            Z = CreateField(0f, "Z", 0f);
        }
    }
}