using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A vector field with four real number components.
    /// </summary>
    public sealed partial class Vector4Field : FloatVectorField<Vector4>
    {
        /* Public properties. */
        public override Vector4 Value
        {
            get => new Vector4(X.Value, Y.Value, Z.Value, W.Value);
            set
            {
                X.Value = value.X;
                Y.Value = value.Y;
                Z.Value = value.Z;
                W.Value = value.W;
            }
        }

        /* Private properties. */
        private FloatField X { get; set; }
        private FloatField Y { get; set; }
        private FloatField Z { get; set; }
        private FloatField W { get; set; }

        /* Constructors. */
        public Vector4Field() : base() { }

        public Vector4Field(float height, float labelWidth, string labelText, Vector4 value)
            : base(height, labelWidth, labelText, value) { }

        public Vector4Field(Vector4Field other) : base(other) { }

        /* Public methods. */
        public override Vector4Field Duplicate()
        {
            return new Vector4Field(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Vector field init.
            base.Init();

            // Set name.
            Name = "Vector4Field";

            // Create float fields.
            X = CreateField(0f, "X", 0f);
            Y = CreateField(0f, "Y", 0f);
            Z = CreateField(0f, "Z", 0f);
            W = CreateField(0f, "W", 0f);
        }
    }
}