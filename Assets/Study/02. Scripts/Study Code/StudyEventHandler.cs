using System;
using UnityEngine;

public class DataClass : EventArgs
{
    public string dataName;
    public int dataNumber;
    public bool isData;

    public DataClass(string dataName, int dataNumber, bool isData)
    {
        this.dataName = dataName;
        this.dataNumber = dataNumber;
        this.isData = isData;
    }
}

public class StudyEventHandler : MonoBehaviour
{
    public event EventHandler handler;

    private void Start()
    {
        handler += StartEvent;

        DataClass data1 = new DataClass("DataClass1", 1, false);
        DataClass data2 = new DataClass("DataClass2", 2, true);
        
        handler?.Invoke(this, data1);
        handler?.Invoke(this, data2);
    }

    private void StartEvent(object sender, EventArgs e)
    {
        
    }
}
