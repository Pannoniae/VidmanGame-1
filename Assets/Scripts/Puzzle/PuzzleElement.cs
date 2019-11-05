using System.Collections.Generic;

public interface PuzzleElement {
    void activate();
    void deactivate();
    List<PuzzleElement> connectedElements { get; set; } // which elements are activated if this element is activated
}