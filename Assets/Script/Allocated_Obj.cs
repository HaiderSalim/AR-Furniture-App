using UnityEngine;

public class Allocated_Obj : MonoBehaviour
{
    public int AllocatedobjIndex;

    [SerializeField]
    private ObjectSelection ObjectSelection;

    public void giveindex()
    {
        ObjectSelection.index = AllocatedobjIndex;
    }
};
