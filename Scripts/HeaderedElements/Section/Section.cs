namespace Rusty.EditorUI
{
    /// <summary>
    /// A set of indented elements under a header title label.
    /// </summary>
    public partial class Section : HeaderedElement<LabelElement>
    {
        /* Constructors. */
        public Section() : base() { }

        public Section(float height, string headerText, int contentsIndentation, params Element[] contents)
            : base(height, headerText, contentsIndentation, contents) { }

        public Section(Section other) : base(other) { }

        /* Public methods. */
        public override Section Duplicate()
        {
            return new Section(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Base HeaderedElement init.
            base.Init();

            // Set name.
            Name = "Section";
        }
    }
}