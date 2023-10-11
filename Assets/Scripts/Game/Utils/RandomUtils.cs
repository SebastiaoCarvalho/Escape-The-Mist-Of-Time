using UnityEngine;

public class RandomUtils {
    public static float RandomBinomial() {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }
}