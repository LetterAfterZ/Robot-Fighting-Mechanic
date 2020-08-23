using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    // based on this talk: https://www.youtube.com/watch?v=raQ3iHhE_Kk
    public float Value;

    // for internal notes only
    [SerializeField][TextArea(3,10)] private string DeveloperDescription;
}

[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}