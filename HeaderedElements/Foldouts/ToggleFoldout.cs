namespace Rusty.EditorUI
{
    /// <summary>
    /// A foldable section of elements, controlled by a toggle field.
    /// </summary>
    public sealed partial class ToggleFoldout : Foldout<ToggleField>
    {
        /* Public properties. */
        public override bool IsOpen
        {
            get => Header.Value;
            set => Header.Value = value;
        }

        /* Constructors. */
        public ToggleFoldout() : base() { }

        public ToggleFoldout(float height, string headerText, int contentsIndentation, bool isOpen, params Element[] contents)
            : base(height, headerText, contentsIndentation, isOpen, contents) { }

        public ToggleFoldout(ToggleFoldout other) : base(other) { }

        /* Public methods. */
        public override ToggleFoldout Duplicate()
        {
            return new ToggleFoldout(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Base HeaderedElement init.
            base.Init();

            // Set name.
            Name = "ToggleFoldout";
        }
    }
}