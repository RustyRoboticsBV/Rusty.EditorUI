using Godot;
using System.Collections.Generic;

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

        public int IndexOf(Element element)
        {
            return Elements.IndexOf(element);
        }

        public Element GetAt(int index)
        {
            return Elements[index];
        }

        public void Add(Element element)
        {
            Elements.Add(element);
            Contents.AddChild(element);
        }

        public void Insert(int index, Element element)
        {
            Elements.Insert(index, element);
            Contents.AddChild(element);
            Contents.MoveChild(element, index);
        }

        public Element Duplicate(Element element)
        {
            Element copy = element.Duplicate();
            Insert(IndexOf(element), copy);
            return copy;
        }

        public void MoveUp(Element element)
        {
            MoveUpAt(IndexOf(element));
        }

        public void MoveUpAt(int index)
        {
            if (index == 0)
                SwapAt(0, Count - 1);
            else
                SwapAt(index, index - 1);
        }

        public void MoveDown(Element element)
        {
            MoveDownAt(IndexOf(element));
        }

        public void MoveDownAt(int index)
        {
            if (index == Count - 1)
                SwapAt(0, Count - 1);
            else
                SwapAt(index, index + 1);
        }

        public void Swap(Element element1, Element element2)
        {
            SwapAt(IndexOf(element1), IndexOf(element2));
        }

        public void SwapAt(int index1, int index2)
        {
            Element element1 = Elements[index1];
            Element element2 = Elements[index2];

            Elements[index1] = element2;
            Elements[index2] = element1;
            Contents.MoveChild(element1, index2);
            Contents.MoveChild(element2, index1);
        }

        public void Remove(Element element)
        {
            Elements.Remove(element);
            Contents.RemoveChild(element);
        }

        public void RemoveAt(int index)
        {
            Contents.RemoveChild(Elements[index]);
            Elements.RemoveAt(index);
        }

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