using Godot;
using System;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A boolean check box field.
    /// </summary>
    public sealed partial class CheckBoxField : Field<bool>
    {
        /* Public properties. */
        public override bool Value
        {
            get => CheckBox.ButtonPressed;
            set => CheckBox.ButtonPressed = value;
        }

        /* Public events. */
        public event Action Pressed;

        /* Private properties. */
        private CheckBox CheckBox { get; set; }

        /* Constructors. */
        public CheckBoxField() : base() { }

        public CheckBoxField(float height, float labelWidth, string labelText, bool value)
            : base(height, labelWidth, labelText, value) { }

        public CheckBoxField(CheckBoxField other) : base(other) { }

        /* Public methods. */
        public override CheckBoxField Duplicate()
        {
            return new CheckBoxField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Set name.
            Name = "CheckBoxField";

            // Create check box.
            CheckBox = new();
            CheckBox.Name = "CheckBox";
            Contents.AddChild(CheckBox);

            // Set up events.
            CheckBox.Pressed += OnPressed;
        }

        /* Private methods. */
        private void OnPressed()
        {
            Pressed?.Invoke();
        }
    }
}