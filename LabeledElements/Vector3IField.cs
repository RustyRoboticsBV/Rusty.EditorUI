using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A vector field with three integer number components.
    /// </summary>
    public sealed partial class Vector3IField : IntVectorField<Vector3I>
    {
        /* Public properties. */
        public override Vector3I Value
        {
            get => new Vector3I(X.Value, Y.Value, Z.Value);
            set
            {
                X.Value = value.X;
                Y.Value = value.Y;
                Z.Value = value.Z;
            }
        }

        /* Private properties. */
        private IntField X { get; set; }
        private IntField Y { get; set; }
        private IntField Z { get; set; }

        /* Constructors. */
        public Vector3IField() : base() { }

        public Vector3IField(float height, float labelWidth, string labelText, Vector3I value)
            : base(height, labelWidth, labelText, value) { }

        public Vector3IField(Vector3IField other) : base(other) { }

        /* Public methods. */
        public override Vector3IField Duplicate()
        {
            return new Vector3IField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Vector field init.
            base.Init();

            // Set name.
            Name = "Vector3IField";

            // Create int fields.
            X = CreateField(0f, "X", 0);
            Y = CreateField(0f, "Y", 0);
            Z = CreateField(0f, "Z", 0);
        }
    }
}