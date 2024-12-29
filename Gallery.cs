using Godot;

namespace Rusty.EditorUI
{
    [GlobalClass]
    public partial class Gallery : VBoxContainer
    {
        [Export] public Texture2D Icon { get; private set; }

        const float height = 20f;
        const float labelw = 128f;

        public override void _Ready()
        {
            AddChild(new LabelElement(height, labelw, "Label"), true);
            AddChild(new ButtonElement(height, "Button"), true);

            AddChild(new CheckBoxField(height, labelw, "Check Box", true), true);
            AddChild(new ToggleField(height, labelw, "Toggle", true), true);

            AddChild(new IntField(height, labelw, "Integer", 0), true);
            AddChild(new FloatField(height, labelw, "Float", 0f), true);

            AddChild(new IntSliderField(height, labelw, "Slider (integer)", 0, 0, 10), true);
            AddChild(new FloatSliderField(height, labelw, "Slider (float)", 0f, 0f, 100f), true);

            AddChild(new CharField(height, labelw, "Character", 'a'), true);
            AddChild(new LineField(height, labelw, "Text (line)", "abc"), true);
            AddChild(new MultilineField(labelw, labelw, "Text (multiline)", "abc\ndefg"), true);

            AddChild(new Vector2Field(height, labelw, "Vector2 (float)", Vector2.Zero), true);
            AddChild(new Vector3Field(height, labelw, "Vector3 (float)", Vector3.Zero), true);
            AddChild(new Vector4Field(height, labelw, "Vector4 (float)", Vector4.Zero), true);

            AddChild(new Vector2IField(height, labelw, "Vector2 (integer)", Vector2I.Zero), true);
            AddChild(new Vector3IField(height, labelw, "Vector3 (integer)", Vector3I.Zero), true);
            AddChild(new Vector4IField(height, labelw, "Vector4 (integer)", Vector4I.Zero), true);

            AddChild(new ColorField(height, labelw, "Color", Colors.Red), true);

            AddChild(new LabeledIcon(height, Icon, 8, "Icon"), true);

            AddChild(new OptionField(height, labelw, "Options", new string[] { "A", "B", "C" }, 0), true);

            AddChild(new ListEntryElement(height, 2, "List Element", 8, CreateListElementContents()), true);
            AddChild(new ListElement(height, "List", "Entry", "Add Entry", 8, CreateListElementContents()), true);

            AddChild(new Section(height, "Region", 8, CreateFoldoutContents()), true);
            AddChild(new LabelFoldout(height, "Foldout (label)", 12, false, CreateFoldoutContents()), true);
            AddChild(new CheckBoxFoldout(height, "Foldout (check box)", 12, false, CreateFoldoutContents()), true);
            AddChild(new ToggleFoldout(height, "Foldout (toggle)", 12, false, CreateFoldoutContents()), true);
        }

        private Element[] CreateFoldoutContents()
        {
            Element[] controls = new Element[3];
            controls[0] = new ToggleField(height, labelw, "Contents1", true);
            controls[1] = new FloatField(height, labelw, "Contents2", 0f);
            controls[2] = new LineField(height, labelw, "Contents3", "");
            return controls;
        }

        private Element CreateListElementContents()
        {
            return new MultilineField(height, 4, labelw, "Value", "");
        }
    }
}