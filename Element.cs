using Godot;
using System;

namespace Rusty.EditorUI
{
    /// <summary>
    /// A base class for all fields.
    /// </summary>
	public abstract partial class Element : MarginContainer, ICloneable
    {
        /* Public properties. */
        /// <summary>
        /// The minimum height of the field.
        /// </summary>
        public virtual float Height
        {
            get => CustomMinimumSize.Y;
            set => CustomMinimumSize = new Vector2(CustomMinimumSize.X, value);
        }

        /// <summary>
        /// The indentation of this field (relative to its parent element).
        /// </summary>
        public int LocalIndentation { get; set; }
        /// <summary>
        /// The global indentation of this field (compared to the root element).
        /// </summary>
        public int GlobalIndentation
        {
            get
            {
                Element parent = GetParentElement();
                if (parent == null)
                    return LocalIndentation;
                else
                    return parent.GlobalIndentation + LocalIndentation;
            }
        }

        /* Constructors. */
        public Element()
        {
            BaseInit();
        }

        public Element(float height) : this()
        {
            Height = height;
        }

        public Element(Element other) : this()
        {
            CopyStateFrom(other);
        }

        /* Public methods. */
        /// <summary>
        /// Make a copy of this field. Also copies all nested fields.
        /// </summary>
        public abstract Element Duplicate();

        /// <summary>
        /// Copy properties and nested fields from another field of the same type.
        /// Returns true on success.
        /// </summary>
        public virtual bool CopyStateFrom(Element other)
        {
            // Make sure the types match.
            if (other.GetType() != GetType())
            {
                GD.Print("Cannot copy from other field because it's not of the same type!");
                return false;
            }

            // Copy internal state.
            Height = other.Height;
            LocalIndentation = other.LocalIndentation;

            return true;
        }

        /// <summary>
        /// Get the parent element. We only consider an element our parent either if it's our direct parent, or if ALL nodes
        /// in-between it and us are container nodes.
        /// </summary>
        public Element GetParentElement()
        {
            Node parent = GetParent();
            while (parent != null)
            {
                if (parent is Element element)
                    return element;
                else if (parent is Container)
                    parent = parent.GetParent();
                else
                    break;
            }
            return null;
        }

        /* Godot overrides. */
        public override void _Process(double delta)
        {
            AddThemeConstantOverride("margin_left", GlobalIndentation);
        }

        /* Protected methods. */
        /// <summary>
        /// Initialize this field. Note that you cannot access nested fields during the execution of this method, as doing do
        /// would break the duplicate method!
        /// </summary>
        protected virtual void Init() { }

        /* Private methods. */
        /// <summary>
        /// The default, non-overridable initialization that all fields undergo upon creation.
        /// Calls the overridable init method.
        /// </summary>
        private void BaseInit()
        {
            // Set size flags.
            SizeFlagsHorizontal = SizeFlags.ExpandFill;

            // Set default properties.
            Height = 32f;

            // Call overridable init method.
            Init();
        }

        object ICloneable.Clone()
        {
            return Duplicate();
        }
    }
}