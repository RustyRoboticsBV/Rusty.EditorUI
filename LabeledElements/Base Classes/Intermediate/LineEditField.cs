using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A single-line text field.
    /// </summary>
    public abstract partial class LineEditField<T> : Field<T>
    {
        /* Protected fields. */
        protected LineEdit LineEdit { get; private set; }

        /* Constructors. */
        public LineEditField() : base() { }

        public LineEditField(float height, float labelWidth, string labelText, T value)
            : base(height, labelWidth, labelText, value) { }

        public LineEditField(LineEditField<T> other) : base(other) { }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Create line edit.
            LineEdit = new LineEdit();
            LineEdit.Name = "LineEdit";
            LineEdit.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            Contents.AddChild(LineEdit);
        }
    }
}