public class Actor {

    public int id;
    public string name;
    public string title;
    public string weapon;
    public float strength;
    public int level;

    public string Talk()
    {
        return "대화를 걸었습니다.";
    }

    public string Hasweapon()
    {
        return weapon;
    }

    public void LevelUp()
    {
        level = level + 1;
    }


}
