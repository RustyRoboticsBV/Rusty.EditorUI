using Godot;
using Godot.Collections;

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
        public Element Template { get; set; }
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
        public int Count => Children.Count - 1;

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

                // Update indices.
                UpdateIndices();

                // Copy button text.
                AddButtonText = otherList.AddButtonText;

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

        /* Godot overrides. */
        public override void _Process(double delta)
        {
            base._Process(delta);

            UpdateIndices();
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Element init.
            base.Init();

            // Add button.
            AddButton = new(20f, "Add Entry");
            AddButton.Name = "AddButton";
            AddButton.ButtonText = "Add Entry";
            Children.Add(AddButton);

            // Set up events.
            AddButton.Pressed += OnPressedAdd;
        }

        /* Private methods. */
        private void SetElements(Array<ListEntryElement> elements)
        {
            // Clear the list.
            Clear();

            // Add elements.
            if (elements != null)
            {
                foreach (ListEntryElement element in elements)
                {
                    Add(element);
                }
            }
        }

        private Array<ListEntryElement> GetElements()
        {
            Array<ListEntryElement> elements = new();
            for (int i = 0; i < Children.Count - 1; i++)
            {
                elements.Add(Children[i] as ListEntryElement);
            }
            return elements;
        }

        private void Clear()
        {
            Children.Clear();
            Children.Add(AddButton);
        }

        private void Add(ListEntryElement element)
        {
            Children.Add(element);
            ConnectEvents(element);
            MoveAddButtonToBottom();
            UpdateIndices();
        }

        private void InsertAt(int elementIndex, ListEntryElement element)
        {
            Children.Insert(elementIndex, element);
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

        private void RemoveAt(int elementIndex)
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
                    element.Index = i;
                }
            }
        }

        // Event handlers.
        private void OnPressedAdd()
        {
            GD.Print("Added");
            ListEntryElement element = CreateElement();
            Add(element);
        }

        private void OnPressedInsert(int index)
        {
            GD.Print("Inserted");
            ListEntryElement element = CreateElement();
            InsertAt(index, element);
        }

        private void OnPressedDuplicate(int index)
        {
            GD.Print("Duplicated");
            DuplicateAt(index);
        }

        private void OnPressedMoveUp(int index)
        {
            GD.Print("Moved Up");
            MoveUpAt(index);
        }

        private void OnPressedMoveDown(int index)
        {
            GD.Print("Moved Down");
            MoveDownAt(index);
        }

        private void OnPressedDelete(int index)
        {
            GD.Print("Deleted");
            RemoveAt(index);
        }
    }
}