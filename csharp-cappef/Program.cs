using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_cappef
{
    internal class Program
    {
        //funzione potenza del 2 (generatore)
        public static System.Collections.Generic.IEnumerable<int> Power(int number)
        {
            int result = 1;

            while (true)
            {
                result = result * number;
                yield return result;
                //riparte da dove rimane
            }
        }

        public static System.Collections.Generic.IEnumerable<int> Genera()
        {
            int result = 1;

            while (true)
            {
                yield return result++;
                //riparte da dove rimane
            }
        }

        public static System.Collections.Generic.IEnumerable<int> Genera2(long n)
        {
            int result = 1;

            while (true)
            {
                yield return result++;
                //riparte da dove rimane
            }
        }

        public static System.Collections.Generic.IEnumerable<long> Genera3(long n)
        {
            long result = 1;

            while (n-- > 0)
            {
                yield return result++;
                //riparte da dove rimane
            }
        }

        public static Func<int> accumulatore(int number) 
        {
            int result = 1;

            return () =>
            {
                result = result * number;
                return result;
            };
        }

        static void Main(string[] args)
        {
            /*
                Console.WriteLine("Hello World!");


                //metodo 1 con uso metodo "Power"
                int cicli = 0;
                foreach (int n in Power(2))
                {
                    Console.Write("{0}, ", n);
                    if (cicli++ == 16)
                        break;
                }
                Console.WriteLine();

                Console.WriteLine("\n");


                //metodo 2 con uso metodo "accumulatore"
                var AccumulatoreUno = accumulatore(2);

                for (int i = 0; i < 16; ++i)
                    Console.Write("{0}, ", AccumulatoreUno());
                Console.WriteLine();
            */

            //contare i numeri che contengono la cifra 2, compresi tra 1 e 1000000000
            //1. metodo, non consuma memoria
            long conta = 0;
            for (long i = 0; i < 10000000; i++)
            {
                if (i.ToString().Contains('2'))
                    conta++;
            }
            Console.WriteLine(conta);

            //2. metodo
            long conta1 = 0;
            long letti = 0;
            foreach(int n in Genera())
            {
                if (n.ToString().Contains('2'))
                    conta1++;
                if (letti++ > 10000000)
                    break;
                
            }
            Console.WriteLine(conta1);

            //3. metodo
            long conta2 = 0;
            foreach (int n in Genera2(10000000))
            {
                if (n.ToString().Contains('2'))
                    conta2++;
            }
            Console.WriteLine(conta2);

            //4. metodo
            List<int> li = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine(Genera2(10000000).Count());
            Console.WriteLine(Genera2(10000000).Average());
            Console.WriteLine(li.Average());


            //stampa numeri pari tra 1 e 10000000
            Console.WriteLine(Genera3(10000000).Where(n => n % 2 == 0).Sum());


            return;

            using (SchoolContext db = new SchoolContext())
            {
                // Create
                Student nuovoStudente = new Student { Name = "Mircea" };
                db.Add(nuovoStudente);
                db.SaveChanges();

                // Read
                Console.WriteLine("Ottenere lista di Studenti");
                List<Student> students = db.Students
                   .OrderBy(student => student.Name).ToList<Student>();

                students.ForEach(s => Console.WriteLine(s));
            }
        }

        [Table("student")]
        [Index(nameof(Email), IsUnique = true)]
        public class Student
        {
            [Key]
            public int StudentId { get; set; }
            [Required]
            public string Name { get; set; }
            public string? Surname { get; set; }
            [Column("student_email")]
            public string? Email { get; set; }
            public List<Course> FrequentedCourses { get; set; }
            public List<Review> Reviews { get; set; }
        }

        [Table("course")]
        public class Course
        {
            [Key]
            public int CourseId { get; set; }
            public string Name { get; set; }

            public List<Student> StudentsEnrolled { get; set; }
        }

        [Table("course_image")]
        public class CourseImage
        {
            [Key]
            public int CourseImageId { get; set; }
            public byte[] Image { get; set; }
            public string Caption { get; set; }

            public int CourseId { get; set; }
            public Course Course { get; set; }
        }

        [Table("review")]
        public class Review
        {
            public int ReviewId { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public int StudentId { get; set; }
            public Student Student { get; set; }
        }

        public class SchoolContext : DbContext
        {
            public DbSet<Student> Students { get; set; }
            public DbSet<Course> Courses { get; set; }
            public DbSet<CourseImage> CourseImages { get; set; }
            public DbSet<Review> Reviews { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=university;Integrated Security=True;Pooling=False");
            }
        }


    }
}
