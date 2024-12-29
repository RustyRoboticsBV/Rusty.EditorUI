using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A single-line string field.
    /// </summary>
    public sealed partial class LineField : LineEditField<string>
    {
        /* Public properties. */
        public override string Value
        {
            get => LineEdit.Text;
            set => LineEdit.Text = value;
        }

        /* Constructors. */
        public LineField() : base() { }

        public LineField(float height, float labelWidth, string labelText, string value)
            : base(height, labelWidth, labelText, value) { }

        public LineField(LineField other) : base(other) { }

        /* Public methods. */
        public override LineField Duplicate()
        {
            return new LineField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // LineEditField init.
            base.Init();

            // Set name.
            Name = "LineField";
        }
    }
}