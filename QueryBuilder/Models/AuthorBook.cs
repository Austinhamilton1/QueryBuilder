using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class AuthorBook : IClassModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }


        public override string ToString()
        {
            return $"{Id}, {AuthorId}, {BookId}";
        }
    }
}
