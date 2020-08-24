using UnityEngine;

public class EvalRepairs : MonoBehaviour
{
    public BoxContainer[] boxes;
    [SerializeField] RobotHealth _robotHealth;
    [SerializeField] RobotLaser _robotLaser;    

    public void EvaluateRepairs()
    {
        //Remove matching parts
        foreach (BoxContainer box in boxes)
        {
            //for each required part
            if(box.boxRequiredParts.Count <= 0)
                continue;

            for (int i=box.boxRequiredParts.Count -1; i > 0; i--)
            {
                //check each added part
                if(box.boxAddedParts.Count <= 0)
                    continue;

                for (int j=box.boxAddedParts.Count -1; j > 0; j--)
                {
                    //if they match remove them both & increment laser progress
                    if (box.boxRequiredParts[i] == box.boxAddedParts[j])
                    {
                        box.boxRequiredParts.RemoveAt(i);
                        box.boxAddedParts.RemoveAt(j);
                        //increment laser
                        _robotLaser.ChargeLaser(5);
                    }
                }
            }
        }

        // Apply damage for missed or extra parts
        foreach (BoxContainer box in boxes)
        {
            //damage for each missed part
            foreach (Item item in box.boxRequiredParts)
            {
                //take damage
                _robotHealth.TakeDamage(5);
            }

            //damage for each missed part
            foreach (Item item in box.boxAddedParts)
            {
                //take damage
                _robotHealth.TakeDamage(5);
            }
        }

    }
}
