using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Downwork_Notifier.Common
{
    public class FilterableCollection<TInner> : DependencyObject// : Freezable
    {
        #region DPs
        public ObservableCollection<TInner> FilteredCollection
        {
            get { return (ObservableCollection<TInner>)GetValue(FilteredCollectionProperty); }
            set { SetValue(FilteredCollectionProperty, value); }
        }
        // Using a DependencyProperty as the backing store for FilteredCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilteredCollectionProperty =
            DependencyProperty.Register("FilteredCollection", typeof(ObservableCollection<TInner>), typeof(FilterableCollection<TInner>), new PropertyMetadata(new ObservableCollection<TInner>()));


        public TInner FilterValue
        {
            get { return (TInner)GetValue(FilterValueProperty); }
            set { SetValue(FilterValueProperty, value); }
        }
        public static readonly DependencyProperty FilterValueProperty =
            DependencyProperty.Register("FilterValue", typeof(TInner), typeof(FilterableCollection<TInner>),
                new PropertyMetadata(default(TInner), (d,e) =>
                {
                    if (d is FilterableCollection<TInner> && e.OldValue != e.NewValue) ((FilterableCollection<TInner>)d).FilterCollection();
                }));
        #endregion DPs

        #region Properties
        public TInner[] UnfilteredCollection { get; set; } = null;
        public Func<TInner, TInner, bool> PassFilterDelegate { get; set; } = null;
        #endregion Properties

        public FilterableCollection() { }
        public FilterableCollection(TInner[] innerCollection) : this()
        {
            if (innerCollection == null || innerCollection.Length == 0)
            {
                throw new ArgumentNullException(nameof(innerCollection));
            }

            UnfilteredCollection = innerCollection;
        }
        public FilterableCollection(TInner[] innerCollection, Func<TInner, TInner, bool> passFilterDelegate) : this (innerCollection)
        {
            PassFilterDelegate = passFilterDelegate ?? throw new ArgumentNullException(nameof(passFilterDelegate));
        }

        public void FilterCollection()
        {
            if (FilterValue == null || FilterValue.Equals(default(TInner)))
            {
                FilteredCollection.Clear();
                return;
            }

            foreach (var item in UnfilteredCollection)
            {
                if (PassFilterDelegate(item, FilterValue))
                {
                    if (!FilteredCollection.Contains(item))
                    {
                        FilteredCollection.Add(item);
                    }
                }
                else
                {
                    if (FilteredCollection.Contains(item))
                    {
                        FilteredCollection.Remove(item);
                    }
                }
            }
        }
    }
}
