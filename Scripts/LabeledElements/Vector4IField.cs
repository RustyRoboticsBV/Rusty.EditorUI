using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A vector field with four integer number components.
    /// </summary>
    public sealed partial class Vector4IField : IntVectorField<Vector4I>
    {
        /* Public properties. */
        public override Vector4I Value
        {
            get => new Vector4I(X.Value, Y.Value, Z.Value, W.Value);
            set
            {
                X.Value = value.X;
                Y.Value = value.Y;
                Z.Value = value.Z;
                W.Value = value.W;
            }
        }

        /* Private properties. */
        private IntField X { get; set; }
        private IntField Y { get; set; }
        private IntField Z { get; set; }
        private IntField W { get; set; }

        /* Constructors. */
        public Vector4IField() : base() { }

        public Vector4IField(float height, float labelWidth, string labelText, Vector4I value)
            : base(height, labelWidth, labelText, value) { }

        public Vector4IField(Vector4IField other) : base(other) { }

        /* Public methods. */
        public override Vector4IField Duplicate()
        {
            return new Vector4IField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Vector field init.
            base.Init();

            // Set name.
            Name = "Vector4IField";

            // Create int fields.
            X = CreateField(0f, "X", 0);
            Y = CreateField(0f, "Y", 0);
            Z = CreateField(0f, "Z", 0);
            W = CreateField(0f, "W", 0);
        }
    }
}