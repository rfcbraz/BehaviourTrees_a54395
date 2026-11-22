using UnityEngine;

public class RobberBehaviour : MonoBehaviour
{
   BehaviourTree tree;

    void Start()
    {
        tree = new BehaviourTree();
        Node steal = new Node("Steal Nothing");
        Node goToDiamond = new Node("Go To Diamond");
        Node goToVan = new Node("Go To Van");

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        tree.PrintTree();
    }
}
