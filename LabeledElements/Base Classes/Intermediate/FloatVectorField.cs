namespace Rusty.EditorUI
{
    /// <summary>
    /// A base class for floating-point vector fields.
    /// </summary>
    public abstract partial class FloatVectorField<T> : Field<T>
    {
        /* Constructors. */
        public FloatVectorField() : base() { }

        public FloatVectorField(float height, float labelWidth, string labelText, T value)
            : base(height, labelWidth, labelText, value) { }

        public FloatVectorField(FloatVectorField<T> other) : base(other) { }

        /* Protected methods. */
        /// <summary>
        /// Construct a field that represents an element of an real number-based vector.
        /// </summary>
        protected FloatField CreateField(float height, string labelText, float value)
        {
            FloatField field = new(height, 16f, labelText, value);
            field.Name = labelText;
            field.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            Contents.AddChild(field);
            return field;
        }
    }
}