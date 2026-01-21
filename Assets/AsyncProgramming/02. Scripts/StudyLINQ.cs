using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Person
{
    public string name;
    public int score;

    public Person(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

[Serializable]
public class Student
{
    public int studentID;
    public string studentName;

    public Student(int studentID, string studentName)
    {
        this.studentID = studentID;
        this.studentName = studentName;
    }
}

[Serializable]
public class Grade
{
    public int studentID;
    public string subject;
    public int score;

    public Grade(int studentID, string subject, int score)
    {
        this.studentID = studentID;
        this.subject = subject;
        this.score = score;
    }
}

public class StudyLINQ : MonoBehaviour
{
    # region 기초
    // public int[] numbers = { 1, 2, 3, 4, 5 };
    //
    // private void Start()
    // {
    //     var result = from number in numbers 
    //         where number > 3 
    //         //orderby number // 오름차순
    //         //orderby number descending //내림차순
    //         select number;
    //
    //     // var result = numbers.Where(n => n > 3).Select(n => n * n);
    //     
    //     foreach (var n in result)
    //         Debug.Log(n);
    // }
    #endregion
    
    #region 평균 검사
    // public List<Person> persons = new List<Person>();
    //
    // public int cutline = 70;
    //
    // private void Start()
    // {
    //     persons.Add(new Person("John", 65));
    //     persons.Add(new Person("Sarah", 80));
    //     persons.Add(new Person("David", 95));
    //     persons.Add(new Person("Emily", 70));
    //     persons.Add(new Person("Michael", 50));
    //
    //     CheckScore();
    // }
    //
    // private void CheckScore()
    // {
    //     // var passPersons = from person in persons 
    //     //     where person.score >= cutline
    //     //     select person;
    //
    //     var passPersons = persons.Where(p => p.score >= cutline);
    //
    //     var failPersons = persons.Except(passPersons);
    //     
    //     foreach (var person in passPersons)
    //     {
    //         Debug.Log($"<color=green>{person.name}</color>");
    //     }
    //     
    //     foreach (var person in failPersons)
    //     {
    //         Debug.Log($"<color=red>{person.name}</color>");
    //     }
    //
    //     var result = passPersons.Average(p => p.score);
    // }
    #endregion

    #region 아이템 가격 비교
    // [Serializable]
    // public class Item
    // {
    //     public string name;
    //     public int price;
    //
    //     public Item(string name, int price)
    //     {
    //         this.name = name;
    //         this.price = price;
    //     }
    // }
    //
    // public List<Item> items = new List<Item>();
    //
    // private void Start()
    // {
    //     items.Add(new Item("초보자 검", 100));
    //     items.Add(new Item("초보자 방패", 200));
    //     items.Add(new Item("초보자 단검", 50));
    //     items.Add(new Item("초보자 투구", 150));
    //     items.Add(new Item("초보자 갑옷", 300));
    //
    //     var average = items.Average(item => item.price);
    //     Debug.Log($"아이템 평균 가격 : {average}");
    //     
    //     var sum = items.Sum(item => item.price);
    //     Debug.Log($"아이템 종합 가격 : {sum}");
    //
    //     var min = items.Min(item => item.price);
    //     var max = items.Max(item => item.price);
    //     Debug.Log($"가장 비싼 아이템의 가격은 {max}");
    //     Debug.Log($"가장 싼 아이템의 가격은 {min}");
    //
    //     // 방어구 중 가장 낮은 가격
    //     var result = items.Where(item => item.price > 100).Min(item => item.price);
    //     
    //     // 평균값 이상인 아이템의 개수 : Count
    //     var count = items.Count(item => item.price > average);
    // }
    #endregion

    #region 값 관련
    // public int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    // public int[] numbers1 = { 1, 2, 3, 4, 5 };
    // public int[] numbers2 = { 3, 2, 1, 4, 5 };
    //
    // private void Start()
    // {
    //     var isExist = numbers.Any(n => n % 3 == 0);
    //
    //     var isAll = numbers.All(n => n > 0);
    //
    //     var isEqual = numbers1.SequenceEqual(numbers2);
    //
    //     var index = numbers.ToList().FindIndex(n => n > 2); // 2보단 큰 값을 찾는 기능 (가장 첫번째 인덱스)
    //
    //     var take = numbers.Where(n => n > 2).Take(3);
    //
    //     string str = "";
    //     foreach (var v in take)
    //         str += v + ", ";
    //
    //     var result = numbers.Distinct(); // 중복 제거
    //
    // }
    #endregion

    public List<Student> students = new List<Student>();
    public List<Grade> grades = new List<Grade>();
    
    private void Awake()
    {
        students.Add(new Student(1, "Alice"));
        students.Add(new Student(2, "Bob"));
        students.Add(new Student(3, "Charlie"));
        students.Add(new Student(4, "Eve"));
        
        grades.Add(new Grade(1, "Math", 90));
        grades.Add(new Grade(2, "Science", 85));
        grades.Add(new Grade(3, "English", 92));
        grades.Add(new Grade(4, "Math", 78 ));
    }

    private void Start()
    {
        // var innerJoin = from student in students
        //     join grade in grades on student.studentID equals grade.studentID
        //     select new
        //     {
        //         StudentID = student.studentID,
        //         StudentName = student.studentName,
        //         Subject = grade.subject,
        //         Score = grade.score
        //     };

        var outerJoin = from student in students
            join grade in grades on student.studentID equals grade.studentID into studentGrades
            from studentGrade in studentGrades.DefaultIfEmpty()
            select new
            {
                StudentID = student.studentID,
                StudentName = student.studentName,
                Subject = studentGrade?.subject, // subject가 null이면 null
                Score = studentGrade?.score ?? 0 // score가 null이면 0으로
            };
        
        foreach (var item in outerJoin)
        {
            Debug.Log($"{item.StudentID}, {item.StudentName}, {item.Subject}, {item.Score}");
        }
    }
}
