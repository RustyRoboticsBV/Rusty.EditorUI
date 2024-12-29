using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A MarginContainer for elements.
    /// </summary>
    public partial class ElementMargin : ElementContainer<MarginContainer>
    {
        /* Public properties. */
        public int MarginLeft
        {
            get => Contents.GetThemeConstant("margin_left");
            set => Contents.AddThemeConstantOverride("margin_left", value);
        }
        public int MarginRight
        {
            get => Contents.GetThemeConstant("margin_right");
            set => Contents.AddThemeConstantOverride("margin_right", value);
        }
        public int MarginBottom
        {
            get => Contents.GetThemeConstant("margin_bottom");
            set => Contents.AddThemeConstantOverride("margin_bottom", value);
        }
        public int MarginTop
        {
            get => Contents.GetThemeConstant("margin_top");
            set => Contents.AddThemeConstantOverride("margin_top", value);
        }

        /* Constructors. */
        public ElementMargin() : base() { }

        public ElementMargin(int marginLeft, int marginRight, int marginBottom, int marginTop) : base()
        {
            MarginLeft = marginLeft;
            MarginRight = marginRight;
            MarginBottom = marginBottom;
            MarginTop = marginTop;
        }

        public ElementMargin(ElementMargin other) : base()
        {
            CopyStateFrom(other);
        }

        /* Public methods. */
        public override Element Duplicate()
        {
            return new ElementMargin(this);
        }

        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is ElementMargin marginContainer)
            {
                MarginLeft = marginContainer.MarginLeft;
                MarginRight = marginContainer.MarginRight;
                MarginBottom = marginContainer.MarginBottom;
                MarginTop = marginContainer.MarginTop;
                return true;
            }
            return false;
        }
    }
}