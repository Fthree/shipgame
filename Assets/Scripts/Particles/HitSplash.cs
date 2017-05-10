using UnityEngine;
using System.Collections;

public class HitSplash : MonoBehaviour {

    public bool isAlive()
    {
        ParticleSystem particleSys = GetComponent<ParticleSystem>();
        return particleSys.IsAlive(true);
    }
}
