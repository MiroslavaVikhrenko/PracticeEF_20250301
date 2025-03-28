namespace Task18_20250328
{
    /*
     Разработайте систему управления студентами и курсами для университета. В системе есть информация о студентах, 
    курсах, преподавателях и результате их обучения.

Определите следующие классы:
■ Student (Студент): Информация о студентах, включая их идентификатор, имя, фамилию и дату рождения.
■ Course (Курс): Информация о курсах, включая их идентификатор, название и описание.
■ Enrollment (Зачисление): Информация о зачислении студентов на курсы, включая идентификатор зачисления, 
    идентификатор студента, идентификатор курса и дату зачисления.
■ Instructor (Преподаватель): Информация о преподавателях, включая их идентификатор, имя и фамилию.

Используя методы расширения LINQ TO ENTITIES, выполните следующие запросы:

1) Получить список студентов, зачисленных на определенный курс.
2) Получить список курсов, на которых учит определенный преподаватель.
3) Получить список курсов, на которых учит определенный преподаватель, вместе с именами студентов, зачисленных на каждый курс.
4) Получить список курсов, на которые зачислено более 5 студентов.
5) Получить список студентов, старше 25 лет.
6) Получить средний возраст всех студентов.
7) Получить самого молодого студента.
8) Получить количество курсов, на которых учится студент с определенным Id.
9) Получить список имен всех студентов.
10) Сгруппировать студентов по возрасту.
11) Получить список студентов, отсортированных по фамилии в алфавитном порядке.
12) Получить список студентов вместе с информацией о зачислениях на курсы.
13) Получить список студентов, не зачисленных на определенный курс.
14) Получить список студентов, зачисленных одновременно на два определенных курса.
15) Получить количество студентов на каждом курсе.

     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Student> students = new List<Student>()
                {
                    new Student(){Name = "Taro", FamilyName = "Yamamoto", BirthDay = new DateTime(2000,04,30)},
                    new Student(){Name = "Keisuke", FamilyName = "Kobayashi", BirthDay = new DateTime(2001,01,20)},
                    new Student(){Name = "Satoshi", FamilyName = "Fujimoto", BirthDay = new DateTime(1999,02,10)},
                    new Student(){Name = "Hiroo", FamilyName = "Ishizuka", BirthDay = new DateTime(1998,03,12)},
                    new Student(){Name = "Hiroshi", FamilyName = "Sato", BirthDay = new DateTime(2003,05,11)},
                    new Student(){Name = "Takeshi", FamilyName = "Okuda", BirthDay = new DateTime(2002,06,27)},
                    new Student(){Name = "Kenji", FamilyName = "Ishikawa", BirthDay = new DateTime(2004,07,19)},
                    new Student(){Name = "Midori", FamilyName = "Matsuda", BirthDay = new DateTime(2005,08,25)},
                    new Student(){Name = "Yuka", FamilyName = "Endo", BirthDay = new DateTime(2006,09,21)},
                    new Student(){Name = "Akiko", FamilyName = "Shimizu", BirthDay = new DateTime(2005,10,15)}
                };
                db.Students.AddRange(students);
                db.SaveChanges();

                List<Instructor> instructors = new List<Instructor>()
                {
                    new Instructor(){Name = "Ryu", FamilyName = "Sakamoto"},
                    new Instructor(){Name = "Yuki", FamilyName = "Takahashi"},
                    new Instructor(){Name = "Mai", FamilyName = "Hirano"},
                    new Instructor(){Name = "Sakura", FamilyName = "Shirakawa"},
                    new Instructor(){Name = "Haruto", FamilyName = "Sakai"},
                    new Instructor(){Name = "Daiki", FamilyName = "Yamada"},
                    new Instructor(){Name = "Souta", FamilyName = "Nakagawa"}
                };
                db.Instructors.AddRange(instructors);
                db.SaveChanges();

                List<Course> courses = new List<Course>()
                {
                    new Course(){Name = "C#", Description = "Advanced", InstructorId = 1},
                    new Course(){Name = "C++", Description = "Basics", InstructorId = 1},
                    new Course(){Name = "Maths", Description = "Theory lectures", InstructorId = 2},
                    new Course(){Name = "Physics", Description = "Lab lessons", InstructorId = 3},
                    new Course(){Name = "English", Description = "Speaking", InstructorId = 4},
                    new Course(){Name = "Presentation", Description = "Soft skills lab", InstructorId = 5},
                    new Course(){Name = "Japanese", Description = "Writing + kanji practice", InstructorId = 6},
                    new Course(){Name = "Databases", Description = "Fundamentals", InstructorId = 7},
                    new Course(){Name = "Design patterns", Description = "Series of workshops", InstructorId = 7},
                    new Course(){Name = "Coding", Description = "Discuss use cases and best practices", InstructorId = 2}
                };

                db.Courses.AddRange(courses);
                db.SaveChanges();

                List<Enrollment> enrollments = new List<Enrollment>()
                {
                    new Enrollment(){StudentId = 1, CourseId = 1},
                    new Enrollment(){StudentId = 1, CourseId = 3},
                    new Enrollment(){StudentId = 1, CourseId = 10},
                    new Enrollment(){StudentId = 2, CourseId = 2},
                    new Enrollment(){StudentId = 2, CourseId = 6},
                    new Enrollment(){StudentId = 2, CourseId = 9},
                    new Enrollment(){StudentId = 3, CourseId = 3},
                    new Enrollment(){StudentId = 3, CourseId = 7},
                    new Enrollment(){StudentId = 3, CourseId = 9},
                    new Enrollment(){StudentId = 4, CourseId = 4},
                    new Enrollment(){StudentId = 4, CourseId = 5},
                    new Enrollment(){StudentId = 4, CourseId = 10},
                    new Enrollment(){StudentId = 5, CourseId = 2},
                    new Enrollment(){StudentId = 6, CourseId = 6},
                    new Enrollment(){StudentId = 6, CourseId = 10},
                    new Enrollment(){StudentId = 7, CourseId = 3},
                    new Enrollment(){StudentId = 7, CourseId = 5},
                    new Enrollment(){StudentId = 8, CourseId = 1},
                    new Enrollment(){StudentId = 8, CourseId = 2},
                    new Enrollment(){StudentId = 8, CourseId = 3},
                    new Enrollment(){StudentId = 9, CourseId = 1},
                    new Enrollment(){StudentId = 9, CourseId = 4},
                    new Enrollment(){StudentId = 9, CourseId = 8},
                    new Enrollment(){StudentId = 10, CourseId = 2},
                    new Enrollment(){StudentId = 10, CourseId = 3},
                    new Enrollment(){StudentId = 10, CourseId = 7},
                    new Enrollment(){StudentId = 10, CourseId = 8}
                };
                db.Enrollments.AddRange(enrollments);
                db.SaveChanges();

                // 1) Получить список студентов, зачисленных на определенный курс.
                GetAllStudentsByCourse(1, db);

                // 2) Получить список курсов, на которых учит определенный преподаватель.
                GetAllCoursesByInstructor(1, db);
            }
        }
        // 2) Получить список курсов, на которых учит определенный преподаватель.
        public static void GetAllCoursesByInstructor(int instructorId, ApplicationContext db)
        {
            // Fetch instructor details
            var instructor = db.Instructors.FirstOrDefault(i => i.Id == instructorId);

            if (instructor == null)
            {
                Console.WriteLine($"Instructor with ID {instructorId} not found.");
                return;
            }

            // Fetch courses taught by this instructor
            var courses = db.Courses
                            .Where(c => c.InstructorId == instructorId)
                            .ToList();

            // Print instructor's name
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine($"Instructor: {instructor.Name} {instructor.FamilyName}");

            // Print courses or indicate if none exist
            if (courses.Any())
            {
                Console.WriteLine("Courses taught:");
                foreach (var course in courses)
                {
                    Console.WriteLine($"- {course.Name}: {course.Description}");
                }
            }
            else
            {
                Console.WriteLine("This instructor does not teach any courses.");
            }
        }
        // 1) Получить список студентов, зачисленных на определенный курс.
        public static void GetAllStudentsByCourse(int courseId, ApplicationContext db)
        {
            // Fetch the course details
            var course = db.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine($"Course with ID {courseId} not found.");
                return;
            }

            // Fetch students enrolled in the course
            var students = db.Enrollments
                             .Where(e => e.CourseId == courseId)
                             .Select(e => e.Student)
                             .ToList();

            // Print results
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine($"Course: {course.Name} ({course.Description})");

            if (students.Any())
            {
                Console.WriteLine("Enrolled Students:");
                foreach (var student in students)
                {
                    Console.WriteLine($"- {student.Name} {student.FamilyName}, Born on: {student.BirthDay.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("No students are enrolled in this course.");
            }
        }
    }
}
