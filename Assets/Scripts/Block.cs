using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;

    internal void UpdateData(Block block)
    {
        id = block.id;
        subject = block.subject;
        grade = block.grade;
        mastery = block.mastery;
        domainid = block.domainid;
        domain = block.domain;
        cluster = block.cluster;
        standardid = block.standardid;
        standarddescription = block.standarddescription;
    }

    private void OnMouseDown() {
    
        PopUpHandler.Singleton.ShowInfo(this);
    }

    private void OnMouseOver() {
        GameManager.Instance.FocusonStack(int.Parse(transform.parent.parent.name));
    }
}
