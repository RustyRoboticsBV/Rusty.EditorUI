namespace Rusty.EditorUI
{
    /// <summary>
    /// A foldable section of elements, controlled by a check box field.
    /// </summary>
    public sealed partial class CheckBoxFoldout : Foldout<CheckBoxField>
    {
        /* Public properties. */
        public override bool IsOpen
        {
            get => Header.Value;
            set => Header.Value = value;
        }

        /* Constructors. */
        public CheckBoxFoldout() : base() { }

        public CheckBoxFoldout(float height, string headerText, int contentsIndentation, bool isOpen, params Element[] contents)
            : base(height, headerText, contentsIndentation, isOpen, contents)
        {
            IsOpen = isOpen;
            Children.Visible = false;
        }

        public CheckBoxFoldout(CheckBoxFoldout other) : base(other) { }

        /* Public methods. */
        public override CheckBoxFoldout Duplicate()
        {
            return new CheckBoxFoldout(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Base foldout init.
            base.Init();

            // Set name.
            Name = "CheckBoxFoldout";
        }
    }
}