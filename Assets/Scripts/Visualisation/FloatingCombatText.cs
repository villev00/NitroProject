using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class FloatingCombatText : MonoBehaviour
{
    [SerializeField]
    Transform damageText;
    [SerializeField]
    GameObject pfDamagePopUp;
    float moveSpeed;
    float fadeSpeed;
    float disappearTimer;
    Color textColor;

    TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        moveSpeed = 1f;
        disappearTimer = 1f;
        fadeSpeed = 2f;
    }

    public static FloatingCombatText Create(Vector3 position, float damageAmount)
    {
        //position += new Vector3(0, 1.2f, 0);
        GameObject damagePopUpTransform = Instantiate(DamagePopUpAsset.i.pfDamagePopUp, position, Quaternion.identity);
        FloatingCombatText damagePopUp = damagePopUpTransform.GetComponent<FloatingCombatText>();
        damagePopUp.SetUp(damageAmount);
        return damagePopUp;
    }
    
    void SetUp(float damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
    }

    private void Update()
    {
        SetPosition();
        disappearTimer -= Time.deltaTime;
        FadeText();
    }

    void SetPosition()
    {
        transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        textMesh.transform.rotation = Camera.main.transform.rotation;
    }

    void FadeText()
    {
        if(disappearTimer < 0)
        {
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textMesh.color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
