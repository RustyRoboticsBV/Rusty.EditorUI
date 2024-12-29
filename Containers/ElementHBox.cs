using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A HBoxContainer for elements.
    /// </summary>
    public partial class ElementHBox : ElementContainer<HBoxContainer>
    {
        /* Constructors. */
        public ElementHBox() : base() { }

        public ElementHBox(ElementHBox other) : base()
        {
            CopyStateFrom(other);
        }

        /* Public methods. */
        public override Element Duplicate()
        {
            return new ElementHBox(this);
        }
    }
}