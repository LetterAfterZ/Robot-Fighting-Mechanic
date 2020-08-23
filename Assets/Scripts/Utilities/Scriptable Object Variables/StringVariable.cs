using System;
using UnityEngine;

[CreateAssetMenu]
public class StringVariable : ScriptableObject
{
    // based on this talk: https://www.youtube.com/watch?v=raQ3iHhE_Kk
    public string Value;

    // for internal notes only
    [SerializeField][TextArea(3,10)] private string DeveloperDescription;
}

[Serializable]
public class StringReference
{
    public bool UseConstant = true;
    public string ConstantValue;
    public StringVariable Variable;

    public string Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}