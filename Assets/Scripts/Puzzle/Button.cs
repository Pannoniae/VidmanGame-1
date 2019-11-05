using UnityEngine;

public class Button : PuzzleElement {

    public Sprite off;
    public Sprite on;
    public override void activate() {
        activated = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = on;
    }

    public override void deactivate() {
        activated = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = off;
    }
}