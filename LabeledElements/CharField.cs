using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A character field.
    /// </summary>
    public sealed partial class CharField : LineEditField<char>
    {
        /* Public properties. */
        public override char Value
        {
            get => LineEdit.Text.Length > 0 ? LineEdit.Text[0] : (char)0;
            set => LineEdit.Text = value.ToString();
        }

        /* Constructors. */
        public CharField() : base() { }

        public CharField(float height, float labelWidth, string labelText, char value)
            : base(height, labelWidth, labelText, value) { }

        public CharField(CharField other) : base(other) { }

        /* Public methods. */
        public override CharField Duplicate()
        {
            return new CharField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Line edit field init.
            base.Init();

            // Set name.
            Name = "CharField";

            // Limit length to one character.
            LineEdit.MaxLength = 1;
        }
    }
}