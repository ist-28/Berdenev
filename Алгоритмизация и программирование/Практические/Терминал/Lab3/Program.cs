using System;
using System.Data.SQLite;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string BDFile = "Data Source=/Students.db;Version=3;";
            
            using (SQLiteConnection conn = new SQLiteConnection(BDFile)) {
                
                conn.Open();

                string createTable = @"CREATE TABLE IF NOT EXISTS students (
                                surname TEXT,
                                math REAL,
                                physics REAL,
                                inform REAL
                            );";
                using (SQLiteCommand cmd = new SQLiteCommand(createTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                PrintRequest(conn, "SELECT COUNT(*) FROM students WHERE math >= 2 AND physics >= 2 AND inform >= 2;", "Студенты, сдавшие сессию");
                PrintRequest(conn, "SELECT COUNT(*) FROM students WHERE math >= 4 AND physics >= 4 AND inform >= 4;", "Студенты с оценками 4 и 5");
                PrintRequest(conn, "SELECT COUNT(*), surname FROM students WHERE math = 5 AND physics = 5 AND inform = 5;", "Студенты с оценками 5");
                PrintRequest(conn, "SELECT AVG(math) AS avg_math, AVG(physics) AS avg_physics, AVG(inform) AS avg_inform FROM students;", "Средние баллы");
                PrintRequest(conn, "SELECT AVG((math + physics + inform) / 3.0) AS avg_group_score FROM students;", "Средний балл группы");
                PrintRequest(conn, "SELECT surname FROM students WHERE (math + physics + inform) /" +
                    " 3.0 > (SELECT AVG((math + physics + inform) / 3.0) FROM students);", "Студенты выше среднего");

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT surname, (math + physics + inform) /" +
                    " 3.0 AS ranking FROM students ORDER BY ranking DESC;", conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nРейтинг Студентов:");
                        while (reader.Read()) {
                            Console.WriteLine($"{reader["surname"]}: {reader["ranking"]}");
                        }
                    }
                }


                    conn.Close();


            }
        }
        static void PrintRequest(SQLiteConnection conn, string query, string description)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader()) { 
                    Console.WriteLine($"\n{description}:");
                    while (reader.Read()) {
                        for (int i = 0; i < reader.FieldCount; i++) { 
                            Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
