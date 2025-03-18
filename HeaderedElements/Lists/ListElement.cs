namespace Rusty.EditorUI
{
    /// <summary>
    /// A list element.
    /// </summary>
    public sealed partial class ListElement : HeaderedElement<LabelElement>
    {
        /* Public properties. */
        /// <summary>
        /// The template field that will be used to construct list element fields from.
        /// </summary>
        public Element Template { get; set; } = new LabelElement() { LabelText = "MISSING_TEMPLATE" };
        /// <summary>
        /// The text displyed above each entry.
        /// </summary>
        public string EntryText { get; set; }
        /// <summary>
        /// The text displayed on the "add element" button.
        /// </summary>
        public string AddButtonText
        {
            get => AddButton.ButtonText;
            set => AddButton.ButtonText = value;
        }

        /// <summary>
        /// The number of elements in the list.
        /// </summary>
        public new int Count => Children.Count - 1;

        /* Private properties. */
        private ButtonElement AddButton { get; set; }

        /* Constructors. */
        public ListElement() : base() { }

        public ListElement(float height, string headerText, string entryText, string addButtonText, int entriesIndentation,
            Element template)
            : base(height, headerText, entriesIndentation)
        {
            EntryText = entryText;
            AddButtonText = addButtonText;
            Template = template;
        }

        public ListElement(ListElement other) : base(other) { }

        /* Indexers. */
        public new ListEntryElement this[int index] => Children[index] as ListEntryElement;

        /* Public methods. */
        public override ListElement Duplicate()
        {
            return new ListElement(this);
        }

        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is ListElement otherList)
            {
                Template = otherList.Template;
                EntryText = otherList.EntryText;
                for (int i = 0; i < Count; i++)
                {
                    ConnectEvents(Children.GetAt(i) as ListEntryElement);
                }
                AddButton = Children.GetAt(Children.Count - 1) as ButtonElement;
                AddButton.Pressed += OnPressedAdd;
                UpdateIndices();

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Add a new element, as if the add button was pressed.
        /// </summary>
        public void Add()
        {
            OnPressedAdd();
        }

        /// <summary>
        /// Clear all elements from the list.
        /// </summary>
        public void Clear()
        {
            while (Count > 0)
            {
                RemoveAt(0);
            }
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Element init.
            base.Init();

            // Set name.
            Name = "List";

            // Add button.
            AddButton = new()
            {
                Name = "AddButton",
                ButtonText = "Add Entry"
            };
            Children.Add(AddButton);

            // Set up events.
            AddButton.Pressed += OnPressedAdd;
        }

        /* Private methods. */
        private void Add(ListEntryElement element)
        {
            Children.Add(element);
            ConnectEvents(element);
            MoveAddButtonToBottom();
            UpdateIndices();
        }

        private void InsertAt(int elementIndex, ListEntryElement element)
        {
            Children.InsertAt(elementIndex, element);
            ConnectEvents(element);
            MoveAddButtonToBottom();
            UpdateIndices();
        }

        private void DuplicateAt(int elementIndex)
        {
            Element element = Children[elementIndex].Duplicate();
            InsertAt(elementIndex + 1, element as ListEntryElement);
        }

        private void MoveUpAt(int elementIndex)
        {
            if (elementIndex == 0)
                Children.SwapAt(0, Children.Count - 2);
            else
                Children.MoveUpAt(elementIndex);
            UpdateIndices();
        }

        private void MoveDownAt(int elementIndex)
        {
            if (elementIndex == Children.Count - 2)
                Children.SwapAt(0, Children.Count - 2);
            else
                Children.MoveDownAt(elementIndex);
            UpdateIndices();
        }

        private new void RemoveAt(int elementIndex)
        {
            Element element = Children[elementIndex];
            Children.RemoveAt(elementIndex);
            DisconnectEvents(element as ListEntryElement);
            UpdateIndices();
        }

        private ListEntryElement CreateElement()
        {
            return new(Height, 0, "Element", 0, Template);
        }

        private void ConnectEvents(ListEntryElement element)
        {
            element.PressedInsert += OnPressedInsert;
            element.PressedDuplicate += OnPressedDuplicate;
            element.PressedMoveUp += OnPressedMoveUp;
            element.PressedMoveDown += OnPressedMoveDown;
            element.PressedDelete += OnPressedDelete;
        }

        private void DisconnectEvents(ListEntryElement element)
        {
            element.PressedInsert -= OnPressedInsert;
            element.PressedDuplicate -= OnPressedDuplicate;
            element.PressedMoveUp -= OnPressedMoveUp;
            element.PressedMoveDown -= OnPressedMoveDown;
            element.PressedDelete -= OnPressedDelete;
        }

        private void MoveAddButtonToBottom()
        {
            while (Children.IndexOf(AddButton) != Children.Count - 1)
            {
                Children.MoveDown(AddButton);
            }
        }

        private void UpdateIndices()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] is ListEntryElement element)
                {
                    element.HeaderText = $"{EntryText} #{i}";
                    element.Name = "Entry" + i;
                    element.Index = i;
                }
            }
        }

        // Event handlers.
        private void OnPressedAdd()
        {
            ListEntryElement element = CreateElement();
            Add(element);
        }

        private void OnPressedInsert(int index)
        {
            ListEntryElement element = CreateElement();
            InsertAt(index, element);
        }

        private void OnPressedDuplicate(int index)
        {
            DuplicateAt(index);
        }

        private void OnPressedMoveUp(int index)
        {
            MoveUpAt(index);
        }

        private void OnPressedMoveDown(int index)
        {
            MoveDownAt(index);
        }

        private void OnPressedDelete(int index)
        {
            RemoveAt(index);
        }
    }
}