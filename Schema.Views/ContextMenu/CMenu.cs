using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Schema.Views.ContextMenu
{
    class CMenu : System.Windows.Controls.ContextMenu
    {
        public CMenu()
        {
            IsVisibleChanged += CMenu_IsVisibleChanged;

        }

        private void CMenu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var placementType = PlacementTarget.GetType();
            if (placementType == typeof(ListBox))
            {
                var listBox = (ListBox)PlacementTarget;
                foreach (var item in Items)
                {
                    var menuItem = item as MenuItem;
                    if (menuItem != null)
                    {
                        var visibleFor =(Type)menuItem.GetValue(VisibleForProperty);
                        

                        var selectionMode = (ESelectionMode)menuItem.GetValue(SelectionModeProperty);
                        var mismatchAction = (ESelectionMismatchAction)menuItem.GetValue(SelectionMismatchProperty);

                        switch (selectionMode)
                        {
                            case ESelectionMode.None:
                                break;
                            case ESelectionMode.Single:
                                if (listBox.SelectedItem.GetType() != visibleFor)
                                {
                                    menuItem.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    if (listBox.SelectedItems.Count == 1)
                                    {

                                        menuItem.CommandParameter = listBox.SelectedItem;
                                        menuItem.Visibility = Visibility.Visible;
                                        menuItem.IsEnabled = true;
                                    }
                                    else
                                    {
                                        if (mismatchAction == ESelectionMismatchAction.Disable)
                                            menuItem.IsEnabled = false;
                                        else if (mismatchAction == ESelectionMismatchAction.Hide)
                                            menuItem.Visibility = Visibility.Collapsed;
                                    }
                                }
                                break;
                            case ESelectionMode.Double:
                                break;
                            case ESelectionMode.Multiple:
                                break;
                            case ESelectionMode.Both:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }


        #region SelectionMode
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.RegisterAttached(
            "SelectionMode", typeof(ESelectionMode), typeof(CMenu), new PropertyMetadata(ESelectionMode.None));

        public static void SetSelectionMode(DependencyObject element, ESelectionMode value)
        {
            element.SetValue(SelectionModeProperty, value);
        }

        public static ESelectionMode GetSelectionMode(DependencyObject element)
        {
            return (ESelectionMode)element.GetValue(SelectionModeProperty);
        }
        #endregion

        #region SelectionMismatch
        public static readonly DependencyProperty SelectionMismatchProperty = DependencyProperty.RegisterAttached(
            "SelectionMismatch", typeof(ESelectionMismatchAction), typeof(CMenu), new PropertyMetadata(default(ESelectionMismatchAction)));

        public static void SetSelectionMismatch(DependencyObject element, ESelectionMismatchAction value)
        {
            element.SetValue(SelectionMismatchProperty, value);
        }

        public static ESelectionMismatchAction GetSelectionMismatch(DependencyObject element)
        {
            return (ESelectionMismatchAction)element.GetValue(SelectionMismatchProperty);
        }
        #endregion

        #region VisibleFor
        public static readonly DependencyProperty VisibleForProperty = DependencyProperty.RegisterAttached(
            "VisibleFor", typeof(Type), typeof(CMenu), new PropertyMetadata(default(object)));

        public static void SetVisibleFor(DependencyObject element, Type value)
        {
            element.SetValue(VisibleForProperty, value);
        }

        public static Type GetVisibleFor(DependencyObject element)
        {
            return (Type)element.GetValue(VisibleForProperty);
        }
        #endregion
    }
}
