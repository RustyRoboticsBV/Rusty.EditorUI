using Godot;
using System.Collections.Generic;

namespace Rusty.EditorUI
{
    /// <summary>
    /// An option button field.
    /// </summary>
    public sealed partial class OptionField : Field<int>
    {
        /* Public properties. */
        /// <summary>
        /// The selected item.
        /// </summary>
        public override int Value
        {
            get => OptionButton.Selected;
            set => Select(value);
        }
        /// <summary>
        /// The selectable options on this field.
        /// </summary>
        public string[] Options
        {
            get => _Options.ToArray();
            set
            {
                SetOptions(value);
            }
        }

        /* Public events. */
        public event OptionButton.ItemSelectedEventHandler ItemSelected;

        /* Private properties. */
        private OptionButton OptionButton { get; set; }
        private List<string> _Options { get; set; }

        /* Constructors. */
        public OptionField() : base()
        {
            Value = -1;
        }

        public OptionField(float height, float labelWidth, string labelText, string[] options, int selected)
            : base(height, labelWidth, labelText, -1)
        {
            Options = options;
            Value = selected;
        }

        public OptionField(OptionField other) : base(other) { }

        /* Public methods. */
        public override OptionField Duplicate()
        {
            return new OptionField(this);
        }

        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is OptionField otherOptions)
            {
                Options = otherOptions.Options;
                Select(otherOptions.Value);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Remove all options from the option button.
        /// </summary>
        public void ClearOptions()
        {
            _Options.Clear();
            OptionButton.Clear();
        }

        /// <summary>
        /// Add an option to the option button.
        /// </summary>
        public void AddOption(string option)
        {
            _Options.Add(option);
            OptionButton.AddItem(option);
        }

        /// <summary>
        /// Set the options on the option button. Selects option -1.
        /// </summary>
        public void SetOptions(string[] options)
        {
            // Remove all pre-existing options.
            ClearOptions();

            // Add each option.
            foreach (string option in options)
            {
                AddOption(option);
            }
        }

        /// <summary>
        /// Selects an option on the option button.
        /// </summary>
        public void Select(int option)
        {
            if (option >= 0 && option < OptionButton.ItemCount)
                OptionButton.Select(option);
            else
                OptionButton.Select(-1);
            OnItemSelected(option);
        }

        /// <summary>
        /// Selects an option on the option button, if it exists. Select -1 otherwise.
        /// </summary>
        public void Select(string option)
        {
            Select(_Options.IndexOf(option));
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Generic field init.
            base.Init();

            // Set name.
            Name = "OptionField";

            // Create option button field.
            OptionButton = new();
            OptionButton.Name = "OptionButton";
            OptionButton.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            Contents.AddChild(OptionButton);

            // Create options array.
            _Options = new();

            // Set up events.
            OptionButton.ItemSelected += OnItemSelected;
        }

        /* Private methods. */
        private void OnItemSelected(long index)
        {
            ItemSelected?.Invoke(index);
        }
    }
}