using UnityEngine.UI;
using UnityEngine;

public class InputFieldLookForManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        GetComponent<InputField>().text = "";
    }

    public void OnPressButtonOutPanel()
    {
        gameObject.SetActive(false);
    }
}
