using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// Base class for horizontal slider fields.
    /// </summary>
    public abstract partial class HSliderField<T> : Field<T>
    {
        /* Public properties. */
        /// <summary>
        /// The minimum value that this slider allows.
        /// </summary>
        public abstract T MinValue { get; set; }
        /// <summary>
        /// The maximum value that this slider allows.
        /// </summary>
        public abstract T MaxValue { get; set; }

        /* Protected properties. */
        protected HSlider Slider { get; private set; }

        /* Constructors. */
        public HSliderField() : base() { }

        public HSliderField(float height, float labelWidth, string labelText, T value, T minValue, T maxValue)
            : base(height, labelWidth, labelText, value)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public HSliderField(HSliderField<T> other) : base(other) { }

        /* Public methods. */
        public sealed override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is HSliderField<T> slider)
            {
                MinValue = slider.MinValue;
                MaxValue = slider.MaxValue;
                return true;
            }
            else
                return false;
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Create slider container.
            MarginContainer container = new();
            container.Name = "SliderContainer";
            container.AddThemeConstantOverride("margin_top", 5);
            container.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            Contents.AddChild(container);

            // Create slider.
            Slider = new();
            Slider.Name = "HSlider";
            Slider.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            container.AddChild(Slider);
        }
    }
}