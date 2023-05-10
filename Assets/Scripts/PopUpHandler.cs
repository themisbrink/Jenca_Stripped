using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;

public class PopUpHandler : MonoBehaviour
{
    public static PopUpHandler Singleton { get; private set; }
    public UIPopup popup;

    public TextMeshProUGUI gradeLabel;
    public TextMeshProUGUI domainLabel;
    public TextMeshProUGUI clusterLabel;
    public TextMeshProUGUI standIdLabel;
    public TextMeshProUGUI descrLabel;

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
            Destroy(Singleton);
        else
            Singleton = this;
    }

    public void ShowInfo(Block block)
    {
        gradeLabel.text = "<color=#E9B524>Grade:<color=#fff> "+ block.grade;
        domainLabel.text = "<color=#E9B524>Domain:<color=#fff> "+block.domain;
        clusterLabel.text = "<color=#E9B524>Cluster:<color=#fff> "+block.cluster;
        standIdLabel.text = "<color=#E9B524>Standard ID:<color=#fff> "+block.standardid;
        descrLabel.text = "<color=#E9B524>Description:<color=#fff> "+block.standarddescription;

        popup.Show();
    }
}
