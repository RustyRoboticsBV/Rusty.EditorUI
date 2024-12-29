namespace Rusty.EditorUI
{
    /// <summary>
    /// A field that contains a mutable value of some kind.
    /// </summary>
    public abstract partial class Field<T> : LabeledElement
    {
        /* Public properties. */
        /// <summary>
        /// The value stored on the field.
        /// </summary>
        public abstract T Value { get; set; }

        /* Constructors. */
        public Field() : base() { }

        public Field(float height, float labelWidth, string labelText, T value) : base(height, labelWidth, labelText)
        {
            Value = value;
        }

        public Field(Field<T> other) : base(other) { }

        /* Public methods. */
        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other))
            {
                Field<T> otherT = other as Field<T>;
                Value = otherT.Value;
                return true;
            }
            else
                return false;
        }
    }
}