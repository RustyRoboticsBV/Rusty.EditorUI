using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A VBoxContainer for elements.
    /// </summary>
    public partial class ElementVBox : ElementContainer<VBoxContainer>
    {
        /* Constructors. */
        public ElementVBox() : base() { }

        public ElementVBox(ElementVBox other) : base()
        {
            CopyStateFrom(other);
        }

        /* Public methods. */
        public override Element Duplicate()
        {
            return new ElementVBox(this);
        }
    }
}