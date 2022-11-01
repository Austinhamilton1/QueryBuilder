using QueryBuilder;
using QueryBuilder.Models;

var database = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + @"\Data\LibraryDB.db";

List<Author> authors;
Author newAuthor;
using (var qb = new QueryBuilder.QueryBuilder(database))
{
    newAuthor = new Author();
    newAuthor.Id = 500;
    newAuthor.FirstName = "Fyodor";
    newAuthor.Surname = "Dostoevsky";
    qb.Create<Author>(newAuthor);
    authors = qb.ReadAll<Author>();
}

authors.Sort();

Console.WriteLine("Add Fyodor Dostoevsky to the database");
foreach(Author a in authors)
    Console.WriteLine(a);

Console.WriteLine();
Console.WriteLine("Change Dostoevsky to Joseph Heller");

using (var qb = new QueryBuilder.QueryBuilder(database))
{
    newAuthor.FirstName = "Joseph";
    newAuthor.Surname = "Heller";
    qb.Update<Author>(newAuthor);
    authors = qb.ReadAll<Author>();
}

authors.Sort();

foreach(Author a in authors)
    Console.WriteLine(a);

Console.WriteLine();
Console.WriteLine("Delete Joseph Heller");

using (var qb = new QueryBuilder.QueryBuilder(database))
{
    qb.Delete<Author>(newAuthor);
    authors = qb.ReadAll<Author>();
}

foreach (Author a in authors)
    Console.WriteLine(a);

Console.WriteLine();
Console.WriteLine("Read the Author with Id 99");

using (var qb = new QueryBuilder.QueryBuilder(database))
{
    Console.WriteLine(qb.Read<Author>(99));
}