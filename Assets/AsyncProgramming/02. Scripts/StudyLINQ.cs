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

public class StudyLINQ : MonoBehaviour
{
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

    public List<Person> persons = new List<Person>();

    public int cutline = 70;

    private void Start()
    {
        persons.Add(new Person("John", 65));
        persons.Add(new Person("Sarah", 80));
        persons.Add(new Person("David", 95));
        persons.Add(new Person("Emily", 70));
        persons.Add(new Person("Michael", 50));

        CheckScore();
    }

    private void CheckScore()
    {
        // var passPersons = from person in persons 
        //     where person.score >= cutline
        //     select person;

        var passPersons = persons.Where(p => p.score >= cutline);

        var failPersons = persons.Except(passPersons);
        
        foreach (var person in passPersons)
        {
            Debug.Log($"<color=green>{person.name}</color>");
        }
        
        foreach (var person in failPersons)
        {
            Debug.Log($"<color=red>{person.name}</color>");
        }
    }
}
