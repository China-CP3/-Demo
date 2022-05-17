using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;//����������Ŀ��
    public Vector3 offsetPos;//�����ƫ��λ��
    public float bodyHeight;//����λ�õ�yƫ��
    //��������ƶ�����ת�ٶ�
    public float moveSpeed;
    public float rotationSpeed;
    private Vector3 targetPos;
    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target==null)
        {
            return;
        }
        targetPos = target.position+target.forward*offsetPos.z;//Vector3.forward ����������ϵ  target.forward���ǽ�ɫ�Լ�������ϵ
        targetPos += Vector3.up * offsetPos.y;
        targetPos += target.right * offsetPos.x;
        this.transform.position=Vector3.Lerp(this.transform.position, targetPos, moveSpeed*Time.deltaTime);//�ȿ����  a+(b-a)*t
        //Vector3.Slerp(this.transform.position, targetPos, moveSpeed*Time.deltaTime); ����

        targetRotation = Quaternion.LookRotation((target.position+Vector3.up*bodyHeight)-this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation,rotationSpeed*Time.deltaTime);
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
