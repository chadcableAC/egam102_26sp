using System.Collections.Generic;
using UnityEngine;

public class ClassRandomizer : MonoBehaviour
{
    [System.Serializable]
    public class Student
    {
        public string name;
        public bool isPresent;
        public int preferredOrder = -1;
    }
    
    public List<Student> studentList;

    public bool isPlaytest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Randomize();    
    }

    public void Randomize()
    {
        // Build a duplicate list
        List<Student> workingList = new();
        List<Student> randomizedList = new();
        
        // Add present students to the working list
        foreach (Student student in studentList)
        {
            if (student.isPresent)
            {
                workingList.Add(student);
            }
        }

        // Any students with a preferred order?
        List<Student> preferredList = new();
        for (int i = workingList.Count - 1; i >= 0; i--)
        {
            Student student = workingList[i];
            if (student.preferredOrder > 0)
            {
                preferredList.Add(student);
                workingList.RemoveAt(i);
            }
        }

        while (preferredList.Count > 0)
        {
            // Add the lowest score first
            int bestIndex = -1;
            int bestOrder = -1;
            for (int i = 0; i < preferredList.Count; i++)
            {
                Student thisStudent = preferredList[i];
                if (thisStudent.preferredOrder < bestOrder || bestOrder == -1)
                {
                    bestIndex = i;
                    bestOrder = thisStudent.preferredOrder;
                }
            }

            Student student = preferredList[bestIndex];
            randomizedList.Add(student);
            preferredList.RemoveAt(bestIndex);
        }

        // Randomly add these students to the randomized list
        while (workingList.Count > 0)
        {
            // Pick a random index
            int randomIndex = Random.Range(0, workingList.Count);

            // Put into the randomized list, remove from the working (So that we don't add it twice)
            Student student = workingList[randomIndex];
            randomizedList.Add(student);
            workingList.RemoveAt(randomIndex);
        }

        // Finally display the results
        string outputString = string.Empty;
        for (int i = 0; i < randomizedList.Count; i++)
        {
            // Current student
            Student currentStudent = randomizedList[i];
            outputString += $"{i + 1}.\t";
            outputString += $"{currentStudent.name}\n";

            // Playtesting student (the next one in line)
            if (isPlaytest)
            {
                int nextIndex = (i + 1) % randomizedList.Count;
                Student nextStudent = randomizedList[nextIndex];
                outputString += $"\t> Playtester: {nextStudent.name}\n";
            }

            if (i < randomizedList.Count - 1)
            {
                outputString += "\n";
            }
        }

        Debug.Log(outputString);
    }
}
