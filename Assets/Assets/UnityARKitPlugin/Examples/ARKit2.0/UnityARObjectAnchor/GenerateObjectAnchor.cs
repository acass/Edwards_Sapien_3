using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GenerateObjectAnchor : MonoBehaviour 
{

	[SerializeField]
	ARReferenceObjectAsset referenceObjectAsset;

	[SerializeField]
	GameObject prefabToGenerate;

	GameObject objectAnchorGO;

    public Text mText;

    void Start () {
		UnityARSessionNativeInterface.ARObjectAnchorAddedEvent += AddObjectAnchor;
		UnityARSessionNativeInterface.ARObjectAnchorUpdatedEvent += UpdateObjectAnchor;
		UnityARSessionNativeInterface.ARObjectAnchorRemovedEvent += RemoveObjectAnchor;
	}

	void AddObjectAnchor(ARObjectAnchor arObjectAnchor)
	{
		Debug.Log ("object anchor added");
        mText.text = "object anchor added: " + referenceObjectAsset.name + "  tracked: " + referenceObjectAsset.objectName;
        if (arObjectAnchor.referenceObjectName == referenceObjectAsset.objectName) {
			Vector3 position = UnityARMatrixOps.GetPosition (arObjectAnchor.transform);
			Quaternion rotation = UnityARMatrixOps.GetRotation (arObjectAnchor.transform);

			objectAnchorGO = Instantiate<GameObject> (prefabToGenerate, position, rotation);
		}
	}

	void UpdateObjectAnchor(ARObjectAnchor arObjectAnchor)
	{
		Debug.Log ("object anchor updated");
        mText.text = "object anchor updated: " + referenceObjectAsset.name + "  tracked: " + referenceObjectAsset.objectName;
        if (arObjectAnchor.referenceObjectName == referenceObjectAsset.objectName) {
			objectAnchorGO.transform.position = UnityARMatrixOps.GetPosition (arObjectAnchor.transform);
			objectAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation (arObjectAnchor.transform);
		}

	}

	void RemoveObjectAnchor(ARObjectAnchor arObjectAnchor)
	{
		Debug.Log ("object anchor removed");
        mText.text = "object anchor removed: " + referenceObjectAsset.name + "  tracked: " + referenceObjectAsset.objectName;
        if ((arObjectAnchor.referenceObjectName == referenceObjectAsset.objectName) && (objectAnchorGO != null)) {
			GameObject.Destroy (objectAnchorGO);
		}
	}

	void OnDestroy()
	{
		UnityARSessionNativeInterface.ARObjectAnchorAddedEvent -= AddObjectAnchor;
		UnityARSessionNativeInterface.ARObjectAnchorUpdatedEvent -= UpdateObjectAnchor;
		UnityARSessionNativeInterface.ARObjectAnchorRemovedEvent -= RemoveObjectAnchor;

	}

	// Update is called once per frame
	void Update () {

	}
}
