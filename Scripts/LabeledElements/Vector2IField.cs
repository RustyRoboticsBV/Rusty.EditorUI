using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A vector field with two integer number components.
    /// </summary>
    public sealed partial class Vector2IField : IntVectorField<Vector2I>
    {
        /* Public properties. */
        public override Vector2I Value
        {
            get => new Vector2I(X.Value, Y.Value);
            set
            {
                X.Value = value.X;
                Y.Value = value.Y;
            }
        }

        /* Private properties. */
        private IntField X { get; set; }
        private IntField Y { get; set; }

        /* Constructors. */
        public Vector2IField() : base() { }

        public Vector2IField(float height, float labelWidth, string labelText, Vector2I value)
            : base(height, labelWidth, labelText, value) { }

        public Vector2IField(Vector2IField other) : base(other) { }

        /* Public methods. */
        public override Vector2IField Duplicate()
        {
            return new Vector2IField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Vector field init.
            base.Init();

            // Set name.
            Name = "Vector2IField";

            // Create int fields.
            X = CreateField(0f, "X", 0);
            Y = CreateField(0f, "Y", 0);
        }
    }
}