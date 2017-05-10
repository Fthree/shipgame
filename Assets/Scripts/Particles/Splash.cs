using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {
    public bool isAlive()
    {
        ParticleSystem particleSys = GetComponent<ParticleSystem>();
        return particleSys.IsAlive(true);
    }
}
