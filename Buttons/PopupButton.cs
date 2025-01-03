using Godot;
using System;

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
            get => MenuButton.Text;
            set => MenuButton.Text = value;
        }
        /// <summary>
        /// The options in the popup menu.
        /// </summary>
        public string[] PopupOptions
        {
            get
            {
                if (MenuButton == null)
                    return new string[0];
                PopupMenu popup = MenuButton.GetPopup();
                int itemCount = popup.ItemCount;
                string[] options = new string[itemCount];
                for (int i = 0; i < itemCount; i++)
                {
                    options[i] = popup.GetItemText(i);
                }
                return options;
            }
            set
            {
                if (MenuButton == null)
                    return;
                PopupMenu popup = MenuButton.GetPopup();
                popup.Clear();
                foreach (string option in value)
                {
                    popup.AddItem(option);
                }
            }
        }

        /* Private properties. */
        private Button FakeButton { get; set; }
        private MenuButton MenuButton { get; set; }

        /* Public events. */
        public event Action<long> PressedOption;

        /* Constructors. */
        public PopupButtonElement() : base() { }

        public PopupButtonElement(float height, string buttonText, string[] popupOptions) : this()
        {
            Height = height;
            ButtonText = buttonText;
            PopupOptions = popupOptions;
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
                PopupOptions = otherButton.PopupOptions;
                return true;
            }
            else
                return false;
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Element init.
            base.Init();

            // Set name.
            Name = "PopupButton";

            // Add fake button for a background.
            ElementMargin buttonContainer = new();
            buttonContainer.Name = "ActionButtonContainer";
            AddChild(buttonContainer);

            FakeButton = new();
            FakeButton.Name = "Background";
            buttonContainer.AddChild(FakeButton);

            // Add real button.
            MenuButton = new();
            MenuButton.Name = "MenuButton";
            MenuButton.Text = "MenuButton";
            buttonContainer.AddChild(MenuButton);

            // Set up events.
            MenuButton.GetPopup().IdPressed += OnPopupPressed;
        }

        /* Private methods. */
        private void OnPopupPressed(long id)
        {
            PressedOption?.Invoke(id);
        }
    }
}