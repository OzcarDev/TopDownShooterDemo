using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : TankMovement
{
    [Header("Weapons")]
    [SerializeField] private int missileAmount = 2;
    public int MissileAmount { get { return missileAmount; } }

    private Vector2 movementInput;
    private ShootingSystem playerShootingSystem;
    private int maxMissileAmount = 2;
    public int MaxMissileAmount { set { maxMissileAmount = value; } get { return maxMissileAmount; } }

    private void Start()
    {
        playerShootingSystem = gameObject.GetComponent<ShootingSystem>();
    }
    private void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            GetShootingInput();
            
            Vector3 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RotateCanon(mousePos);
        }
        
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPaused)
        {
            Move(GetMovementInput());
        }
    }

    private void GetShootingInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(playerShootingSystem.ShootingBulletsCoroutine(Proyectile.ProyectileType.bullet));

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (missileAmount > 0)
            {
                StartCoroutine(playerShootingSystem.ShootingBulletsCoroutine(Proyectile.ProyectileType.missile));
                missileAmount--;
            }
        }
    }

    private Vector2 GetMovementInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        return new Vector2(movementInput.x, movementInput.y).normalized;
    }

    public void RestoreMissile()
    {
        missileAmount = maxMissileAmount;
    }

    public void OnGamePaused()
    {
        rb.velocity = Vector2.zero;
    }

}
