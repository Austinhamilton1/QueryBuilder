using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class User : IClassModel, IComparable<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OtherUserDetails { get; set; } = string.Empty;
        public double AmountOfFine { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;


        public int CompareTo(User? other)
        {
            return string.Compare(this.UserName, other.UserName);
        }

        public override string ToString()
        {
            return $"{Id}, {UserName}, {Address}, {OtherUserDetails}, {AmountOfFine}, {Email}, {PhoneNumber}";
        }
    }
}
