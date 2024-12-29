using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A real number field.
    /// </summary>
    public sealed partial class FloatField : SpinBoxField<float>
    {
        /* Public properties. */
        public override float Value
        {
            get => (float)SpinBox.Value;
            set => SpinBox.Value = value;
        }

        /* Constructors. */
        public FloatField() : base() { }

        public FloatField(float height, float labelWidth, string labelText, float value)
            : base(height, labelWidth, labelText, value) { }

        public FloatField(FloatField other) : base(other) { }

        /* Public methods. */
        public override FloatField Duplicate()
        {
            return new FloatField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Spin-box field init.
            base.Init();

            // Set name.
            Name = "FloatField";

            // Limit spin-box to three digits after the decimal point.
            SpinBox.Step = 0.001;
        }
    }
}