using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            Member member = new Member("Aram", "student");
            Book book = new Book("Samvel","S1","dramma","Raffi");
            library.AddMember(member);
            library.AddBook(book);
            library.AddBorrowedBook(member, "Samvel", "Raffi");
            library.DisplayMembers();
            library.DisplayBooks();
        }
    }

    abstract class LibraryEntity
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public LibraryEntity(string name)
        {
            Name = name;
        }

        abstract public void DisplayDetails();
    }

    class Book : LibraryEntity
    {
        public Book(string name, string iSBN, string category, string author) : base(name)
        {
            ISBN = iSBN;
            Category = category;
            Author = author;
        }
        public string Author {  get; set; }
        public string ISBN { get; set; }
        public string Category { get; set; }

        public override void DisplayDetails()
        {
            Console.WriteLine($"{Author} | {Name} | {Category} | {ISBN} | {Id}");
        }
    }

    class Member : LibraryEntity
    {
        public Member(string name, string memberType) : base(name)
        {
            MembershipType = memberType;
        }
        public string MembershipType {  get; set; }

        public List<string> BorrowedBooks { get; set; } = new List<string>();

        public override void DisplayDetails()
        {
            string borrowedBooks = BorrowedBooks.Count > 0 ? string.Join(", ", BorrowedBooks) : "No books borrowed";
            Console.WriteLine($"{Name} | {MembershipType} | {borrowedBooks} | {Id}");
        }
      
    }

    class Library
    {
        List<Member> members = new List<Member>();
        List<Book> books = new List<Book>();

        public void AddBorrowedBook(Member member, string name, string author)
        {
            foreach (Book item in books)
            {
                if (item.Name == name || item.Author == author)
                {
                    member.BorrowedBooks.Add($"{name} by {author}");
                    Console.WriteLine($"{member.Name} borrowed {name} by {author} {item.Id}");
                    return; 
                }
            }
            Console.WriteLine($"Book \"{name}\" by {author} not found or already borrowed.");
        }
        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void AddMember(Member member)
        {
            members.Add(member);
        }

        public void DisplayBooks()
        {
            foreach(Book el in books)
            {
                el.DisplayDetails();
            }
        }

        public void DisplayMembers()
        {
            foreach (Member el in members)
            {
                el.DisplayDetails();
            }
        }
    }
}
