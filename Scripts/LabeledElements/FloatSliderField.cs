namespace Rusty.EditorUI
{
    /// <summary>
    /// A horizontal float slider field.
    /// </summary>
    public sealed partial class FloatSliderField : HSliderField<float>
    {
        /* Public properties. */
        public override float Value
        {
            get => (float)Slider.Value;
            set => Slider.Value = value;
        }
        public override float MinValue
        {
            get => (float)Slider.MinValue;
            set => Slider.MinValue = value;
        }
        public override float MaxValue
        {
            get => (float)Slider.MaxValue;
            set => Slider.MaxValue = value;
        }

        /* Constructors. */
        public FloatSliderField() : base() { }

        public FloatSliderField(float height, float labelWidth, string labelText, float value, float minValue, float maxValue)
            : base(height, labelWidth, labelText, value, minValue, maxValue) { }

        public FloatSliderField(FloatSliderField other) : base(other) { }

        /* Public methods. */
        public override FloatSliderField Duplicate()
        {
            return new FloatSliderField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Slider field init.
            base.Init();

            // Set name.
            Name = "FloatSliderField";

            // Limit slider to three digits after the decimal point.
            Slider.Step = 0.001;
        }
    }
}