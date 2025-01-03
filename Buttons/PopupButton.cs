using Godot;
using System;
using System.Reflection.PortableExecutable;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A button element.
    /// </summary>
	public sealed partial class PopupButtonElement : Element
    {
        /* Public properties. */
        /// <summary>
        /// The text that is displayed on the button.
        /// </summary>
        public string ButtonText
        {
            get => RealButton.Text;
            set => RealButton.Text = value;
        }

        /* Private properties. */
        private Button FakeButton { get; set; }
        private Button RealButton { get; set; }

        /* Public events. */
        public event Action<int> Selected;

        /* Constructors. */
        public PopupButtonElement() : base() { }

        public PopupButtonElement(float height, string buttonText) : this()
        {
            Height = height;
            ButtonText = buttonText;
        }

        public PopupButtonElement(Element other) : this()
        {
            CopyStateFrom(other);
        }

        /* Public methods. */
        public override Element Duplicate()
        {
            return new PopupButtonElement(this);
        }

        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is PopupButtonElement otherButton)
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

            // Add fake button as a background.
            Button fakeButton = new();
            fakeButton.Name = "Background";
            AddChild(fakeButton);

            // Add real button.
            MenuButton menuButton = new();
            menuButton.Name = "ActionButton";
            menuButton.Text = "...";
            menuButton.GetPopup().AddItem("Insert");
            menuButton.GetPopup().AddItem("Duplicate");
            menuButton.GetPopup().AddItem("Move Up");
            menuButton.GetPopup().AddItem("Move Down");
            menuButton.GetPopup().AddItem("Delete");
            AddChild(menuButton);

            // Add button.
            RealButton = new();
            RealButton.Name = "Button";
            RealButton.Text = "Text";
            AddChild(RealButton);
        }
    }
}