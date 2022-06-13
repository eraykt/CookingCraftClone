using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacks : MonoBehaviour
{
    [SerializeField] GameObject stackPrefab;
    [SerializeField] Transform objTransform;
    [SerializeField] float distance;

    [SerializeField] MineController _mine;

    public IEnumerator AddStack()
    {
        while (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
        {
            yield return new WaitForSeconds(_mine.collectingSpeed);
            Instantiate(stackPrefab, new Vector3(objTransform.transform.position.x, objTransform.transform.position.y, objTransform.transform.position.z), objTransform.rotation, this.transform);
            objTransform.transform.position = new Vector3(objTransform.transform.position.x, objTransform.transform.position.y + distance, objTransform.transform.position.z);
            GameManager.Instance.PlayerStack++;
            yield return null;
        }
    }
}
