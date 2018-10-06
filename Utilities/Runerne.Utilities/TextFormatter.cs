using System.Collections.Generic;
using System.Linq;

namespace Runerne.Utilities
{
    /// <summary>
    /// The TextFormatter class is capable of working with tabulators, i.e. tabulators can be set, identified and removed. With an instance
    /// of the TextFormatter, a text can be formatted accordingly. This is made, by using tabulators (\ t) in the text, which is formatted.
    /// </summary>
    public class TextFormatter
    {
        /// <summary>
        /// Represents a tabulator entry in the TextFormatter.
        /// </summary>
        public class Tab
        {
            /// <summary>
            /// The position of the tabulator.
            /// </summary>
            public int Position { get; }

            /// <summary>
            /// Creates a TextFormatter tabulator with the specified position.
            /// </summary>
            /// <param name="position">The position of the tabulator.</param>
            public Tab(int position)
            {
                Position = position;
            }
        }

        /// <summary>
        /// The tabulators contained in the TextFormatter.
        /// </summary>
        public IEnumerable<Tab> Tabs
        {
            get { return _tabs; }
        }

        private readonly List<Tab> _tabs = new List<Tab>();

        /// <summary>
        /// Sets a tabulator in the TextFormatter at the specified position.
        /// </summary>
        /// <param name="position">The position of the tabulator.</param>
        /// <returns>The tabulator.</returns>
        public Tab SetTab(int position)
        {
            var newTab = new Tab(position);

            for (var i = 0; i < _tabs.Count; i++)
            {
                if (_tabs[i].Position >= position)
                {
                    _tabs.Insert(i, newTab);
                    return newTab;
                }
            }

            _tabs.Add(newTab);
            return newTab;
        }

        /// <summary>
        /// Returns all the tabulators set on the specified position.
        /// </summary>
        /// <param name="position">The position of the tabulators to be searched.</param>
        /// <returns>The tabulators set on the specified position</returns>
        public IEnumerable<Tab> TabsAt(int position)
        {
            return _tabs.Where(o => o.Position == position);
        }

        /// <summary>
        /// Clears (removces) all the specified tabulators (by instance) from the TextFormatter.
        /// </summary>
        /// <param name="tabs">The tabulators, which will be cleared.</param>
        public void ClearTabs(IEnumerable<Tab> tabs)
        {
            foreach (var tab in tabs)
                ClearTab(tab);
        }

        /// <summary>
        /// Clears (removes) one specific tabulator (by instance) from the TextFormatter.
        /// </summary>
        /// <param name="tab">The tabulator, which will be cleared.</param>
        public void ClearTab(Tab tab)
        {
            _tabs.Remove(tab);
        }

        /// <summary>
        /// Formats a text according to the set tabulators and specified tabulator entries in the specified text.
        /// </summary>
        /// <param name="textBlock">The text to be formatted.</param>
        /// <returns>The formatted text.</returns>
        public string FormatTextBlock(string textBlock)
        {
            var iTab = -1;
            var tabPos = 0;
            var formattedLine = "";
            foreach (var ch in textBlock.ToCharArray())
            {
                switch (ch)
                {
                    case '\t':
                        iTab++;
                        if (iTab < _tabs.Count)
                        {
                            tabPos = _tabs[iTab].Position;
                            formattedLine = formattedLine.PadRight(tabPos).Substring(0, tabPos);
                        }
                        break;

                    case '\n':
                        {
                            formattedLine += ch;
                            formattedLine += "".PadRight(tabPos);
                        }
                        break;

                    default:
                        formattedLine += ch;
                        break;
                }
            }

            return formattedLine;
        }
    }
}
