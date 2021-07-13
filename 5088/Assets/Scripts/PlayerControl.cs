using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed;                        // 플레이어 이동 속도
    [SerializeField]
    private float lookSensitivity;              // 감도
    [SerializeField]
    private float cameraRotationLimit;          // 카메라 회전 제한
    private float currentCameraRotationX = 0f;   // 현재 카메라 각도

    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;

    public bool isFreeMode = true;                   // 자유이동모드 여부

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFreeMode)
        {
            Move();                 // wasd 플레이어 움직임
            CameraRotation();       // 상하 카메라 회전
            //CharacterRotation();    // 좌우 캐릭터 회전
        }
    }

    void FixedUpdate()
    {
        FreezeRotation();
    }

    private void Move()
    {
       
        // Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;  // 속도 곱해주고 정규화

        // myRigid.MovePosition(transform.position + velocity * Time.deltaTime);   // 실제 플레이어 움직이기
        float inputHorizontal = Input.GetAxis("Horizontal"); // 수평이동
        float inputVertical = Input.GetAxis("Vertical"); // 수직이동

        Vector3 moveDirection = new Vector3(inputHorizontal, 0f, inputVertical); // 이동 방향 벡터 잡기

        transform.Translate(moveDirection * speed * Time.deltaTime); // 정규화하여 이동
    }

    // 상하 카메라 회전
    private void CameraRotation()
    {
        float inputMouseHor = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime; // 마우스 수평이동 입력
        float inputMouseVer = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime; // 마우스 상하이동 입력

        //float xRotation = Input.GetAxisRaw("Mouse Y");          // 마우스 입력 설정
        //float cameraRotationX = xRotation * lookSensitivity;    // 회전값과 감도 설정
        currentCameraRotationX -= inputMouseVer;              // 현재 카메라 회전값에 더해줌(반전시켜서)
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);    // 정해준 각도만큼만 카메라가 회전하도록 제한

        theCamera.transform.localRotation = Quaternion.Euler(currentCameraRotationX, 0f, 0f);    // 카메라 상하회전
        transform.Rotate(0f, inputMouseHor, 0f); // Player 오브젝트 수평회전
    }

    void FreezeRotation()
    {
        // 회전 속도 0 (= 물체에 충돌하면 플레이어가 혼자 회전하는 버그 해결)
        myRigid.angularVelocity = Vector3.zero;
    }

    // 좌우 캐릭터 회전
    //private void CharacterRotation()
    //{
    //    //float yRotation = Input.GetAxisRaw("Mouse X");          // 마우스 입력 설정
    //    //Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;  // 캐릭터 회전 벡터값과 감도 설정
    //    //myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationY));  // 플레이어에 적용
    //    //                                        // Euler값을 Quaternion으로 바꿈
    //}
}
