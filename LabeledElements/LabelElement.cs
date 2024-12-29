namespace Rusty.EditorUI
{
    /// <summary>
    /// A label element.
    /// </summary>
	public partial class LabelElement : LabeledElement
    {
        /* Constructors. */
        public LabelElement() : base() { }

        public LabelElement(float height, float labelWidth, string labelText)
            : base(height, labelWidth, labelText) { }

        public LabelElement(LabelElement other) : base(other) { }

        /* Public methods. */
        public override Element Duplicate()
        {
            return new LabelElement(this);
        }
    }
}