using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GoalPresenter : MonoBehaviour
{

    public int previewRange;

    public Goal currentGoal;

    [FormerlySerializedAs("panel1")] public Image panel0;
    [FormerlySerializedAs("panel2")] public Image panel1;
    [FormerlySerializedAs("panel3")] public Image panel2;
    
    
    void Start()
    {
        GoalManager.instance.OnGoalSolved += OnGoalSolved;

        // is initialzed, need to pick up first goal myself.
        if (GoalManager.instance.initialized)
        {
            OnGoalSolved(null);
        }
        
    }

    void OnGoalSolved(Goal g)
    {
        currentGoal = GoalManager.instance.GetFutureGoal(previewRange);

        panel0.color = currentGoal.GetColor(0);
        panel1.color = currentGoal.GetColor(1);
        panel2.color = currentGoal.GetColor(2);

    }
}
