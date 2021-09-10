using UnityEngine;

public class QuakeBox : Tower
{
    private float shootTimer;

    [SerializeField]
    private int shootTimeInitialValue = 200;
    public override void Shoot()
    {
        Debug.Log("Tremble");
    }

    public void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= (shootTimeInitialValue-towerData.fireRate)/60f) //turning to seconds
        {
            shootTimer = 0;
            Shoot();
        }
    }
    
    public override void Init()
    {
        base.Init();
        
    }
}