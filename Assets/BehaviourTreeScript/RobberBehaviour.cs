using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
   BehaviourTree tree;

   public GameObject diamond;
   public GameObject van;
    public GameObject backdoor;
    public GameObject frontdoor;

   NavMeshAgent agent;

   public enum ActionState {IDLE, WORKING};
    ActionState state = ActionState.IDLE; 

    Node.Status treeStatus = Node.Status.RUNNING;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal Nothing");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToBackDoor = new Leaf("Go To Backdoor", GoToBackDoor);
        Leaf goToFrontDoor = new Leaf("Go To Frontdoor", GoToFrontDoor);
        Leaf goToVan = new Leaf("Go To Van", GoToVan);
        Selector opendoor = new Selector("Open Door");

        opendoor.AddChild(goToBackDoor);
        opendoor.AddChild(goToFrontDoor);

        steal.AddChild(opendoor);
        steal.AddChild(goToDiamond);
        //steal.AddChild(goToBackDoor);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        tree.PrintTree();


    }

    public Node.Status GoToDiamond()
    {
        return GoToLocation(diamond.transform.position);

    }

     public Node.Status GoToBackDoor()
    {
        return GoToLocation(backdoor.transform.position);

    }

     public Node.Status GoToFrontDoor()
    {
        return GoToLocation(frontdoor.transform.position);

    }
    
        public Node.Status GoToVan()
    {
        return GoToLocation(van.transform.position);


    }

    Node.Status GoToLocation ( Vector3 destination)
    {
        float distanceToTarge = Vector3.Distance(destination, this.transform.position);
        if(state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination)>= 2)
        {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        }
        else if (distanceToTarge < 2)
        {
            state =ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    void Update()
    {
        tree.Process();

        if (treeStatus == Node.Status.RUNNING)
        treeStatus = tree.Process();
    }

}
