using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class Author : IClassModel, IComparable<Author>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;


        public int CompareTo(Author? other)
        {
            return string.Compare(this.Surname, other.Surname);
        }

        public override string ToString()
        {
            return $"{Id}, {FirstName}, {Surname}";
        }
    }
}
