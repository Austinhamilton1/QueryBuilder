namespace QueryBuilder.Models
{
    public class Category : IClassModel, IComparable<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public int CompareTo(Category? other)
        {
            return string.Compare(this.Name, other.Name);
        }

        public override string ToString()
        {
            return $"{Id}, {Name}";
        }
    }
}
