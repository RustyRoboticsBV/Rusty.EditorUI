using Godot;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Rusty.EditorUI
{
    /// <summary>
    /// An element container.
    /// </summary>
    public abstract partial class ElementContainer<T> : Element where T : Container, new()
    {
        /* Public properties. */
        public int Count => Elements.Count;

        /* Protected properties. */
        protected T Contents { get; private set; }

        /* Private properties. */
        private List<Element> Elements { get; } = new();

        /* Indexers. */
        public Element this[int index] => GetAt(index);

        /* Public methods. */
        public override bool CopyStateFrom(Element other)
        {
            if (base.CopyStateFrom(other) && other is ElementContainer<T> otherContainer)
            {
                Clear();
                for (int i = 0; i < otherContainer.Count; i++)
                {
                    Add(otherContainer[i].Duplicate());
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the index of a child element.
        /// </summary>
        public int IndexOf(Element element)
        {
            return Elements.IndexOf(element);
        }

        /// <summary>
        /// Get the child element at some index.
        /// </summary>
        public Element GetAt(int index)
        {
            return Elements[index];
        }

        /// <summary>
        /// Add a child element.
        /// </summary>
        public void Add(Element element)
        {
            Elements.Add(element);
            Contents.AddChild(element);
        }

        /// <summary>
        /// Insert a child element at some index.
        /// </summary>
        public void InsertAt(int index, Element element)
        {
            Elements.Insert(index, element);
            Contents.AddChild(element);
            Contents.MoveChild(element, index);
        }

        /// <summary>
        /// Duplicate a child element.
        /// </summary>
        public Element Duplicate(Element element)
        {
            Element copy = element.Duplicate();
            InsertAt(IndexOf(element), copy);
            return copy;
        }

        /// <summary>
        /// Decrement the index of some element on the container.
        /// </summary>
        public void MoveUp(Element element)
        {
            try
            {
                MoveUpAt(IndexOf(element));
            }
            catch
            {
                throw new ArgumentException($"The element '{element.Name}' was not stored on the container '{Name}' and could "
                    + "not be moved.");
            }
        }

        /// <summary>
        /// Decrement the index of an element at some index.
        /// </summary>
        public void MoveUpAt(int index)
        {
            if (index == 0)
                SwapAt(0, Count - 1);
            else
                SwapAt(index, index - 1);
        }

        /// <summary>
        /// Increment the index of some element on the container.
        /// </summary>
        public void MoveDown(Element element)
        {
            try
            {
                MoveDownAt(IndexOf(element));
            }
            catch
            {
                throw new ArgumentException($"The element '{element.Name}' was not stored on the container '{Name}' and could "
                    + "not be moved.");
            }
        }

        /// <summary>
        /// Increment the index of an element at some index.
        /// </summary>
        public void MoveDownAt(int index)
        {
            if (index == Count - 1)
                SwapAt(0, Count - 1);
            else
                SwapAt(index, index + 1);
        }

        /// <summary>
        /// Swap the positions of two elements on the container.
        /// </summary>
        public void Swap(Element element1, Element element2)
        {
            int index1 = IndexOf(element1);
            if (index1 == -1)
            {
                throw new ArgumentException($"The element '{element1.Name}' was not stored on the container '{Name}' and could "
                    + "not be moved.");
            }
            int index2 = IndexOf(element2);
            if (index2 == -1)
            {
                throw new ArgumentException($"The element '{element2.Name}' was not stored on the container '{Name}' and could "
                    + "not be moved.");
            }
            SwapAt(index1, index2);
        }

        /// <summary>
        /// Swap the positions of two elements on the container.
        /// </summary>
        public void SwapAt(int index1, int index2)
        {
            Element element1 = Elements[index1];
            Element element2 = Elements[index2];

            Elements[index1] = element2;
            Elements[index2] = element1;
            Contents.MoveChild(element1, index2);
            Contents.MoveChild(element2, index1);
        }

        /// <summary>
        /// Remove an element from the container.
        /// </summary>
        public void Remove(Element element)
        {
            try
            {
                Elements.Remove(element);
                Contents.RemoveChild(element);
            }
            catch
            {
                throw new ArgumentException($"The element '{element.Name}' was not stored on the container '{Name}' and could "
                    + "not be removed.");
            }
        }

        /// <summary>
        /// Remove an element at some index from the container.
        /// </summary>
        public void RemoveAt(int index)
        {
            Contents.RemoveChild(Elements[index]);
            Elements.RemoveAt(index);
        }

        /// <summary>
        /// Remove all elements from the container.
        /// </summary>
        public void Clear()
        {
            while (Count > 0)
            {
                Contents.RemoveChild(Elements[0]);
                Elements.RemoveAt(0);
            }
        }

        /* Protected methods. */
        protected override void Init()
        {
            // Element jnit.
            base.Init();

            // Create container.
            Contents = new();
            Contents.Name = "Contents";
            Contents.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            Contents.SizeFlagsVertical = SizeFlags.ExpandFill;
            AddChild(Contents);
        }
    }
}