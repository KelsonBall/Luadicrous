using System;
using System.Xml;
using Gtk;
using Luadicrous.Framework.Serialization;

namespace Luadicrous.Framework.Widgets.Components
{
    public class Calendar : LeafElement
    {
        private Gtk.Calendar _calendar;

        internal override Widget Widget
        {
            get { return _calendar; }
            set { _calendar = (Gtk.Calendar)value; }
        }

        public Calendar()
        {
            _calendar = new Gtk.Calendar
            {
                Date = DateTime.Now,
                DisplayOptions = CalendarDisplayOptions.ShowHeading | CalendarDisplayOptions.ShowDayNames | CalendarDisplayOptions.ShowDetails
            };
        }

        internal static ElementPair Parse(XmlNode node, Control root)
        {
            Calendar calendar = new Calendar();
            BindDate(calendar, node, root);
            return new ElementPair(
                calendar,
                e => calendar);
        }

        internal static void BindDate(Calendar element, XmlNode node, Control root)
        {
            XmlAttribute dateAttribute = node.Attribute("Date");
            if (dateAttribute != null)
            {
                if (dateAttribute.IsBinding())
                {
                    Action<EventHandler> subscribe = func =>
                    {
                        element._calendar.DaySelected += func;                        
                        element._calendar.DaySelectedDoubleClick += func;                                                
                    };

                    root.BindingContext.BindProperty(
                            subscribe,
                            () => element._calendar.Date,
                            value => element._calendar.Date = SetDateFromDynamic(value),
                            "Date",
                            dateAttribute.Value);
                }
                else
                {
                    element._calendar.Date = SetDateFromDynamic(dateAttribute.Value);
                }
            }
        }

        internal static DateTime SetDateFromDynamic(dynamic value)
        {
            if (value is DateTime)
            {
                return (DateTime)value;                
            }
            else
            {
                return DateTime.Parse((string)value);
            }
        }
    }
}
