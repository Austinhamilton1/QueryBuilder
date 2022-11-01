using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class Book : IClassModel, IComparable<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;


        public int CompareTo(Book? other)
        {
            return string.Compare(this.Title, other.Title);
        }

        public override string ToString()
        {
            return $"{Id}, {Title}, {Isbn}";
        }
    }
}
