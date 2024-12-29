namespace Rusty.EditorUI
{
    /// <summary>
    /// A foldable section of elements.
    /// </summary>
    public abstract partial class Foldout<T> : HeaderedElement<T>
        where T : LabeledElement, new()
    {
        /* Public properties. */
        public abstract bool IsOpen { get; set; }

        /* Constructors. */
        public Foldout() : base() { }

        public Foldout(float height, string headerText, int contentsIndentation, bool isOpen, params Element[] contents)
            : base(height, headerText, contentsIndentation, contents)
        {
            IsOpen = isOpen;
            Children.Visible = isOpen;
        }

        public Foldout(Foldout<T> other) : base(other) { }

        /* Public methods. */
        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is Foldout<T> otherFoldout)
            {
                IsOpen = otherFoldout.IsOpen;
                return true;
            }
            return false;
        }

        /* Godot overrides. */
        public override void _Process(double delta)
        {
            base._Process(delta);

            Children.Visible = IsOpen;
        }
    }
}