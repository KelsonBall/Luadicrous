using Luadicrous.Framework.Attributes;
using Luadicrous.Framework.Converters;
using Luadicrous.Framework.Extensions;
using Luadicrous.Framework.Mvvm;
using System;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public static class Binder
    {
        public static void Bind(this VisualTreeElement element, XmlNode node)
        {
            Type elementType = element.GetType();
            if (elementType.HasAttribute<BindableCollectionAttribute>())
            {
                BindCollection(element, elementType.GetCustomAttribute<BindableCollectionAttribute>(), node);
            }

            foreach (MemberInfo bindableProperty in elementType.MembersWithAttribute<BindablePropertyAttribute>(BindingFlags.Public | BindingFlags.Instance))
            {
                if (node.Attribute(bindableProperty.Name) != null)
                {
                    element.BindProperty((PropertyInfo)bindableProperty, bindableProperty.GetCustomAttribute<BindablePropertyAttribute>(), node.Attribute(bindableProperty.Name).BindingExpression());
                }
            }

            foreach (MemberInfo bindableEvent in elementType.MembersWithAttribute<BindableEventAttribute>(BindingFlags.Public | BindingFlags.Instance))
            {
                if (node.Attribute(bindableEvent.Name) != null)
                {
                    element.BindEvent(bindableEvent, node.Attribute(bindableEvent.Name).BindingExpression());
                }
            }
        }

        public static void BindProperty(this VisualTreeElement element, PropertyInfo elementProperty, BindablePropertyAttribute attribute, BindingExpression binding)
        {
            if (!binding.IsDatabound)
            {
                elementProperty.SetValue(element, BindingConverter.ConvertBindingValue(elementProperty.PropertyType, binding.Source));
                return;
            }

            Action<Action> subscriber;
            BindingMode mode;

            if (string.IsNullOrEmpty(attribute.ChangeHandler))
            {
                subscriber = handler => { };
                mode = BindingMode.FromViewModel;
            }
            else
            {
                PropertyInfo viewChangeHandler = element.GetType()
                                                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                        .Single(p => p.Name == attribute.ChangeHandler);
                subscriber = handler =>
                {
                    Action current = (Action)viewChangeHandler.GetValue(element);
                    current += handler;
                    viewChangeHandler.SetValue(element, current);
                };

                mode = BindingMode.TwoWay;
            }

            element.BindingContext.BindProperty(
                    subscriber,
                    elementProperty.CreateGetDelegate<dynamic>(element),
                    elementProperty.CreateSetDelegate<dynamic>(element),
                    elementProperty.Name,
                    binding.Target,
                    mode);
        }

        public static void BindEvent(this VisualTreeElement element, MemberInfo elementEvent, BindingExpression binding)
        {
            PropertyInfo viewChangeHandler = element.GetType()
                                                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                        .Single(p => p.Name == elementEvent.Name);

            Action<Action> subscriber = handler =>
            {
                Action current = (Action)viewChangeHandler.GetValue(element);
                current += handler;
                viewChangeHandler.SetValue(element, current);
            };

            element.BindingContext.BindCommand(subscriber, elementEvent.Name, binding.Target);
        }

        public static void BindCollection(this VisualTreeElement element, BindableCollectionAttribute attribute, XmlNode node)
        {
            Type elementType = element.GetType();
            XmlAttribute templateAttribute = node.Attribute(attribute.TemplateProperty);
            XmlAttribute sourceAttribute = node.Attribute(attribute.Source);

            if (sourceAttribute == null ^ templateAttribute == null)
            {
                throw new ArgumentException("Bindable collection source or template undefined.");
            }

            if (sourceAttribute == null)
            {
                return;
            }

            PropertyInfo property = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                               .Single(p => p.Name == attribute.TemplateProperty);

            property.SetValue(element, templateAttribute.Value);

            Action<string, dynamic> adder = elementType
                                                .GetMethod(attribute.AdderName)
                                                .CreateDelegate(typeof(Action<string, dynamic>), element)
                                                as Action<string, dynamic>;

            Action<string, dynamic> remover = elementType
                                                .GetMethod(attribute.RemoverName)
                                                .CreateDelegate(typeof(Action<string, dynamic>), element)
                                                as Action<string, dynamic>;

            element.BindingContext.BindCollection(remover, adder, sourceAttribute.BindingExpression());
        }
    }
}