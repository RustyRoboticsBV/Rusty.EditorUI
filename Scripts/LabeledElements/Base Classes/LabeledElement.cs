using Godot;
using System;

namespace Rusty.EditorUI
{
    /// <summary>
    /// Base class for all fields.
    /// </summary>
	public abstract partial class LabeledElement : Element
    {
        /* Public properties. */
        /// <summary>
        /// The minimum width of the label at no indentation.
        /// </summary>
        public float LabelWidth { get; set; }
        /// <summary>
        /// The text displayed in the field label.
        /// </summary>
        public virtual string LabelText
        {
            get => Label.Text;
            set => Label.Text = value;
        }
        /// <summary>
        /// The color of the field label text.
        /// </summary>
        public Color LabelColor
        {
            get => Label.GetThemeColor("font_color");
            set => Label.AddThemeColorOverride("font_color", value);
        }
        /// <summary>
        /// The horizontal size flags of the field label.
        /// </summary>
        public SizeFlags LabelSizeFlags
        {
            get => LabelContainer.SizeFlagsHorizontal;
            set => LabelContainer.SizeFlagsHorizontal = value;
        }

        /* Protected properties. */
        protected HBoxContainer Contents { get; private set; }
        protected float IndentedLabelWidth => LabelWidth - GlobalIndentation;
        protected Label Label { get; private set; }

        /* Public events. */
        public event Action MouseEnteredLabel;
        public event Action MouseExitedLabel;

        /* Private properties. */
        private MarginContainer LabelContainer { get; set; }

        /* Constructors. */
        public LabeledElement() : base() { }

        public LabeledElement(float height, float labelWidth, string labelText) : base(height)
        {
            LabelWidth = labelWidth;
            LabelText = labelText;
        }

        public LabeledElement(Element other) : base(other) { }

        /* Public methods. */
        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is LabeledElement otherLabeled)
            {
                LabelWidth = otherLabeled.LabelWidth;
                LabelText = otherLabeled.LabelText;
                LabelColor = otherLabeled.LabelColor;
                LabelSizeFlags = otherLabeled.LabelSizeFlags;
                return true;
            }
            return false;
        }

        /* Godot overrides. */
        public override void _Process(double delta)
        {
            base._Process(delta);

            Label.CustomMinimumSize = new(IndentedLabelWidth, Label.CustomMinimumSize.Y);
        }

        /* Protected methods. */
        /// <summary>
        /// Initialize this field. Note that you cannot access nested fields during the execution of this method, as doing do
        /// would break the duplicate method!
        /// </summary>
        protected override void Init()
        {
            // Contents container.
            Contents = new();
            Contents.Name = "Contents";
            Contents.SizeFlagsHorizontal = SizeFlags.Fill;
            Contents.SizeFlagsVertical = SizeFlags.Fill;
            AddChild(Contents);

            // Add label.
            LabelContainer = new();
            LabelContainer.Name = "LabelContainer";
            LabelContainer.SizeFlagsHorizontal = SizeFlags.Fill;
            LabelContainer.SizeFlagsVertical = SizeFlags.Fill;
            LabelContainer.AddThemeConstantOverride("margin_top", 3);
            Contents.AddChild(LabelContainer);

            Label = new();
            Label.Name = "Label";
            Label.SizeFlagsVertical = SizeFlags.ShrinkCenter;
            LabelContainer.AddChild(Label);

            // Set default properties.
            LabelWidth = 128f;
            LabelText = "Text";
            LabelColor = Colors.White;
            LabelSizeFlags = SizeFlags.Fill;

            // Set up events.
            LabelContainer.MouseEntered += OnMouseEnteredContents;
            LabelContainer.MouseExited += OnMouseExitedContents;
        }

        /* Private methods. */
        private void OnMouseEnteredContents()
        {
            MouseEnteredLabel?.Invoke();
        }

        private void OnMouseExitedContents()
        {
            MouseExitedLabel?.Invoke();
        }
    }
}