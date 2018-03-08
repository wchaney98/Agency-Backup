using UnityEngine;

public class StandardAgentController : AAgentController
{
    private GameObject bulletPrefab;
    private GameObject specialPrefab;

    private float specialCooldownTimer = 0f;
    private float specialCooldown;

    private Agent agent;

    public override void Init(Agent agent)
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet1");
        // TODO change to grenade
        specialPrefab = Resources.Load<GameObject>("Prefabs/Laser1");

        this.agent = agent;
    }

    public override void ProcessPrimary(GameObject go, Vector3 mousePos)
    {
        GameObject b = Object.Instantiate(bulletPrefab, go.transform.position, Quaternion.identity);
        Bullet scr = b.GetComponent<Bullet>();
        scr.Direction = mousePos - go.transform.position;
        scr.Speed = 35f;
        scr.Creator = go;
        scr.Team = Team.Player;
        scr.LifeTime = 2f;
        scr.CheckPath();

        EventManager.Instance.TriggerEvent("ZoomSlap", new EventParam());
        SoundManager.Instance.DoPlayOneShot(new[] {SoundFile.PistolShot0}, go.transform.position);

        EventManager.Instance.TriggerEvent("PlayerShoot", new EventParam());
    }

    public override void ProcessSpecial(GameObject go, Vector3 mousePos)
    {
        GameObject b = Object.Instantiate(specialPrefab, go.transform.position, Quaternion.identity);
        Bullet scr = b.GetComponent<Bullet>();
        scr.Direction = mousePos - go.transform.position;
        scr.Speed = 10f;
        scr.Creator = go;
        scr.Team = Team.Player;
        scr.LifeTime = 15f;
    }
}