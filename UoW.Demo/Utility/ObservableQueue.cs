using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace UoW.Demo.Utility
{
    public class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region Constructors

        public ObservableQueue() : base() { }

        public ObservableQueue(IEnumerable<T> collection) : base(collection) { }

        public ObservableQueue(int capacity) : base(capacity) { }

        #endregion

        #region Overrides

        public virtual new T Dequeue()
        {
            var item = base.Dequeue();
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
            return item;
        }

        public virtual new void Enqueue(T item)
        {
            base.Enqueue(item);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
        }

        public virtual new void Clear()
        {
            base.Clear();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset, default);
        }

        #endregion

        #region CollectionChanged

        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T item)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(
                action
                , item
                , item == null ? -1 : 0)
            );

            OnPropertyChanged(nameof(Count));
        }

        #endregion

        #region PropertyChanged

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string proertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proertyName));
        }

        #endregion
    }

}
