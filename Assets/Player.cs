using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
     * Mise en situation : 
     * Yana se retrouve contre le mighty Patriark, mais une chose est très spo0ky!
     * Quand Yana se déplace en diagonale elle va plus vite... why?
     * 
     * 1 - Fixer les mouvements diagonales qui sont plus rapides
     * Indices - vecteur.magnitude donne la longueur du vecteur
     * 
     * 2 - Yana a une détection de marde, elle peut seulement voir devant elle
     * Il serait mieux d'envoyer plusieurs raycast avec un angle qui les séparents
     * Pourtant le code semble valide, il y a surement une erreur dans la classe "MathLib"
     * Indice y'a des debug.drawray, alors va en "scene mode" pour test
     * 
     * 3 - Yana a vraiment peur devant le godly Patriark...
     * Elle aimerait créer un shield avec "Spacebar" mais pourtant il est de taille 0
     * Le shield lui permetterais de résister plusieurs coup de "God Slash of justice"
     * Il parait que si tu prend un vecteur de longueur radius et que tu le rotate
     * On peut faire un cercle composé de plusieurs points..
     * 
     * 4 (Bonus) - Le bouclier de Yana fonctionne, mais n'est pas optimal.
     * Une manière de le rendre plus optimal serais de diffracté 
     * les impacts en aillant des curves dans le bouclier. Puisque Yana aime les maths,
     * Elle considérais rajouter une fonction Sin qui affecterais sa bulles
     * 
     */

    [Header("Stats")]
    [SerializeField] float speed;

    [Header("Detection")]
    [SerializeField] float rayCastLength;
    [SerializeField] float[] rayCastAngle;

    [Header("Shield")]
    //combien de point pour faire le cerlce du shield
    [SerializeField] int shieldResolution;
    [SerializeField] float shieldRadiusMax;
    [SerializeField] float shieldTimeToMaxRadius;
    [SerializeField] float sinFrequency;
    [SerializeField] float sinTemplarMagic;
    float shieldCurrentTimer;

    LineRenderer lineRenderer;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector2 input = GetJoystickInput();

        Move(input);
        Detection(input);
        GenerateShield();
    }

    Vector2 GetJoystickInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        return new Vector2(xInput, yInput);
    }

    private void Move(Vector2 input)
    {
        transform.position += (Vector3)input * speed * Time.deltaTime;
    }

    private void Detection(Vector2 direction)
    {
        //Aucun input
        if (direction.magnitude == 0)
            return;

        //Ray cast stuff
        RaycastHit hitInfo;
        bool hitTarget = false;

        //Boucle sur tous les angles ou tant que la detection n'a rien touché
        for (int i = 0; i < rayCastAngle.Length && !hitTarget; i++)
        {
            //J'ai entendu parler d'une légende raccontante une function dans MathLib pour rotate un vector..
            Vector3 realDirection = direction;
            hitTarget = Physics.Raycast(transform.position, realDirection, out hitInfo, rayCastLength);
            Debug.DrawRay(transform.position, realDirection * rayCastLength);
        }

        if (hitTarget)
        {
            //On s'en criss
        }
    }

    void GenerateShield()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            shieldCurrentTimer += Time.deltaTime;
        }
        else
        {
            shieldCurrentTimer -= Time.deltaTime;
        }

        //Limite la valeur du timer entre 0 et max
        shieldCurrentTimer = Mathf.Clamp(shieldCurrentTimer, 0, shieldTimeToMaxRadius);

        //Calculer le ratio entre 0 et 1 du timer
        float t = shieldCurrentTimer / shieldTimeToMaxRadius;

        float radius = Mathf.Lerp(0, shieldRadiusMax, t);
        GenerateShieldLineRenderer(radius);
    }

    void GenerateShieldLineRenderer(float radius)
    {
        Vector3[] pointPositions = new Vector3[shieldResolution];
        float angle = 360f / (shieldResolution - 1);

        for (int i = 0; i < shieldResolution; i++)
        {
            //Calculer les points autour du cercle
            pointPositions[i] = Vector3.zero;
        }

        lineRenderer.positionCount = shieldResolution;
        lineRenderer.SetPositions(pointPositions);
    }

    float GetSinShieldBubble(float radius, int i)
    {
        float magicNumber = i * Mathf.PI / shieldResolution / sinTemplarMagic;
        //Formule pour deflecter 50% des damages, essaye des shits qui deflecte 75%
        float sinOfTemplar = Mathf.Sin(magicNumber + Time.time * sinFrequency);
        float sinOfGuardian = (radius/50) + sinOfTemplar;

        return radius * sinOfGuardian; 
    }
}
