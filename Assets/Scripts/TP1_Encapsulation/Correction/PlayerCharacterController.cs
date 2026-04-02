using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TP1_Encapsulation.Correction
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerCharacterController : MonoBehaviour
    {
        private PlayerCharacter playerCharacter;
        private Rigidbody rb;

        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;

        private bool isGrounded;
        private float horizontalInput;
        private float verticalInput;

        // R�f�rences aux animations (si besoin)
        private Animator animator;

        // Effet sp�cial pour le dash (particules, tra�n�e, etc.)
        [SerializeField] private ParticleSystem dashEffect;
        [SerializeField] private float dashCost = 25f; // Co�t en mana pour le dash
        [SerializeField] private float dashMultiplier = 2.5f; // Multiplicateur de vitesse pour le dash
        [SerializeField] private float dashDuration = 0.5f; // Dur�e du dash
        private bool isDashing = false;

        private void Awake()
        {
            playerCharacter = GetComponent<PlayerCharacter>();
            animator = GetComponent<Animator>();

            // Configurer le rigidbody pour un jeu 3D
            rb = GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.LogError("Rigidbody component is missing on this GameObject.");
            }

            if (groundCheck == null)
            {
                // Cr�er un point de v�rification du sol s'il n'est pas d�fini
                groundCheck = new GameObject("GroundCheck").transform;
                groundCheck.SetParent(transform);
                groundCheck.localPosition = new Vector3(0, -1f, 0);
            }
        }

        private void Update()
        {
            // R�cup�rer les entr�es du joueur
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // V�rifier si le joueur est au sol
            CheckGrounded();

            // Gestion du saut
            if (Input.GetButtonDown("Jump") && isGrounded && !playerCharacter.IsDead)
            {
                Jump();
            }

            // Gestion du dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
            {
                TryDash();
            }

            // Mise � jour des animations (si besoin)
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            // Ne pas permettre de mouvement si le personnage est mort
            if (playerCharacter.Health <= 0) return;

            // Appliquer le mouvement
            if (!isDashing)
            {
                Move();
            }
        }

        private void Move()
        {
            // Utiliser la vitesse de d�placement du PlayerCharacter
            float currentSpeed = playerCharacter.MoveSpeed;

            // Mouvement pour un jeu 3D
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized * currentSpeed;
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

            // Rotation du personnage dans la direction du mouvement (pour 3D)
            if (movement.x != 0 || movement.z != 0)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            }
        }

        private void Jump()
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void CheckGrounded()
        {
            
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void TryDash()
        {
            // V�rifier si le joueur a assez de mana pour faire un dash
            if (playerCharacter.SpendMana(dashCost))
            {
                StartCoroutine(DashRoutine());
            }
            else
            {
                Debug.Log("Pas assez de mana pour dash!");
            }
        }

        private IEnumerator DashRoutine()
        {
            isDashing = true;

            // Activer l'effet de dash
            if (dashEffect != null)
            {
                dashEffect.Play();
            }

            // Activer l'invincibilit� pendant le dash
            playerCharacter.ActivateInvincibility(dashDuration);

            // Appliquer le dash
            float originalSpeed = playerCharacter.MoveSpeed;
            float dashSpeed = originalSpeed * dashMultiplier;

            // Pour 3D
            Vector3 dashDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            if (dashDirection == Vector3.zero) dashDirection = transform.forward;
            rb.linearVelocity = dashDirection * dashSpeed;

            yield return new WaitForSeconds(dashDuration);

            // R�initialiser l'�tat du dash
            isDashing = false;
        }

        private void UpdateAnimations()
        {
            if (animator != null)
            {
                // Mettre � jour les param�tres d'animation
                float movementMagnitude = new Vector2(horizontalInput, verticalInput).magnitude;
                animator.SetFloat("Speed", movementMagnitude);
                animator.SetBool("IsGrounded", isGrounded);
                animator.SetBool("IsDashing", isDashing);
            }
        }

        // M�thodes pour les interactions avec le monde
        public void TakePowerUp(string powerUpType)
        {
            switch (powerUpType)
            {
                case "Speed":
                    // Augmenter temporairement la vitesse
                    StartCoroutine(SpeedBoostRoutine(5f, 1.5f));
                    break;
                case "Health":
                    // R�cup�rer de la vie
                    playerCharacter.Heal(20);
                    break;
                case "Mana":
                    // R�cup�rer du mana
                    playerCharacter.SpendMana(-50); // Utilisation n�gative pour gagner du mana
                    break;
                    // Autres types de power-ups...
            }
        }

        private IEnumerator SpeedBoostRoutine(float duration, float multiplier)
        {
            float originalSpeed = playerCharacter.MoveSpeed;
            playerCharacter.SetMoveSpeed(originalSpeed * multiplier);

            yield return new WaitForSeconds(duration);

            playerCharacter.SetMoveSpeed(originalSpeed);
        }

        // Dessiner les gizmos pour le debug
        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = isGrounded ? Color.green : Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}