using Microsoft.EntityFrameworkCore;

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
                    new Enrollment(){StudentId = 6, CourseId = 1},
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

                // 3) Получить список курсов, на которых учит определенный преподаватель, вместе с именами студентов, зачисленных на каждый курс.
                GetAllCoursesWithStudentsByInstructor(2, db);

                // 4) Получить список курсов, на которые зачислено более 5 студентов.
                GetCoursesWithMoreThanThreeStudents(db);

                // 5) Получить список студентов, старше 25 лет.
                GetStudentsOlderThan21(db);

                // 6) Получить средний возраст всех студентов.
                GetAverageStudentAge(db);

                // 7) Получить самого молодого студента.
                GetYoungestStudent(db);

                // 8) Получить количество курсов, на которых учится студент с определенным Id.
                GetStudentCoursesDetails(db, 3);

                // 9) Получить список имен всех студентов.
                GetAllStudents(db);

                // 10) Сгруппировать студентов по возрасту.
                GetStudentsGroupedByAge(db);

                // 11) Получить список студентов, отсортированных по фамилии в алфавитном порядке.
                GetStudentsSortedByFamilyName(db);

                // 12) Получить список студентов вместе с информацией о зачислениях на курсы.
                GetAllStudentsWithEnrollments(db);

                // 13) Получить список студентов, не зачисленных на определенный курс.
                GetAllCoursesWithNonEnrolledStudents(db, 1);

                // 14) Получить список студентов, зачисленных одновременно на два определенных курса.
                GetStudentsEnrolledInTwoCourses(db, 1, 10);

                // 15) Получить количество студентов на каждом курсе.
                GetAllCoursesWithEnrollments(db);
            }
        }
        // 15) Получить количество студентов на каждом курсе.
        public static void GetAllCoursesWithEnrollments(ApplicationContext db)
        {
            var coursesWithEnrollments = db.Courses
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Students = c.Enrollments.Select(e => e.Student.Name + " " + e.Student.FamilyName).ToList()
                })
                .ToList();

            Console.WriteLine("\n----------------------------------\n");

            foreach (var course in coursesWithEnrollments)
            {                
                Console.WriteLine($"\nCourse: {course.Name} - {course.Description}");
                Console.WriteLine($"Total Students Enrolled: {course.Students.Count}");

                if (course.Students.Any())
                {
                    foreach (var student in course.Students)
                    {
                        Console.WriteLine($"  - {student}");
                    }
                }
                else
                {
                    Console.WriteLine("  No students enrolled.");
                }
            }
        }
        // 14) Получить список студентов, зачисленных одновременно на два определенных курса.
        public static void GetStudentsEnrolledInTwoCourses(ApplicationContext db, int courseId1, int courseId2)
        {
            var courses = db.Courses
                .Where(c => c.Id == courseId1 || c.Id == courseId2)
                .Select(c => new { c.Id, c.Name, c.Description })
                .ToList();

            if (courses.Count < 2)
            {
                Console.WriteLine("One or both courses not found.");
                return;
            }

            var studentsEnrolledInBoth = db.Students
                .Where(s => s.Enrollments.Any(e => e.CourseId == courseId1) &&
                            s.Enrollments.Any(e => e.CourseId == courseId2))
                .Select(s => s.Name + " " + s.FamilyName)
                .ToList();
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine("\nCourses:");
            foreach (var course in courses)
            {
                Console.WriteLine($"  - {course.Name} ({course.Description})");
            }

            Console.WriteLine("\nStudents enrolled in both courses:");
            if (studentsEnrolledInBoth.Any())
            {
                foreach (var student in studentsEnrolledInBoth)
                {
                    Console.WriteLine($"  - {student}");
                }
            }
            else
            {
                Console.WriteLine("  No students are enrolled in both courses.");
            }
        }
        // 13) Получить список студентов, не зачисленных на определенный курс.
        public static void GetAllCoursesWithNonEnrolledStudents(ApplicationContext db, int courseId)
        {
            var course = db.Courses
        .Where(c => c.Id == courseId)
        .Select(c => new
        {
            c.Id,
            c.Name,
            c.Description,
            NonEnrolledStudents = db.Students
                .Where(s => !s.Enrollments.Any(e => e.CourseId == courseId))
                .Select(s => s.Name + " " + s.FamilyName)
                .ToList()
        })
        .FirstOrDefault(); // Get only one course or null if not found

            if (course == null)
            {
                Console.WriteLine($"Course with ID {courseId} not found.");
                return;
            }
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine($"\nCourse ID: {course.Id}, Name: {course.Name}, Description: {course.Description}");

            if (course.NonEnrolledStudents.Any())
            {
                Console.WriteLine("  Students NOT enrolled in this course:");
                foreach (var student in course.NonEnrolledStudents)
                {
                    Console.WriteLine($"  - {student}");
                }
            }
            else
            {
                Console.WriteLine("  All students are enrolled in this course.");
            }
        }
        // 12) Получить список студентов вместе с информацией о зачислениях на курсы.
        public static void GetAllStudentsWithEnrollments(ApplicationContext db)
        {
            var students = db.Students
                .Select(s => new
                {
                    s.Id,
                    FullName = s.Name + " " + s.FamilyName,
                    Enrollments = s.Enrollments.Select(e => new
                    {
                        e.Course.Name,
                        e.EnrollmentDate
                    }).ToList()
                })
                .ToList(); // Forces execution in SQL Server

            if (students.Any())
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("Students and their enrollments:");
                foreach (var student in students)
                {
                    Console.WriteLine($"\nID: {student.Id}, Name: {student.FullName}");
                    if (student.Enrollments.Any())
                    {
                        Console.WriteLine("  Enrollments:");
                        foreach (var enrollment in student.Enrollments)
                        {
                            Console.WriteLine($"  - {enrollment.Name}: {enrollment.EnrollmentDate:yyyy-MM-dd}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No enrollments.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No students found in the database.");
            }
        }
        // 11) Получить список студентов, отсортированных по фамилии в алфавитном порядке.
        public static void GetStudentsSortedByFamilyName(ApplicationContext db)
        {
            var students = db.Students
                .OrderBy(s => s.FamilyName)
                .ThenBy(s => s.Name) // Secondary sorting by Name if FamilyNames are the same
                .Select(s => new
                {
                    s.FamilyName,
                    s.Name,
                    s.Id
                })
                .ToList(); // Executes query in database

            if (students.Any())
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("Students sorted by Family Name:");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FamilyName}, {student.Name} (ID: {student.Id})");
                }
            }
            else
            {
                Console.WriteLine("No students found in the database.");
            }
        }
        // 10) Сгруппировать студентов по возрасту.
        public static void GetStudentsGroupedByAge(ApplicationContext db)
        {
            var studentsGroupedByAge = db.Students
        .AsEnumerable() // Ensures in-memory evaluation
        .Select(s => new
        {
            FullName = s.Name + " " + s.FamilyName,
            Age = DateTime.Today.Year - s.BirthDay.Year -
                  (DateTime.Today < s.BirthDay.AddYears(DateTime.Today.Year - s.BirthDay.Year) ? 1 : 0)
        })
        .GroupBy(s => s.Age)
        .OrderBy(g => g.Key)
        .ToList();

            if (studentsGroupedByAge.Any())
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("Students Grouped by Age:");
                foreach (var group in studentsGroupedByAge)
                {
                    Console.WriteLine($"\nAge: {group.Key}");
                    foreach (var student in group)
                    {
                        Console.WriteLine($"- {student.FullName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No students found in the database.");
            }
        }
        // 9) Получить список имен всех студентов.
        public static void GetAllStudents(ApplicationContext db)
        {
            var students = db.Students
                .Select(s => new
                {
                    s.Id,
                    FullName = s.Name + " " + s.FamilyName
                })
                .ToList(); // Executes the query and loads the data into memory

            if (students.Any())
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine("List of Students:");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.FullName}");
                }
            }
            else
            {
                Console.WriteLine("No students found in the database.");
            }
        }
        // 8) Получить количество курсов, на которых учится студент с определенным Id.
        public static void GetStudentCoursesDetails(ApplicationContext db, int studentId)
        {
            var studentDetails = db.Students
                .Where(s => s.Id == studentId)
                .Select(s => new
                {
                    FullName = s.Name + " " + s.FamilyName,
                    TotalCourses = s.Enrollments.Count(),
                    Courses = s.Enrollments.Select(e => new
                    {
                        CourseName = e.Course.Name,
                        CourseDescription = e.Course.Description,
                        EnrollmentDate = e.EnrollmentDate
                    }).ToList()
                })
                .FirstOrDefault(); // Get student or null if not found

            if (studentDetails != null)
            {
                Console.WriteLine("\n----------------------------------\n");
                Console.WriteLine($"Student: {studentDetails.FullName}");
                Console.WriteLine($"Total Number of Courses Enrolled: {studentDetails.TotalCourses}");
                Console.WriteLine("Courses enrolled:");

                foreach (var course in studentDetails.Courses)
                {
                    Console.WriteLine($"- {course.CourseName}: {course.CourseDescription}, Enrolled on: {course.EnrollmentDate.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine($"Student with ID {studentId} not found.");
            }
        }
        // 7) Получить самого молодого студента.
        public static void GetYoungestStudent(ApplicationContext db)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Find the youngest student
            var youngestStudent = db.Students
                                    .OrderByDescending(s => s.BirthDay) // Sort by BirthDay (youngest first)
                                    .Select(s => new
                                    {
                                        FullName = s.Name + " " + s.FamilyName,
                                        BirthDate = s.BirthDay,
                                        Age = EF.Functions.DateDiffYear(s.BirthDay, today) // Calculate age
                                    })
                                    .FirstOrDefault(); // Get the first (youngest) student

            // Print result
            Console.WriteLine("\n----------------------------------\n");
            if (youngestStudent != null)
            {
                Console.WriteLine("The youngest student:");
                Console.WriteLine($"- {youngestStudent.FullName}, Age: {youngestStudent.Age}, Birthdate: {youngestStudent.BirthDate.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("No students found in the database.");
            }
        }

        // 6) Получить средний возраст всех студентов.
        public static void GetAverageStudentAge(ApplicationContext db)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Calculate average age
            double? averageAge = db.Students
                                   .Select(s => EF.Functions.DateDiffYear(s.BirthDay, today)) // Calculate age using SQL
                                   .Average();

            // Print result
            Console.WriteLine("\n----------------------------------\n");
            if (averageAge.HasValue)
            {
                Console.WriteLine($"The average age of all students is: {Math.Round(averageAge.Value, 2)} years");
            }
            else
            {
                Console.WriteLine("No students found in the database.");
            }
        }
        // 5) Получить список студентов, старше 25 лет.
        public static void GetStudentsOlderThan21(ApplicationContext db)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Query students older than 21
            var students = db.Students
                             .Where(s => EF.Functions.DateDiffYear(s.BirthDay, today) > 21) // Calculate age using DateDiffYear
                             .Select(s => new
                             {
                                 FullName = s.Name + " " + s.FamilyName,
                                 Age = EF.Functions.DateDiffYear(s.BirthDay, today) // Calculate age
                             })
                             .ToList();

            // Print results
            Console.WriteLine("\n----------------------------------\n");
            if (students.Any())
            {
                Console.WriteLine("Students older than 21 years:");
                foreach (var student in students)
                {
                    Console.WriteLine($"- {student.FullName}, Age: {student.Age}");
                }
            }
            else
            {
                Console.WriteLine("No students found who are older than 21.");
            }
        }

        // 4) Получить список курсов, на которые зачислено более 5 студентов.
        public static void GetCoursesWithMoreThanThreeStudents(ApplicationContext db)
        {
            // Query courses with more than 3 students enrolled
            var coursesWithStudents = db.Courses
                                        .Where(c => c.Enrollments.Count > 3) // Filter courses with more than 3 students
                                        .Select(c => new
                                        {
                                            CourseName = c.Name,
                                            CourseDescription = c.Description,
                                            Enrollments = c.Enrollments.Select(e => new
                                            {
                                                StudentName = e.Student.Name,
                                                StudentFamilyName = e.Student.FamilyName,
                                                EnrollmentDate = e.EnrollmentDate
                                            }).ToList()
                                        })
                                        .ToList();

            // Print results
            Console.WriteLine("\n----------------------------------\n");
            if (coursesWithStudents.Any())
            {
                Console.WriteLine("Courses with more than 3 students enrolled:");

                foreach (var course in coursesWithStudents)
                {
                    Console.WriteLine($"- {course.CourseName} ({course.CourseDescription})");

                    if (course.Enrollments.Any())
                    {
                        Console.WriteLine("  Enrolled Students:");
                        foreach (var enrollment in course.Enrollments)
                        {
                            Console.WriteLine($"    - {enrollment.StudentName} {enrollment.StudentFamilyName}, Enrolled on: {enrollment.EnrollmentDate.ToShortDateString()}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No courses found with more than 3 students enrolled.");
            }
        }

        // 3) Получить список курсов, на которых учит определенный преподаватель, вместе с именами студентов, зачисленных на каждый курс.
        public static void GetAllCoursesWithStudentsByInstructor(int instructorId, ApplicationContext db)
        {
            // Fetch instructor details
            var instructor = db.Instructors.FirstOrDefault(i => i.Id == instructorId);

            if (instructor == null)
            {
                Console.WriteLine($"Instructor with ID {instructorId} not found.");
                return;
            }

            // Fetch courses taught by this instructor, including enrolled students
            var coursesWithStudents = db.Courses
                                        .Where(c => c.InstructorId == instructorId)
                                        .Select(c => new
                                        {
                                            CourseName = c.Name,
                                            CourseDescription = c.Description,
                                            Enrollments = c.Enrollments.Select(e => new
                                            {
                                                StudentName = e.Student.Name,
                                                StudentFamilyName = e.Student.FamilyName,
                                                EnrollmentDate = e.EnrollmentDate
                                            }).ToList()
                                        })
                                        .ToList();

            // Print instructor's name
            Console.WriteLine("\n----------------------------------\n");
            Console.WriteLine($"Instructor: {instructor.Name} {instructor.FamilyName}");

            // Print courses and enrolled students in a hierarchical format
            if (coursesWithStudents.Any())
            {
                Console.WriteLine("Courses taught:");
                foreach (var course in coursesWithStudents)
                {
                    Console.WriteLine($"- {course.CourseName} ({course.CourseDescription})");

                    if (course.Enrollments.Any())
                    {
                        Console.WriteLine("  Enrolled Students:");
                        foreach (var enrollment in course.Enrollments)
                        {
                            Console.WriteLine($"    - {enrollment.StudentName} {enrollment.StudentFamilyName}, Enrolled on: {enrollment.EnrollmentDate.ToShortDateString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No students enrolled in this course.");
                    }
                }
            }
            else
            {
                Console.WriteLine("This instructor does not teach any courses.");
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
