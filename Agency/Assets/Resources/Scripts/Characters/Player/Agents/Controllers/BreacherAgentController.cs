using UnityEngine;

public class BreacherAgentController : AAgentController
{
    private GameObject bulletPrefab;
    private GameObject specialPrefab;

    private float specialCooldownTimer = 0f;
    private float specialCooldown;

    private float timeBetweenShots = 0.1f;
    private float timeBetweenShotsTimer = 0.1f;

    public override Sprite GetSprite()
    {
        return ResourcePaths.BreacherSprite;
    }

    public override void Init(Agent agent)
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet1");
        specialPrefab = Resources.Load<GameObject>("Prefabs/FlashBang");

        specialCooldown = agent.SpecialCooldown;
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
                Bullet scr = b.GetComponent<Bullet>();
                scr.Direction = mousePos - go.transform.position;
                scr.Speed = 15f;
                scr.Creator = go;
                scr.Team = Team.Player;
                scr.LifeTime = 2f;
                scr.CheckPath();

                EventManager.Instance.TriggerEvent("ZoomSlap", new EventParam());
                SoundManager.Instance.DoPlayOneShot(new[] { SoundFile.PistolShot0 }, go.transform.position);

                EventManager.Instance.TriggerEvent("PlayerShoot", new EventParam());
            }
        }
    }

    public override bool ProcessSpecial(GameObject go, Vector3 mousePos)
    {
        base.ProcessSpecial(go, mousePos);

        GameObject b = Object.Instantiate(specialPrefab, go.transform.position, Quaternion.identity);

        return true;
    }
}