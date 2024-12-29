using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A foldable section of elements.
    /// </summary>
    public sealed partial class LabelFoldout : Foldout<LabelElement>
    {
        /* Public properties. */
        public override bool IsOpen { get; set; }
        public override string HeaderText
        {
            get
            {
                if (!(base.HeaderText.StartsWith(OpenedSymbol) || base.HeaderText.StartsWith(FoldedSymbol)))
                    return base.HeaderText;
                else
                    return base.HeaderText.Substring(3);
            }
            set
            {
                if (IsOpen)
                    base.HeaderText = $"{OpenedSymbol}  " + value;
                else
                    base.HeaderText = $"{FoldedSymbol}  " + value;
            }
        }

        /* Private properties. */
        private bool Highlighted { get; set; }
        private const char OpenedSymbol = '\u25BC';
        private const char FoldedSymbol = '\u25B6';

        /* Constructors. */
        public LabelFoldout() : base() { }

        public LabelFoldout(float height, string headerText, int contentsIndentation, bool isOpen, params Element[] contents)
            : base(height, headerText, contentsIndentation, isOpen, contents) { }

        public LabelFoldout(LabelFoldout other) : base(other) { }

        /* Public methods. */
        public override LabelFoldout Duplicate()
        {
            return new LabelFoldout(this);
        }

        /* Godot overrides. */
        public override void _Process(double delta)
        {
            base._Process(delta);

            HeaderText = HeaderText;
        }

        public override void _GuiInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouse && mouse.ButtonIndex == MouseButton.Left)
            {
                if (mouse.Pressed && Highlighted)
                {
                    IsOpen = !IsOpen;
                    GrabFocus();
                }
            }
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Base foldout init.
            base.Init();

            // Set name.
            Name = "LabelFoldout";

            // Set up events.
            Header.MouseEnteredLabel += OnMouseEnter;
            Header.MouseExitedLabel += OnMouseExit;
        }

        /* Private methods. */
        private void OnMouseEnter()
        {
            Color color = new Color(1f, 1f, 1f, 0.75f);
            HeaderColor = color;
            Highlighted = true;
        }

        private void OnMouseExit()
        {
            Color color = Colors.White;
            HeaderColor = color;
            Highlighted = false;
        }
    }
}