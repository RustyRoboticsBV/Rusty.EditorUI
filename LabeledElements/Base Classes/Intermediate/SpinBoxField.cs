using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A real number field.
    /// </summary>
    public abstract partial class SpinBoxField<T> : Field<T>
    {
        /* Protected fields. */
        protected SpinBox SpinBox { get; set; }

        /* Constructors. */
        public SpinBoxField() : base() { }

        public SpinBoxField(float height, float labelWidth, string labelText, T value)
            : base(height, labelWidth, labelText, value) { }

        public SpinBoxField(SpinBoxField<T> other) : base(other) { }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Create spin-box.
            SpinBox = new SpinBox();
            SpinBox.Name = "SpinBox";
            SpinBox.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            SpinBox.MinValue = -1 << 60;
            SpinBox.MaxValue = 1 << 60;
            Contents.AddChild(SpinBox);
        }
    }
}