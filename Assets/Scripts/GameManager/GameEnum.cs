
public enum AttackType { 
    NormalAttack,
    Skill1,
    Skill2,
    Skill3,
    Skill4, 
}

public enum Item { 
    Buff,
    Coin
}
public enum Character {
    Player,
    Enemy
}

public enum CharacterToSelect {
    Goku,
    Vegeta,
    Trunk,
    Gohan
}
public enum Results {
    Win,
    Lose,
    Default
}
public enum Buff {
    Bean,
    Coin,
    Shield
}

public enum CharacterState {
    Idle,
    Attack,
    Attack3,
    Transform
}

public enum GameUI { 
    PlayScreen,
    HomeScreen,
    CharacterChoose,
    Transform,
    ReadyScreen,
    VsScreen,
    FreeScreen,
    CoinsScreen,
    VictoryScreen,
}

public static class GameEnum {
    public static CharacterState characterState;
}