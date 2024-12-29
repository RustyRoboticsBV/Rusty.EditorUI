using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// An integer number field.
    /// </summary>
    public sealed partial class IntField : SpinBoxField<int>
    {
        /* Public properties. */
        public override int Value
        {
            get => (int)SpinBox.Value;
            set => SpinBox.Value = value;
        }

        /* Constructors. */
        public IntField() : base() { }

        public IntField(float height, float labelWidth, string labelText, int value)
            : base(height, labelWidth, labelText, value) { }

        public IntField(IntField other) : base(other) { }

        /* Public methods. */
        public override IntField Duplicate()
        {
            return new IntField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Spin-box field init.
            base.Init();

            // Set name.
            Name = "IntField";

            // Limit spin-box to whole numbers.
            SpinBox.Step = 1;
        }
    }
}