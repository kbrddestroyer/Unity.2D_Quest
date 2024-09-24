using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MergeSchema : ScriptableObject
{
    [System.Serializable]
    public struct MergeStruct
    {
        public Mergable f;
        public Mergable s;
        public Mergable output;
    }

    [SerializeField] private MergeStruct[] merges;

    public Mergable Validate(Mergable f, Mergable s)
    {
        foreach (MergeStruct merge in merges)
        {
            if (
                (f.ID == merge.f.ID && s.ID == merge.s.ID) ||
                (f.ID == merge.s.ID && s.ID == merge.f.ID)
                )
                return merge.output;
        }
        return null;
    }

    public MergeStruct[] Merges { get => merges; }
}
