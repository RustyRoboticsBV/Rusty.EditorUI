using Godot;
using System;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A boolean toggle field.
    /// </summary>
    public sealed partial class ToggleField : Field<bool>
    {
        /* Public properties. */
        public override bool Value
        {
            get => CheckButton.ButtonPressed;
            set => CheckButton.ButtonPressed = value;
        }

        /* Public events. */
        public event Action Pressed;

        /* Private properties. */
        private CheckButton CheckButton { get; set; }

        /* Constructors. */
        public ToggleField() : base() { }

        public ToggleField(float height, float labelWidth, string labelText, bool value)
            : base(height, labelWidth, labelText, value) { }

        public ToggleField(ToggleField other) : base(other) { }

        /* Public methods. */
        public override ToggleField Duplicate()
        {
            return new ToggleField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Set name.
            Name = "ToggleField";

            // Create check box.
            CheckButton = new();
            CheckButton.Name = "CheckButton";
            Contents.AddChild(CheckButton);

            // Set up events.
            CheckButton.Pressed += OnPressed;
        }

        /* Private methods. */
        private void OnPressed()
        {
            Pressed?.Invoke();
        }
    }
}
