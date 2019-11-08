using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PuzzleElement : MonoBehaviour {
    public abstract void activate();
    public abstract void deactivate();

    public bool activated = startEnabled;
    public GM gm;
    public static bool startEnabled => false;

    public void Awake() {
        gm.registerElement(this);
    }

    public virtual void flip() {
        if (activated) {
            deactivate();
        }
        else {
            activate();
        }
    }

    /**
     * This method checks if all inputs are true.
     */
    public virtual bool checkInputs() {
        return connectedElements.All(element => element.activated);
    }

    List<PuzzleElement> connectedElements { get; set; } // which elements are connected to this element
}