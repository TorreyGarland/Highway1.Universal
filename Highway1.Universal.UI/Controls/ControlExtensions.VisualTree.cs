namespace Highway1.Universal.UI.Controls
{

    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    partial class ControlExtensions
    {

        /// <summary>Prepends an item to the beginning of an enumeration</summary>
        /// <typeparam name="T">The type of item in the enumeration</typeparam>
        /// <param name="list">The existing enumeration</param>
        /// <param name="head">The item to return before the enumeration</param>
        /// <returns>An enumerator that returns the head, followed by the rest of the list</returns>
        public static IEnumerable<T> PrependWith<T>(this IEnumerable<T> list, T head)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            yield return head;
            foreach (T item in list)
                yield return item;
        }

        /// <summary>Gets the VisualStateGroup with the given name, looking up the visual tree</summary>
        /// <param name="root">AssociatedObject to start from</param>
        /// <param name="groupName">Name of the group to look for</param>
        /// <returns>The group, if found, or null</returns>
        public static VisualStateGroup GetVisualStateGroup(this FrameworkElement root, string groupName)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));
            var selfOrAncestors = root.GetVisualAncestors().PrependWith(root);
            foreach (var element in selfOrAncestors)
            {
                var groups = VisualStateManager.GetVisualStateGroups(element);
                if (groups != null)
                {
                    var group = groups.OfType<VisualStateGroup>().FirstOrDefault(g => g.Name == groupName);
                    if (group != null)
                        return group;
                }
            }
            return null;
        }

        /// <summary>Returns a visual child of an element</summary>
        /// <param name="node">The element whose child is desired</param>
        /// <param name="index">The index of the child</param>
        /// <returns>The found child, or null if not found (usually means visual tree is not ready)</returns>
        public static FrameworkElement GetVisualChild(this FrameworkElement node, int index)
        {
            try
            {
                return VisualTreeHelper.GetChild(node, index) as FrameworkElement;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while trying to get child index " + index + ". " + ex.Message);
            }
            return null;
        }

        /// <summary>Gets the ancestors.</summary>
        /// <param name="start">The start.</param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject start)
        {
            var parent = VisualTreeHelper.GetParent(start);
            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }

        /// <summary>Gets the visual children.</summary>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static ImmutableList<DependencyObject> GetVisualChildren(this DependencyObject parent)
        {
            var results = ImmutableList<DependencyObject>.Empty;
            var childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int counter = 0; counter < childCount; counter++)
                results = results.Add(VisualTreeHelper.GetChild(parent, counter));
            return results;
        }

        /// <summary>Gets the logical children breadth first.</summary>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static ImmutableList<FrameworkElement> GetLogicalChildrenBreadthFirst(this FrameworkElement parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            var results = ImmutableList<FrameworkElement>.Empty;
            var queue = new Queue<FrameworkElement>(parent.GetVisualChildren().OfType<FrameworkElement>());
            while (queue.Count > 0)
            {
                var element = queue.Dequeue();
                results = results.Add(element);
                foreach (var visualChild in element.GetVisualChildren().OfType<FrameworkElement>())
                    queue.Enqueue(visualChild);
            }
            return results;
        }

        /// <summary>Gets the visual ancestors.</summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static ImmutableList<FrameworkElement> GetVisualAncestors(this FrameworkElement node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            var results = ImmutableList<FrameworkElement>.Empty;
            var parent = node.GetVisualParent();
            while (parent != null)
            {
                results = results.Add(parent);
                parent = parent.GetVisualParent();
            }
            return results;
        }

        /// <summary>Gets the visual parent.</summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static FrameworkElement GetVisualParent(this FrameworkElement node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            return VisualTreeHelper.GetParent(node) as FrameworkElement;
        }

        /// <summary>Gets the type of the parent by.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static T GetParentByType<T>(this DependencyObject element)
            where T : FrameworkElement
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            T result = null;
            var parent = VisualTreeHelper.GetParent(element);
            while (parent != null)
            {
                result = parent as T;
                if (result != null)
                    return result;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

    }

}