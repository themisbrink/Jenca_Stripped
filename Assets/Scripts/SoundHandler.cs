using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlaySound(string sound){
        MasterAudio.PlaySound(sound);
    }
}
