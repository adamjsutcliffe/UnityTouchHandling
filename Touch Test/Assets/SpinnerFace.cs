using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpinnerFace : MonoBehaviour
{
    [SerializeField] private TextMeshPro numberText;

    public void UpdateNumber(int number)
    {
        numberText.text = $"{number}";
    }

    private void UpdateBeat()
    {

    }
}
