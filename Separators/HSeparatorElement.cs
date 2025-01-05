using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A horizontal separator element.
    /// </summary>
	public partial class HSeparatorElement : Element
    {
        /* Public properties. */
        /// <summary>
        /// The color of the separator.
        /// </summary>
        public Color Color
        {
            get => Separator.Modulate;
            set => Separator.Modulate = value;
        }

        /* Protected properties. */
        protected HSeparator Separator { get; private set; }

        /* Private properties. */
        private MarginContainer LabelContainer { get; set; }

        /* Constructors. */
        public HSeparatorElement() : base() { }

        public HSeparatorElement(float height) : base(height) { }

        public HSeparatorElement(Element other) : base(other) { }

        /* Public methods. */
        public override Element Duplicate()
        {
            return new HSeparatorElement(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Base element init.
            base.Init();

            // Add separator.
            Separator = new();
            AddChild(Separator);

            // Set default height.
            Height = 4;
        }
    }
}