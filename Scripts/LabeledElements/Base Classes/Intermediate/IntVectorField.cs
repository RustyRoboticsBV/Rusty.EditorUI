namespace Rusty.EditorUI
{
    /// <summary>
    /// A base class for integer vector fields.
    /// </summary>
    public abstract partial class IntVectorField<T> : Field<T>
    {
        /* Constructors. */
        public IntVectorField() : base() { }

        public IntVectorField(float height, float labelWidth, string labelText, T value)
            : base(height, labelWidth, labelText, value) { }

        public IntVectorField(IntVectorField<T> other) : base(other) { }

        /* Protected methods. */
        /// <summary>
        /// Construct a field that represents an element of an integer number-based vector.
        /// </summary>
        protected IntField CreateField(float height, string labelText, int value)
        {
            IntField field = new(height, 16f, labelText, value);
            field.Name = labelText;
            field.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            Contents.AddChild(field);
            return field;
        }
    }
}