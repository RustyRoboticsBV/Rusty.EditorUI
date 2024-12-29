namespace Rusty.EditorUI
{
    /// <summary>
    /// A horizontal int slider field.
    /// </summary>
    public sealed partial class IntSliderField : HSliderField<int>
    {
        /* Public properties. */
        public override int Value
        {
            get => (int)Slider.Value;
            set => Slider.Value = value;
        }
        public override int MinValue
        {
            get => (int)Slider.MinValue;
            set => Slider.MinValue = value;
        }
        public override int MaxValue
        {
            get => (int)Slider.MaxValue;
            set => Slider.MaxValue = value;
        }

        /* Constructors. */
        public IntSliderField() : base() { }

        public IntSliderField(float height, float labelWidth, string labelText, int value, int minValue, int maxValue)
            : base(height, labelWidth, labelText, value, minValue, maxValue) { }

        public IntSliderField(IntSliderField other) : base(other) { }

        /* Public methods. */
        public override IntSliderField Duplicate()
        {
            return new IntSliderField(this);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Slider field init.
            base.Init();

            // Set name.
            Name = "IntSliderField";

            // Limit slider to whole numbers.
            Slider.Step = 1;
        }
    }
}