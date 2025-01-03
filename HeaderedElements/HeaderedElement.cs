using Godot;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A pair of a header element and VBoxContainerelement.
    /// </summary>
    public abstract partial class HeaderedElement<T> : Element
        where T : LabeledElement, new()
    {
        /* Public properties. */
        /// <summary>
        /// The label text of the header element.
        /// </summary>
        public virtual string HeaderText
        {
            get => Header.LabelText;
            set => Header.LabelText = value;
        }
        /// <summary>
        /// The color of the header text.
        /// </summary>
        public Color HeaderColor
        {
            get => Header.LabelColor;
            set => Header.LabelColor = value;
        }
        /// <summary>
        /// The horizontal size flags of the header label.
        /// </summary>
        public SizeFlags HeaderSizeFlags
        {
            get => Header.LabelSizeFlags;
            set => Header.LabelSizeFlags = value;
        }

        /// <summary>
        /// The number of elements contained in this header.
        /// </summary>
        public int Count => Children.Count;
        /// <summary>
        /// The indentation of the contents relative to this headered element.
        /// </summary>
        public int ContentsIndentation
        {
            get => Children.LocalIndentation;
            set => Children.LocalIndentation = value;
        }

        /* Protected properties. */
        protected T Header { get; set; }
        protected ElementVBox Children { get; set; }

        /* Private properties. */
        private VBoxContainer Contents { get; set; }

        /* Constructors. */
        public HeaderedElement() : base() { }
        
        public HeaderedElement(float height, string headerText, int contentsIndentation, params Element[] contents) : this()
        {
            HeaderText = headerText;
            ContentsIndentation = contentsIndentation;
            foreach (Element content in contents)
            {
                Add(content);
            }
        }

        public HeaderedElement(HeaderedElement<T> other) : base(other) { }

        /* Indexers. */
        public Element this[int index] => GetAt(index);

        /* Public methods. */
        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is HeaderedElement<T> otherHeadered)
            {
                Header.CopyStateFrom(otherHeadered.Header);
                Children.CopyStateFrom(otherHeadered.Children);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return the index of an element contained on this header. Returns -1 if the element wasn't contained on this header.
        /// </summary>
        public int IndexOf(Element element)
        {
            return Children.IndexOf(element);
        }
        
        /// <summary>
        /// Get an element contained on this header.
        /// </summary
        public Element GetAt(int index)
        {
            return Children.GetAt(index);
        }

        /// <summary>
        /// Add an element to this header.
        /// </summary>
        public void Add(Element element)
        {
            Children.Add(element);
        }

        /// <summary>
        /// Insert an element into this header at some index.
        /// </summary>
        public void InsertAt(int index, Element element)
        {
            Children.InsertAt(index, element);
        }

        /// <summary>
        /// Remove an element from this header.
        /// </summary>
        public void RemoveAt(int index)
        {
            Children.RemoveAt(index);
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Base field init.
            base.Init();

            // Set name.
            Name = "Headered";

            // Add contents.
            Contents = new();
            Contents.Name = "Contents";
            AddChild(Contents);

            Header = new();
            Header.Name = "Header";
            Header.SizeFlagsHorizontal = SizeFlags.Fill;
            Contents.AddChild(Header);

            Children = new();
            Children.Name = "Children";
            Contents.AddChild(Children);

            // Set default properties.
            FocusMode = FocusModeEnum.All;
            ContentsIndentation = 12;
        }
    }
}