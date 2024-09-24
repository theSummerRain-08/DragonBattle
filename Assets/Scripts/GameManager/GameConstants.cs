
public static class GameConstants {
    public static string[] playerName = new string[5] {"GOKUZ",  "VEGETAZ", "TRUNKZ",  "GOHANZ",  "CADICZ" };

    public static int[] goldToUnlockLevel 
        = new int[21] { 0, 0, 2000, 10000, 15000, 30000, 50000, 100000, 200000, 300000, 500000,
       750000, 1000000, 1200000, 1500000,  1800000, 2000000, 2200000,  2500000, 2800000, 3000000};

    public static int[] powerLevel
        = new int[21] { 1000, 4000, 16000, 48000, 160000, 900000, 2000000,7000000, 10000000, 30000000, 40000000, 50000000,
        70000000, 100000000, 200000000, 400000000, 500000000, 700000000, 800000000, 900000000, 1000000000};

    
    public static int[] ManaToCastSkill = new int[6] { 17, 50, 10, 50, 50, 0 };


    public static float[] dmgScale = new float[5] { 0.5f, 8f, 1f, 16f, 8f };



    public static float[] TimeToCastSkill = new float[6] { 0.1f, 3.5f, 0.1f, 0.2f, 0.1f, 1.5f };

    public static int[] levelCharacter = new int[21] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
    13, 14, 15, 16, 17, 18, 19, 20, 21};

    public static int[] enemyLevel = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public static float[] enemyDelayTime = new float[10] { 5, 4.5f, 4, 3.5f, 3, 2.5f, 2.5f, 2f, 1.5f, 1 };


    public static float TimeToChange = 0.2f;

    public static float TimeActiveShield = 10f;

    public static string[] playerNameForTransform = new string[5] { "GOKUZ", "VEGETAZ", "TRUNKZ", "VEGITOZ", "GOHANZ" };
}
