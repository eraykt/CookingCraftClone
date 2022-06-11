using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region Player Movement Scripts 

    PlayerMovement _mover;
    MobileInputs _mobileInput;

    #endregion

    [SerializeField] MineController _mine;
    [SerializeField] Transform stackTransform;
    
    #region Player Movement Bools

    private bool dragStarted;
    private bool isMoving;

    #endregion

    private void Awake()
    {
        _mover = GetComponent<PlayerMovement>();
        _mobileInput = new MobileInputs();
    }

    void Update()
    {
        if (_mobileInput.TouchCount > 0)
        {
            if (_mobileInput.touch.phase == TouchPhase.Began)
            {
                isMoving = true;
                _mover.FirstTouch();
            }
        }
        
        if (isMoving)
        {
            if (_mobileInput.touch.phase == TouchPhase.Moved)
            {
                dragStarted = true;
                _mover.Moving();
            }

            if (_mobileInput.touch.phase == TouchPhase.Ended)
            {
                _mover.Ended();
                isMoving = false;
                dragStarted = false;
            }
        }

        if (dragStarted)
        {
            _mover.Move();
            _mover.Rotate();
        }


    }

    public IEnumerator Stack()
    {
        while (GameManager.Instance.PlayerStack < GameManager.Instance.PlayerStackLimit)
        {
            yield return new WaitForSeconds(_mine.collectingSpeed);
            stackTransform.GetChild(GameManager.Instance.PlayerStack).gameObject.SetActive(true);
            GameManager.Instance.PlayerStack++;
            yield return null;

        }
    }
}
