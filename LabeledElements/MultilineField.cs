using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A multi-line text field.
    /// </summary>
    public sealed partial class MultilineField : Field<string>
    {
        /* Public properties. */
        public override string Value
        {
            get => TextEdit.Text;
            set => TextEdit.Text = value;
        }

        /* Private properties. */
        private new VBoxContainer Contents { get; set; }
        private TextEdit TextEdit { get; set; }

        /* Constructors. */
        public MultilineField() : base() { }

        public MultilineField(float height, float labelWidth, string labelText, string value)
            : base(height, labelWidth, labelText, value) { }

        public MultilineField(float singleLineHeight, int lineNumber, float labelWidth, string labelText, string value)
            : this(singleLineHeight * (lineNumber + 1), labelWidth, labelText, value) { }

        public MultilineField(MultilineField other) : base(other) { }

        /* Public methods. */
        public override MultilineField Duplicate()
        {
            return new MultilineField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Set name.
            Name = "MultilineField";

            // Create text edit.
            Contents = new();
            base.Contents.Reparent(Contents);
            base.Contents.Name = "Header";
            Contents.Name = "Contents";
            AddChild(Contents);

            TextEdit = new();
            TextEdit.Name = "TextEdit";
            TextEdit.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            TextEdit.SizeFlagsVertical = SizeFlags.ExpandFill;
            Contents.AddChild(TextEdit);
        }
    }
}