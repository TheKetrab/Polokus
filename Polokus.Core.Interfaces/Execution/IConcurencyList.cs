﻿namespace Polokus.Core.Interfaces.Execution
{
    public interface IConcurencyList<T>
    {
        public void Add(T item);
        public int Remove(Predicate<T> predicate);
        public List<T> GetAll();
        public bool Any();
        public bool Any(Predicate<T> predicate);
    }
}