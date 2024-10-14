using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;
public class clientNetworkTransform : NetworkTransform
{


    protected override bool OnIsServerAuthoritative()
    {
        //owner of object is us not server!
        return false;
    }



}