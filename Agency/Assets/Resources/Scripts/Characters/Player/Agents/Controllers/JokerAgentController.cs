using UnityEngine;

public class JokerAgentController : AAgentController
{
    private GameObject bulletPrefab;
    private GameObject specialPrefab;

    private float specialCooldownTimer = 0f;
    private float specialCooldown;

    private Agent agent;

    private float timeBetweenShots = 0.5f;
    private float timeBetweenShotsTimer = 0.5f;

    public override Sprite GetSprite()
    {
        return ResourcePaths.JokerSprite;
    }

    public override void Init(Agent agent)
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Grenade");
        specialPrefab = Resources.Load<GameObject>("Prefabs/ShotgunShell");

        this.agent = agent;
        specialCooldown = agent.SpecialCooldown;
        timeBetweenShots = agent.PrimaryCooldown;
    }

    public override void ProcessPrimary(GameObject go, Vector3 mousePos, float delta, bool inCover)
    {
        base.ProcessPrimary(go, mousePos, delta, inCover);

        timeBetweenShotsTimer += delta;

        if (!inCover && Input.GetMouseButton(0))
        {
            if (timeBetweenShotsTimer >= timeBetweenShots)
            {
                timeBetweenShotsTimer = 0f;

                GameObject b = Object.Instantiate(bulletPrefab, go.transform.position, Quaternion.identity);
                Bullet scr = b.GetComponent<Grenade>();
                scr.Direction = mousePos - go.transform.position;
                scr.Speed = 7f;
                scr.Creator = go;
                scr.Team = Team.Player;
                scr.LifeTime = 15f;

                EventManager.Instance.TriggerEvent("ZoomSlap", new EventParam());
                SoundManager.Instance.DoPlayOneShot(new[] { SoundFile.PistolShot0 }, go.transform.position);

                EventManager.Instance.TriggerEvent("PlayerShoot", new EventParam());
            }
        }
    }

    /// <summary>
    /// Returns true if the timer needs to be reset
    /// </summary>
    /// <param name="go"></param>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    public override bool ProcessSpecial(GameObject go, Vector3 mousePos)
    {
        base.ProcessSpecial(go, mousePos);


        for (int i = 0; i < agent.Level * 5; i++)
        {
            GameObject b = Object.Instantiate(specialPrefab, go.transform.position, Quaternion.identity);
            Bullet scr = b.GetComponent<Bullet>();
            Vector2 shotgunDir = mousePos - go.transform.position;
            shotgunDir += Random.insideUnitCircle * 2;
            scr.Direction = shotgunDir;
            scr.Speed = 7f;
            scr.Creator = go;
            scr.Team = Team.Player;
            scr.LifeTime = 0.5f;
        }
        
        return true;

    }
}