using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A list element field. Not supposed to be used directly.
    /// </summary>
    public sealed partial class ListEntryElement : HeaderedElement<LabelElement>
    {
        /* Public properties. */
        public int Index { get; set; }

        /* Public delegates. */
        public delegate void EventHandler(int index);

        /* Public events. */
        public event EventHandler PressedInsert;
        public event EventHandler PressedDuplicate;
        public event EventHandler PressedMoveUp;
        public event EventHandler PressedMoveDown;
        public event EventHandler PressedDelete;

        /* Private properties. */
        PopupButtonElement ActionButton { get; set; }

        /* Constructors. */
        public ListEntryElement() : base() { }

        public ListEntryElement(float lineHeight, int index, string headerText, int contentsIndentation, Element contentsTemplate)
            : base(lineHeight, headerText, contentsIndentation)
        {
            // Add duplicate of template as nested field.
            Element element = contentsTemplate.Duplicate();
            element.LocalIndentation = ContentsIndentation;
            Add(element);

            // Set field properties.
            Index = index;
        }

        public ListEntryElement(ListEntryElement other) : base(other) { }

        /* Public methods. */
        public override ListEntryElement Duplicate()
        {
            return new ListEntryElement(this);
        }

        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is ListEntryElement otherListElement)
            {
                Index = otherListElement.Index;
                return true;
            }
            else
                return false;
        }

        /* Protected properties. */
        protected override void Init()
        {
            // Base headered element init.
            base.Init();

            // Make label expand & fill.
            HeaderSizeFlags = SizeFlags.ExpandFill;
            ContentsIndentation = 0;

            // Add fake button for a background.
            ActionButton = new()
            {
                SizeFlagsHorizontal = SizeFlags.ShrinkEnd,
                CustomMinimumSize = new Vector2(100f, Height)
            };
            Header.AddChild(ActionButton);

            ActionButton.ButtonText = "...";
            ActionButton.PopupOptions = new string[] { "Insert", "Duplicate", "Move Up", "Move Down", "Delete" };

            // Set up events.
            ActionButton.PressedOption += OnPopupPressed;

            // Set name.
            Name = "ListEntryElement";
        }

        /* Private properties. */
        private void OnPopupPressed(long index)
        {
            switch (index)
            {
                case 0:
                    PressedInsert?.Invoke(Index);
                    break;
                case 1:
                    PressedDuplicate?.Invoke(Index);
                    break;
                case 2:
                    PressedMoveUp?.Invoke(Index);
                    break;
                case 3:
                    PressedMoveDown?.Invoke(Index);
                    break;
                case 4:
                    PressedDelete?.Invoke(Index);
                    break;
            }
        }
    }
}