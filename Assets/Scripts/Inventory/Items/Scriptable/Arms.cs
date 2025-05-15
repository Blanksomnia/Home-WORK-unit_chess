using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item/Arms", order = 1)]
public class Arms : EmptyItem
{
    public int damage;
    public float radius;
    public LayerMask unit;
    public LayerMask obstical;
    public string tag = "Player";
    public Characters characters;

    public Arms(Arms arms) : base(arms) 
    {
        this.damage = arms.damage;
        this.radius = arms.radius;
        this.unit = arms.unit;
        this.obstical = arms.obstical;
        this.tag = arms.tag;
        this.characters = arms.characters;
    }


    public override void Use(Character character)
    {
        Find(character);
    }

    private void Find(Character character)
    {
        Collider[] targets = Physics.OverlapSphere(character.transform.position, radius, unit);
        for (int i = 0;  i < targets.Length; i++)
            if(tag == null || tag == "")
                Attack(character, targets[i].transform);
            else if (targets[i].tag == tag)
                Attack(character, targets[i].transform);           
       
    }

    private void Attack(Character character, Transform target)
    {
        Vector3 direction = target.transform.position - character.transform.position;
        if (!Physics.Raycast(character.transform.position + new Vector3(0, 3f, 0), direction, radius / 2, obstical))
            characters.GetDamage(target, damage);
    }

}
