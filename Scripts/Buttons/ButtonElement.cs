using Godot;
using System;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A button element.
    /// </summary>
	public sealed partial class ButtonElement : Element
    {
        /* Public properties. */
        /// <summary>
        /// The text that is displayed on the button.
        /// </summary>
        public string ButtonText
        {
            get => Button.Text;
            set => Button.Text = value;
        }

        /* Private properties. */
        private Button Button { get; set; }

        /* Public events. */
        public event Action Pressed;

        /* Constructors. */
        public ButtonElement() : base() { }

        public ButtonElement(float height, string buttonText) : this()
        {
            Height = height;
            ButtonText = buttonText;
        }

        public ButtonElement(Element other) : this()
        {
            CopyStateFrom(other);
        }

        /* Public methods. */
        public override Element Duplicate()
        {
            return new ButtonElement(this);
        }

        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is ButtonElement otherButton)
            {
                ButtonText = otherButton.ButtonText;

                return true;
            }
            return false;
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Element init.
            base.Init();

            // Add button.
            Button = new();
            Button.Name = "Button";
            Button.Text = "Text";
            Button.SizeFlagsHorizontal = SizeFlags.Fill;
            Button.SizeFlagsVertical = SizeFlags.Fill;
            Button.Pressed += OnPressed;
            AddChild(Button);
        }

        /* Private methods. */
        private void OnPressed()
        {
            Pressed?.Invoke();
        }
    }
}