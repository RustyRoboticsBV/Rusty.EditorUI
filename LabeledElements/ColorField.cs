using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A color picker field.
    /// </summary>
    public sealed partial class ColorField : Field<Color>
    {
        /* Public properties. */
        public override Color Value
        {
            get => ColorPickerButton.Color;
            set => ColorPickerButton.Color = value;
        }

        /* Private properties. */
        private ColorPickerButton ColorPickerButton { get; set; }

        /* Constructors. */
        public ColorField() : base() { }

        public ColorField(float height, float labelWidth, string labelText, Color value)
            : base(height, labelWidth, labelText, value) { }

        public ColorField(ColorField other) : base(other) { }

        /* Public methods. */
        public override ColorField Duplicate()
        {
            return new ColorField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Set name.
            Name = "ColorField";

            // Create color picker button.
            ColorPickerButton = new();
            ColorPickerButton.Name = "ColorPickerButton";
            ColorPickerButton.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            Contents.AddChild(ColorPickerButton);
        }
    }
}