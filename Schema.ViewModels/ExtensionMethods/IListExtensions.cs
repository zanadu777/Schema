using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.ViewModels.ExtensionMethods
{
    public static class IListExtensions
    {

        public static ObservableCollection<T> ToObservableCollection<T>(this IList iList)
        {
            var collection = new ObservableCollection<T>();
            foreach (T item in iList)
                collection.Add(item);

            return collection;
        }
    }
}
