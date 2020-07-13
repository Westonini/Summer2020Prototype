using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public string textToDisplay;
    public int fontSize = 10;

    [Space]
    public TextMeshProUGUI textBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Human") && collision.gameObject.tag == "Player")
        {
            textBox.fontSize = fontSize;
            textBox.text = textToDisplay;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Human") && collision.gameObject.tag == "Player")
            textBox.text = "";
    }
}
