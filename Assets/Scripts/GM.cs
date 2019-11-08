using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public List<PuzzleElement> elements = new List<PuzzleElement>();

    public void registerElement(PuzzleElement element) {
        elements.Add(element);
    }
}