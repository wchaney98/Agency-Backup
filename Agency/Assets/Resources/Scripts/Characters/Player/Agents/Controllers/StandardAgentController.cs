using UnityEngine;

public class StandardAgentController : AAgentController
{
    private GameObject bulletPrefab;
    private GameObject specialPrefab;

    private float specialCooldownTimer = 0f;
    private float specialCooldown;

    private Agent agent;

    private float timeBetweenShots = 0.5f;
    private float timeBetweenShotsTimer = 0.5f;

    private GameObject grenadeObject = null;

    public override Sprite GetSprite()
    {
        return ResourcePaths.StandardSprite;
    }

    public override void Init(Agent agent)
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet1");
        // TODO change to grenade
        specialPrefab = Resources.Load<GameObject>("Prefabs/Grenade");

        this.agent = agent;
    }

    public override void ProcessPrimary(GameObject go, Vector3 mousePos, float delta, bool inCover)
    {
        base.ProcessPrimary(go, mousePos, delta, inCover);

        timeBetweenShotsTimer += delta;

        Debug.Log(timeBetweenShotsTimer);

        if (!inCover && Input.GetMouseButton(0))
        {
            if (timeBetweenShotsTimer >= timeBetweenShots)
            {
                timeBetweenShotsTimer = 0f;

                GameObject b = Object.Instantiate(bulletPrefab, go.transform.position, Quaternion.identity);
                Bullet scr = b.GetComponent<Bullet>();
                scr.Direction = mousePos - go.transform.position;
                scr.Speed = 35f;
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

    /// <summary>
    /// Returns true if the timer needs to be reset
    /// </summary>
    /// <param name="go"></param>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    public override bool ProcessSpecial(GameObject go, Vector3 mousePos)
    {
        base.ProcessSpecial(go, mousePos);

        if (grenadeObject == null)
        {
            base.ProcessSpecial(go, mousePos);

            GameObject b = Object.Instantiate(specialPrefab, go.transform.position, Quaternion.identity);
            Bullet scr = b.GetComponent<Grenade>();
            scr.Direction = mousePos - go.transform.position;
            scr.Speed = 7f;
            scr.Creator = go;
            scr.Team = Team.Player;
            scr.LifeTime = 15f;

            grenadeObject = b;

            return true;
        }
        else
        {
            grenadeObject.GetComponent<Grenade>().ManualDetonate();
            //grenadeObject = null;

            return false;
        }
    }

    public override void ProcessDuringSpecial(GameObject go, Vector3 mousePos)
    {
        base.ProcessDuringSpecial(go, mousePos);

        if (grenadeObject != null)
        {
            grenadeObject.GetComponent<Grenade>().ManualDetonate();
            //grenadeObject = null;
        }
    }
}