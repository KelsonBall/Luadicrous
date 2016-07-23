using System;
using System.Collections.Generic;

namespace Luadicrous.Framework
{
	public abstract class VisualTree<T> where T : VisualTree<T>
	{
		private T _Root;
		public ValueChangedEventHandler<T> RootChanged { get; set; }
		public T Root
		{
			get { return _Root; }
			private set
			{
				_Root = value;
				RootChanged?.Invoke(value);
				ForAllDecendents(element => element.RootChanged?.Invoke(value));
			}
		}

		private T _Parent;
		public ValueChangedEventHandler<T> AncestorChanged { get; set; }
		public ValueChangedEventHandler<T> ParentChanged { get; set; }
		public T Parent
		{
			get { return _Parent; }
			set
			{
				_Parent = value;
				ParentChanged?.Invoke(value);
				ForAllDecendents(element => element.AncestorChanged?.Invoke(value));
				T root = getRoot((T)this);
				if (root != Root)
				{
					Root = root;
				}
			}
		}

		private List<T> _Children = new List<T>();
		public IReadOnlyCollection<T> Children
		{
			get { return _Children.AsReadOnly(); }
		}

		public ValueChangedEventHandler<T> DecendentAdded { get; set; }
		public ValueChangedEventHandler<T> ChildAdded { get; set; }
		protected virtual T AddChildren(params T[] children)
		{
			foreach (var child in children)
			{
				child.Parent = (T)this;
				_Children.Add(child);
				ChildAdded?.Invoke(child);
				ForAllAncestors(element => element.DecendentAdded?.Invoke(child));
			}
			return (T)this;
		}

		public ValueChangedEventHandler<T> DecendentRemoved { get; set; }
		public ValueChangedEventHandler<T> ChildRemoved { get; set; }
		protected virtual T RemoveChildren(params T[] children)
		{
			foreach (var child in children)
			{
				child.Parent = null;
				_Children.Remove(child);
				ChildRemoved?.Invoke(child);
				ForAllAncestors(element => element.DecendentRemoved?.Invoke(child));
			}
			return (T)this;
		}

		public T ForAllAncestors(Action<T> action)
		{
			if (Parent != null)
				action(Parent);

			Parent?.ForAllAncestors(action);

			return (T)this;
		}

		public T ForAllDecendents(Action<T> action)
		{
			foreach (T child in Children)
			{
				action(child);
				child.ForAllDecendents(action);
			}
			return (T)this;
		}

		private static T getRoot(T element)
		{
			return element.Parent == null ? element : getRoot(element.Parent);
		}

		public VisualTree()
		{
			AncestorChanged += (element) =>
			{
				T root = getRoot((T)this);
				if (root != Root)
					Root = root;
			};
		}
	}
}

