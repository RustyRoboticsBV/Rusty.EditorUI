using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// An icon element followed by a label.
    /// </summary>
    public sealed partial class LabeledIcon : Field<Texture2D>
    {
        /* Public properties. */
        public override float Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
            }
        }
        /// <summary>
        /// The texture displayed by the icon.
        /// </summary>
        public override Texture2D Value
        {
            get => Icon.Texture;
            set => Icon.Texture = value;
        }
        /// <summary>
        /// The margin between the icon and the label.
        /// </summary>
        public int Margin
        {
            get => MarginContainer.GetThemeConstant("margin_right");
            set => MarginContainer.AddThemeConstantOverride("margin_right", value);
        }
        /// <summary>
        /// The color of the icon.
        /// </summary>
        public Color Color
        {
            get => Icon.Modulate;
            set => Icon.Modulate = value;
        }

        /* Private properties. */
        private MarginContainer MarginContainer { get; set; }
        private TextureRect Icon { get; set; }

        /* Constructors. */
        public LabeledIcon() : base() { }

        public LabeledIcon(float height, Texture2D value, int margin, string labelText)
            : base(height, 0f, labelText, value)
        {
            Height = height;
            Margin = margin;
        }

        public LabeledIcon(LabeledIcon other) : base(other) { }

        /* Public methods. */
        public override LabeledIcon Duplicate()
        {
            return new LabeledIcon(this);
        }

        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is LabeledIcon otherIcon)
            {
                Height = otherIcon.Height;
                Margin = otherIcon.Margin;
                Color = otherIcon.Color;
            }
            return false;
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Set name.
            Name = "LabeledIcon";

            // Add icon container.
            MarginContainer = new();
            MarginContainer.Name = "IconContainer";
            Contents.AddChild(MarginContainer);
            Contents.MoveChild(MarginContainer, 0);

            // Add icon rect.
            Icon = new();
            Icon.Name = "Icon";
            Icon.ExpandMode = TextureRect.ExpandModeEnum.FitWidth;
            Icon.StretchMode = TextureRect.StretchModeEnum.Scale;
            MarginContainer.AddChild(Icon);

            // Make unclickable.
            MouseFilter = MouseFilterEnum.Ignore;
            MarginContainer.MouseFilter = MouseFilterEnum.Ignore;
            Icon.MouseFilter = MouseFilterEnum.Ignore;
        }
    }
}